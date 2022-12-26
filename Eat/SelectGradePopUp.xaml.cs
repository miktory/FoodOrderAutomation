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
    public partial class SelectGradePopUp : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private Grade _selectedGrade;
        private RequestPage _parentRequest;
        private EditGradePage _parentEditGrade;
        private ObservableCollection<Grade> _grades;
        private UserInfo.UserInfo _userInfo;
        public bool IsAdditionalButtonsVisible { get => _parentEditGrade == null ? false: true; }
        public ObservableCollection<Grade> Grades { get => _grades; }
        public SelectGradePopUp(UserInfo.UserInfo userInfo, RequestPage parent, Grade selectedGrade)
        {
            InitializeComponent();
            _userInfo = userInfo;
            _parentRequest = parent;
            LoadCollection();      
            if (_grades.Count != 0)
            {
                var temp = new List<Grade>(_grades);
                if (temp.Find(x => x.ID == selectedGrade.ID) != null)
                {
                    temp.Find(x => x.ID == selectedGrade.ID).ChangeSelectionStatus(true);
                    _selectedGrade = temp.Find(x => x.ID == selectedGrade.ID);
                    _grades = new ObservableCollection<Grade>(temp);
                    GradeCollectionView.ItemsSource = Grades;
                }
                else
                {
                    _selectedGrade = _grades[0];
                }
            }
            else
            {
                _selectedGrade = new Grade(-1, "Default", false, new List<int>(),-1);
            }
            BindingContext = this;
        }
        public SelectGradePopUp(UserInfo.UserInfo userInfo, EditGradePage parent, Grade selectedGrade)
        {
            InitializeComponent();
            _userInfo = userInfo;
            _parentEditGrade = parent;
            LoadCollection();
            if (_grades.Count != 0)
            {
                var temp = new List<Grade>(_grades);
                if (temp.Find(x => x.ID == selectedGrade.ID) != null)
                {
                    temp.Find(x => x.ID == selectedGrade.ID).ChangeSelectionStatus(true);
                    _selectedGrade = temp.Find(x => x.ID == selectedGrade.ID);
                    _grades = new ObservableCollection<Grade>(temp);
                    GradeCollectionView.ItemsSource = Grades;
                }
                else
                {
                    _selectedGrade = _grades[0];
                }
            }
            else
            {
                _selectedGrade = new Grade(-1, "Default", false, new List<int>(), -1);
            }
            BindingContext = this;
        }
        public async void EditGradeName(object sender, EventArgs e)
        {
            var popUpInitialValue = _selectedGrade.Name;
            var result = await DisplayPromptAsync("Редактирование","Введите название класса: " , initialValue: popUpInitialValue, maxLength: 30);
            if (!string.IsNullOrEmpty(result))
            {
                if (Database.DatabaseInfo.ChangeGradeName(_selectedGrade, result))
                {
                    _selectedGrade.ChangeName(result);
                    _parentEditGrade.LoadCollection(_selectedGrade);
                    await DisplayAlert("Уведомление", "Название класса изменено.", "ОК");
                }
                else
                    await DisplayAlert("Уведомление", "Не удалось изменить названик класса.", "ОК");
            }
        }
        public async void CreateGrade(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Создание", "Введите название класса: ");
            if (!string.IsNullOrEmpty(result))
            {
                if (Database.DatabaseInfo.CreateGrade(result, _userInfo.Id))
                {
                    await DisplayAlert("Уведомление", "Новый класс создан.", "ОК");
                    LoadCollection();
                    if (_grades.Count > 0)
                    {
                        _grades[0].ChangeSelectionStatus(true);
                        _selectedGrade = _grades[0];
                    }
                    _parentEditGrade.LoadCollection(_selectedGrade);
                }
                else
                    await DisplayAlert("Уведомление", "Не удалось создать новый класс.", "ОК");
            }
        }
        public async void DeleteGrade(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Удаление", "Вы действительно хотите удалить этот класс?", "ДА", "НЕТ");
            if (result)
            {
                if (Database.DatabaseInfo.DeleteGrade(_selectedGrade))
                {
                    await DisplayAlert("Уведомление", "Класс удалён.", "ОК");
                    LoadCollection();
                    if (_grades.Count > 0)
                    {
                        _grades[0].ChangeSelectionStatus(true);
                        _selectedGrade = _grades[0];
                    }
                    else
                    {
                        _selectedGrade = new Grade(-1, "Default", false, new List<int>(), -1);
                    }
                    _parentEditGrade.LoadCollection(_selectedGrade);                  
                    
                }
                else
                    await DisplayAlert("Уведомление", "Не удалось удалить класс.", "ОК");
            }
        }
        private async void SelectItem(object sender, EventArgs e)
        {
            Grade selectedGrade = (Grade)((TappedEventArgs)e).Parameter;   
            foreach (var grade in _grades)
                grade.ChangeSelectionStatus(false);
            selectedGrade.ChangeSelectionStatus(true);
            _selectedGrade = selectedGrade;
        }
        private void LoadCollection()
        {
           if (_parentEditGrade == null)
                _grades = new ObservableCollection<Grade>(Database.DatabaseInfo.GetTeacherGradeList(_userInfo));
           else
                _grades = new ObservableCollection<Grade>(Database.DatabaseInfo.GetAllGradeList());
            GradeCollectionView.ItemsSource = _grades;
        }
        private async void SaveAndExit(object sender, EventArgs e)
        {
            if (_parentEditGrade == null)
                _parentRequest.LoadCollection(_selectedGrade);
            else
                _parentEditGrade.LoadCollection(_selectedGrade);
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