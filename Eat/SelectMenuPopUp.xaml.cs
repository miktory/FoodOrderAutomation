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
    public partial class SelectMenuPopUp : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private UserMenu _selectedMenu;
        private MainPage1 _parent;
        private ObservableCollection<UserMenu> _userMenus;
        private UserInfo.UserInfo _userInfo;
        private bool _isDeleteEnabled = false;
        public ObservableCollection<UserMenu> UserMenus { get => _userMenus; }
        public bool IsDeleteEnabled { get => _isDeleteEnabled; }
        public SelectMenuPopUp(UserInfo.UserInfo userInfo, MainPage1 parent)
        {
            InitializeComponent();
            _userInfo = userInfo;
            UpdateCollection();
            _parent = parent;
            BindingContext = this;
        }

        private async void SelectItem(object sender, EventArgs e)
        {
            UserMenu menu = (UserMenu)((TappedEventArgs)e).Parameter;
            _userInfo.ChangeSelectedMenu(menu.MenuID);
            menu.ChangeSelectionStatus(true);
            _selectedMenu = menu;
            UpdateCollection();
        
        }
        private async void CreateMenu(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Создание меню", "Название меню", "СОЗДАТЬ", "ОТМЕНА", maxLength: 20);
            if (string.IsNullOrEmpty(result))
                await DisplayAlert("Ошибка", "Название меню не может быть пустым.", "ОК");
            else
            {
                Database.DatabaseInfo.CreateUserMenu(_userInfo,result);
                UpdateCollection();
            }
        }
        private async void DeleteMenu(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Удаление меню", "Вы уверены, что хотите удалить данное меню?", "ДА", "ОТМЕНА");
            if (result)
            {
                Database.DatabaseInfo.DeleteUserMenu(_userInfo, _selectedMenu);
                UpdateCollection();
            }
        }
        private async void UpdateCollection()
        {
            _userMenus = Database.DatabaseInfo.GetUserMenus(_userInfo);
            MenuCollectionView.ItemsSource = UserMenus;
            if (_userMenus.Any() == true)
            {
                if (_userMenus.Count() >= 2)
                {
                    _isDeleteEnabled = true;
                    OnPropertyChanged("IsDeleteEnabled");
                } else
                {
                    _isDeleteEnabled = false;
                    OnPropertyChanged("IsDeleteEnabled");
                }
                _selectedMenu = new List<UserMenu>(_userMenus).Find(x => x.MenuID == _userInfo.SelectedMenu);
            }
            else
            {
                _isDeleteEnabled = false;
                OnPropertyChanged("IsDeleteEnabled");
            }
        }
        private async void SaveAndExit(object sender, EventArgs e)
        {
            Database.DatabaseInfo.UpdateUserInfo(_userInfo);
            _parent.LoadChangesFromDatabase();
            PopupNavigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected override bool OnBackgroundClicked()
        {
            SaveAndExit(null,null);
            return base.OnBackgroundClicked();
        }
    }
}