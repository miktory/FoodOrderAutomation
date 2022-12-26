using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eat.HomePage;
using Eat.Database;
using Eat.AdditionalFunctions;
using Eat.CollectionItems;
using Rg.Plugins.Popup.Services;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class ParentHomePage : ContentPage
    {
        public string NameAndSurname { get => _nameAndSurname; }
        public string[] Roles { get => _roles; }
        public List<UserInfo.UserInfo> Children { get => _children; }
        public string SelectedRoleName { get => _selectedRoleName; }
        private string _nameAndSurname = "";
        private string[] _roles = new string[] { };
        private string _selectedRoleName = "";
        private List<UserInfo.UserInfo> _children = new List<UserInfo.UserInfo>();
        private UserInfo.UserInfo _userInfo;
        private int[] _relatedID = new int[] { };
    
        public ParentHomePage(int userId)
        {
            InitializeComponent();
            var userInfo = Database.DatabaseInfo.GetUserInfo(userId);
            _userInfo = userInfo;
            _nameAndSurname = userInfo.Name + " " + userInfo.Surname;
            _selectedRoleName = Database.DatabaseInfo.GetKeyValueByParameter("roles_description", "name", "id", "3").ToUpper();
            _relatedID = userInfo.RelatedID;
            foreach (var e in _relatedID)
            {
                var child = Database.DatabaseInfo.GetUserInfo(e);
                _children.Add(child);
            }
            Console.WriteLine("Старт");
            Console.WriteLine(userInfo.Roles[0].ToString());
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
      
        private async void DisplayRoleSelection(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SelectRolePopUp(_userInfo, new MainPage1(_userInfo.Id)));
        }
        private void OpenChildProfile(object sender, EventArgs e)
        {
            UserInfo.UserInfo child= (UserInfo.UserInfo)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new MainPage1(child.Id));
        }
    } 
}