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
    public partial class EditGradePage : ContentPage
    {
        public ObservableCollection<BriefInfo> Students { get => _students; }
        public string GradeName { get => "Класс: " + _selectedGrade.Name; }
        public string TeacherName { get => "Кл. Рук.: " + _selectedTeacher.Name; }
        private ObservableCollection<BriefInfo> _students;
        private UserInfo.UserInfo _userInfo;
        private Grade _selectedGrade;
        private BriefInfo _selectedTeacher;
        private List<BriefInfo> _teachers;
        public EditGradePage(UserInfo.UserInfo userInfo)
        {
            InitializeComponent();    
            NavigationPage.SetHasNavigationBar(this, false);
            _userInfo = userInfo;
            _students = new ObservableCollection<BriefInfo>();
            var grades = Database.DatabaseInfo.GetAllGradeList();
            _teachers = Database.DatabaseInfo.GetTeacherList();
            if (grades.Count != 0)
            {
                LoadCollection(grades[0]);
                _selectedGrade = grades[0];
                if (_teachers.Count != 0)
                {
                    _selectedTeacher = _teachers.Find(x => x.ID == _selectedGrade.TeacherID);
                    if (_selectedTeacher == null)
                        _selectedTeacher = new BriefInfo("Default", -1, false);
                }
            }      
            BindingContext = this;
        }
        private async void ShowGradeSelectionPopUp(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new SelectGradePopUp(_userInfo, this, _selectedGrade));
        }
        private async void OpenUserSelector(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectUserPage(new List<BriefInfo>(Database.DatabaseInfo.GetAllStudentsList()),_selectedGrade));
        }
        private async void ShowTeacherSelectionPopUp(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new SelectTeacherPopUp(_teachers,this,_selectedTeacher,_selectedGrade));
        }
        private async void DeleteStudent(object sender, EventArgs e)
        {
            BriefInfo selectedStudent = (BriefInfo)((TappedEventArgs)e).Parameter;
            var result = await DisplayAlert("Удаление", "Вы действительно хотите удалить ученика из выбранного класса?", "ДА", "НЕТ");
            if (result)
            {
                if (Database.DatabaseInfo.DeleteStudentFromGrade(selectedStudent.ID))
                {
                    if (_selectedGrade.StudentsID.Remove(selectedStudent.ID))
                    {
                        LoadCollection(_selectedGrade);
                        await DisplayAlert("Уведомление", "Ученик удалён из выбранного класса.", "ОК");
                    }
                    else 
                    {
                        await DisplayAlert("Ошибка", "Не удалось удалить ученика из выбранного класса.", "ОК");
                    }
                }
            }
        }

        public async void LoadCollection(Grade grade)
        {
            _students = new ObservableCollection<BriefInfo>();
            _selectedGrade = grade;
            _selectedTeacher = _teachers.Find(x => x.ID == _selectedGrade.TeacherID);
            if (_selectedTeacher == null)
                _selectedTeacher = new BriefInfo("Default", -1, false);
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
            OnPropertyChanged("TeacherName");
        }
        private void ClosePage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        protected override async void OnAppearing()
        {
            LoadCollection(_selectedGrade);
            base.OnAppearing();
        }
    }
}