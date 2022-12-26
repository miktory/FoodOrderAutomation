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
    public partial class EditRolesPopUp : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private ObservableCollection<Role> _userRoles;
        private UserInfo.UserInfo _userInfo;
        public ObservableCollection<Role> UserRoles { get => _userRoles; }
        public EditRolesPopUp(UserInfo.UserInfo userInfo)
        {
            InitializeComponent();
            _userInfo = userInfo;
            UpdateCollection();       
            BindingContext = this;
        }

        private async void SelectItem(object sender, EventArgs e)
        {
            Role role = (Role)((TappedEventArgs)e).Parameter;
            if (role.IsSelected)
            {
                if (Database.DatabaseInfo.DeleteRoleFromUser(_userInfo.Id, role.ID))
                {
                    await DisplayAlert("Уведомление", "Выбранная роль убрана из списка ролей пользователя.", "ОК");
                    role.ChangeSelectionStatus(false);
                }
                else
                    await DisplayAlert("Ошибка", "Не удалось убрать выбранную роль из списка ролей пользователя.", "ОК");
            }
            else
            {
                if (Database.DatabaseInfo.AddRoleToUser(_userInfo.Id, role.ID))
                {
                    await DisplayAlert("Уведомление", "Выбранная роль добавлена в список ролей пользователя.", "ОК");
                    role.ChangeSelectionStatus(true);
                }
                else
                    await DisplayAlert("Ошибка", "Не удалось добавить выбранную роль в список ролей пользователя.", "ОК");
            }
            UpdateCollection();
        }
        private async void CreateMenu(object sender, EventArgs e)
        {
        }
        private async void DeleteMenu(object sender, EventArgs e)
        {
        }
        private async void UpdateCollection()
        {
            _userRoles = new ObservableCollection<Role>() { new Role(1, "", false), new Role(2, "", false), new Role(3, "", false), new Role(4, "", false), new Role(5, "", false) };
            foreach (var role in _userRoles)
            {
                role.ChangeName(Database.DatabaseInfo.GetKeyValueByParameter("roles_description", "name", "id", role.ID.ToString()));
                var status = Int32.Parse(Database.DatabaseInfo.GetQuantityByParameter("user_roles", string.Format("role_id={0} AND user_id", role.ID), _userInfo.Id.ToString())) > 0 ? true : false;
                role.ChangeSelectionStatus(status);
            }
        }
        private async void SaveAndExit(object sender, EventArgs e)
        {        
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