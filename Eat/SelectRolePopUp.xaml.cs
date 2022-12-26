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
    public partial class SelectRolePopUp : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private int _selectedRoleTemp;
        private MainPage1 _parent;
        private ObservableCollection<Role> _userRoles;
        private UserInfo.UserInfo _userInfo;
        private bool _isDeleteEnabled = false;
        public ObservableCollection<Role> UserRoles { get => _userRoles; }
        public bool IsDeleteEnabled { get => _isDeleteEnabled; }
        public SelectRolePopUp(UserInfo.UserInfo userInfo, MainPage1 parent)
        {
            InitializeComponent();
            _userInfo = userInfo;
            _parent = parent;
            _selectedRoleTemp = userInfo.SelectedRole;
            UpdateCollection();       
            BindingContext = this;
        }

        private async void SelectItem(object sender, EventArgs e)
        {
            Role role = (Role)((TappedEventArgs)e).Parameter;
            _userInfo.ChangeSelectedRole(role.ID);
            role.ChangeSelectionStatus(true);
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
            var userRolesList = new List<Role>();
            foreach (var id in _userInfo.Roles)
            {
                int currentRoleID = 2;
                string currentRoleName = "Default";
                bool isSelected = Int32.Parse(id) == _userInfo.SelectedRole ? true : false;
                Int32.TryParse(id, out currentRoleID);
                currentRoleName = Database.DatabaseInfo.GetKeyValueByParameter("roles_description", "name", "id", currentRoleID.ToString());
                Role role = new Role(currentRoleID,currentRoleName,isSelected);
                userRolesList.Add(role);
            }
            _userRoles = new ObservableCollection<Role>(userRolesList);
            MenuCollectionView.ItemsSource = _userRoles;
        }
        private async void SaveAndExit(object sender, EventArgs e)
        {
            Database.DatabaseInfo.UpdateUserInfo(_userInfo);
            _parent.LoadChangesFromDatabase();
            if (_userInfo.SelectedRole == 3)
            {
                if (_selectedRoleTemp !=3)
                    Navigation.PushAsync(new ParentHomePage(_userInfo.Id));
            }
            else if (_selectedRoleTemp == 3)
                Navigation.PushAsync(_parent);
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
            SaveAndExit(null, null);
            return base.OnBackgroundClicked();
        }
    }
}