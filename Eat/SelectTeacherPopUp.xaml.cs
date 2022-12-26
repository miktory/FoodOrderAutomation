using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eat.CollectionItems;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectTeacherPopUp : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private BriefInfo _selectedTeacher;
        private ObservableCollection<BriefInfo> _teachers;
        private EditGradePage _parent;
        private Grade _grade;
        public ObservableCollection<BriefInfo> Teachers { get => _teachers; }
        public SelectTeacherPopUp(List<BriefInfo> teachers, EditGradePage parent, BriefInfo selectedTeacher, Grade grade)
        {
            InitializeComponent();
            _teachers = new ObservableCollection<BriefInfo>(teachers);
            _parent = parent;
            _selectedTeacher = selectedTeacher;
            _grade = grade;
            if (_teachers.Count != 0)
            {
                foreach (var teacher in _teachers)
                    teacher.ChangeSelectionStatus(false);
                var temp = new List<BriefInfo>(_teachers);
                if (temp.Find(x => x.ID == _selectedTeacher.ID) != null)
                {
                    temp.Find(x => x.ID == _selectedTeacher.ID).ChangeSelectionStatus(true);
                    _selectedTeacher = temp.Find(x => x.ID == _selectedTeacher.ID);
                    _teachers = new ObservableCollection<BriefInfo>(temp);
                    TeacherCollectionView.ItemsSource = Teachers;
                }
                else
                {
                    _selectedTeacher = _teachers[0];
                }
            }
            else
            {
                _selectedTeacher = new BriefInfo("Default", -1, false);
            }
            BindingContext = this;
        }
        private async void SelectItem(object sender, EventArgs e)
        {
            BriefInfo selectedTeacher = (BriefInfo)((TappedEventArgs)e).Parameter;   
            foreach (var teacher in _teachers)
                teacher.ChangeSelectionStatus(false);
            selectedTeacher.ChangeSelectionStatus(true);
            _selectedTeacher = selectedTeacher;          
        }
        private async void SaveAndExit(object sender, EventArgs e)
        {
            if (!Database.DatabaseInfo.ChangeGradeTeacher(_grade, _selectedTeacher.ID))
                await DisplayAlert("Уведомление", "Не удалось сменить учителя.", "ОК");
            else
                _grade.ChangeTeacher(_selectedTeacher.ID);
            _parent.LoadCollection(_grade);
            PopupNavigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}