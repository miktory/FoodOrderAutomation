using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eat.CollectionItems;
using Xamarin.Forms;
using Rg.Plugins.Popup;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestPage : ContentPage
    {
        public ObservableCollection<BriefInfo> Students { get => _students; }
        public string GradeName { get => _selectedGrade.Name; }
        private ObservableCollection<BriefInfo> _students;
        private UserInfo.UserInfo _userInfo;
        private Grade _selectedGrade;
        public RequestPage(UserInfo.UserInfo userInfo)
        {
            InitializeComponent();    
            NavigationPage.SetHasNavigationBar(this, false);
            _userInfo = userInfo;
            _students = new ObservableCollection<BriefInfo>();
            var grades = Database.DatabaseInfo.GetTeacherGradeList(userInfo);
            if (grades.Count != 0)
            {
                LoadCollection(grades[0]);
                _selectedGrade = grades[0];
            }
            else
            {
                var emptyGrade = new Grade(-1, "Default", false, new List<int>(), -1);
                LoadCollection(emptyGrade);
                _selectedGrade = emptyGrade;
            }

            BindingContext = this;
        }
        private async void ShowGradeSelectionPopUp(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new SelectGradePopUp(_userInfo, this, _selectedGrade));
        }
        private async void TagStudent(object sender, EventArgs e)
        {
            BriefInfo selectedStudent = (BriefInfo)((TappedEventArgs)e).Parameter;
            if (selectedStudent.IsSelected)
                if (Database.DatabaseInfo.TagStudent(selectedStudent.ID, _userInfo.SelectedDate, false))
                {
                    selectedStudent.ChangeSelectionStatus(false);
                }
                else
                    await DisplayAlert("Ошибка", "Невозможно отметить ученика. Скорее всего, он не оплатил заказ на текущую дату.", "ОК");
            else
            {
                if (Database.DatabaseInfo.TagStudent(selectedStudent.ID, _userInfo.SelectedDate, true))
                {
                    selectedStudent.ChangeSelectionStatus(true);
                }
                else
                    await DisplayAlert("Ошибка", "Невозможно отметить ученика. Скорее всего, он не оплатил заказ на текущую дату.", "ОК");
            }
        }

        public void LoadCollection(Grade grade)
        {
            _students = new ObservableCollection<BriefInfo>();
            _selectedGrade = grade;
            foreach (var id in grade.StudentsID)
            {
                var userInfo = Database.DatabaseInfo.GetUserInfo(id);
                if (userInfo.Name == "")
                {
                    _students.Clear();
                    break;
                }
                else
                {
                    var orderStatus = Database.DatabaseInfo.GetKeyValueByParameter("orders", "status", string.Format("user_id = {0} AND order_date ", id), string.Format("'{0}'", _userInfo.SelectedDate.ToString("yyyy.MM.dd")));
                    _students.Add(new BriefInfo(userInfo.Surname + " " + userInfo.Name + " " + userInfo.Patronymic, userInfo.Id, orderStatus == "-1" ? false : bool.Parse(orderStatus)));
                }
            }
            StudentCollectionView.ItemsSource = Students;
            OnPropertyChanged("GradeName");
        }
        private void ClosePage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}