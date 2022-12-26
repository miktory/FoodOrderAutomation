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
using System.ComponentModel;
using Rg.Plugins.Popup.Services;
using Eat.CollectionItems;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class MainPage1 : ContentPage, INotifyPropertyChanged
    {
        public string NameAndSurname { get => _nameAndSurname; }
        public string Balance { get => _balance.ToString() + " " + "₽"; }
        public string[] Roles { get => _roles; }
        public string SelectedDate { get => _selectedDate.Date.ToString("dd.MM.yyyy"); set
            {
                OnPropertyChanged("SelectedDate");
            }
        }
        public string ProfileDescription
        {
            get => "ID: " + _userInfo.Id; 
        }
        public string FirstCategoryName
        {
            get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "1");
        }
        public string FirstCategoryDescription
        {
            get {
                var number = Database.DatabaseInfo.GetQuantityByParameter("date_menu", "category_id=1 AND date", "'" + _selectedDate.Date.ToString("MM.dd.yyyy") + "'");
                return number + " " + AdditionalFunctions.Functions.Declension.GetDeclension(Int32.Parse(number),"выбор","выбора", "выборов"); 
            }
        }
        public string SecondCategoryName
        {
            get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "2");
        }
        public string SecondCategoryDescription
        {
            get
            {
                var number = Database.DatabaseInfo.GetQuantityByParameter("date_menu", "category_id=2 AND date", "'" + _selectedDate.Date.ToString("MM.dd.yyyy") + "'");
                return number + " " + AdditionalFunctions.Functions.Declension.GetDeclension(Int32.Parse(number), "выбор", "выбора", "выборов");
            }
        }
        public string ThirdCategoryName
        {
            get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "3");
        }
        public string ThirdCategoryDescription
        {
            get
            {
                var number = Database.DatabaseInfo.GetQuantityByParameter("date_menu", "category_id=3 AND date", "'" + _selectedDate.Date.ToString("MM.dd.yyyy") + "'");
                return number + " " + AdditionalFunctions.Functions.Declension.GetDeclension(Int32.Parse(number), "выбор", "выбора", "выборов");
            }
        }
        public string SelectedMenuName { get => _selectedMenuName; set => OnPropertyChanged("SelectedMenuName"); }
        public double SelectedMenuCost
        {
            get
            {
                return _selectedMenuCost; 
            }
            set
            {
                OnPropertyChanged("SelectedMenuCost");
                OnPropertyChanged("PayButtonText");
            }
        }
        public string PayButtonText
        {
            get
            {
                if (_paymentStatus)
                    return "Отменить";
                else
                    return "Оплатить " + "(" + _selectedMenuCost.ToString() + "₽" + ")";
            }
        }
        public Color PayButtonColor
        {
            get
            {
                if (_paymentStatus)
                    return Color.White;
                else
                    return Color.FromHex("#EF3124");
            }
        }
        public Color PayButtonTextColor
        {
            get
            {
                if (_paymentStatus)
                    return Color.FromHex("#EF3124");
                else
                    return Color.White;
            }
        }        
        public string SelectedRoleName { get => _selectedRoleName; set => OnPropertyChanged("SelectedRoleName"); }
        private int _selectedRole = 2;
        private string _selectedMenuName = "";
        private string _nameAndSurname = "";
        private double _balance = 0;
        private string[] _roles = new string[] { };
        private DateTime _selectedDate = new DateTime();
        private string _selectedRoleName = "";
        private UserInfo.UserInfo _userInfo;
        private double _selectedMenuCost = 0;
        private bool _paymentStatus;
        private UserMenu _selectedMenu { get => new List<UserMenu>(Database.DatabaseInfo.GetUserMenus(_userInfo)).Find(x => x.MenuID == _userInfo.SelectedMenu); }


        public MainPage1(int userId)
        {
            InitializeComponent();
            var userInfo = Database.DatabaseInfo.GetUserInfo(userId);
            _nameAndSurname = userInfo.Name + " " + userInfo.Surname;
            _balance = userInfo.Balance;
            _selectedDate = userInfo.SelectedDate;
            _userInfo = userInfo;
            _selectedRole = userInfo.SelectedRole;
            _selectedRoleName = Database.DatabaseInfo.GetKeyValueByParameter("roles_description", "name", "id", _selectedRole.ToString()).ToUpper();
            NavigationPage.SetHasNavigationBar(this, false);
            _selectedDate = DateTime.Now;
            _userInfo.ChangeSelectedDate(_selectedDate);
          //  _selectedMenu = new List<UserMenu>(Database.DatabaseInfo.GetUserMenus(_userInfo)).Find(x => x.MenuID == _userInfo.SelectedMenu);
            _selectedMenuName = _selectedMenu.Name;
            DatabaseInfo.UpdateUserInfo(_userInfo);
            _selectedMenuCost = DatabaseInfo.GetUserMenuCost(_userInfo);
            _paymentStatus = Int32.Parse(Database.DatabaseInfo.GetQuantityByParameter("orders", string.Format("user_id = {0} AND order_date", _userInfo.Id), "'" + _selectedDate.Date.ToString("MM.dd.yyyy") + "'")) > 0 ? true : false;
            AdaptPageToRole();
            BindingContext = this;
        }
        private async void SelectDate(object sender, EventArgs e)
        {
            if (_selectedRole != 4)
                datePicker.MinimumDate = DateTime.Now;
            datePicker.MaximumDate = DateTime.Now.AddDays(7);
            datePicker.Date = _selectedDate;
            Device.BeginInvokeOnMainThread(() =>
            {
                if (datePicker.IsFocused)
                    datePicker.Unfocus();
                datePicker.Focus();
            });

        }
        private async void DisplayNotifications(object sender, EventArgs e)
        {
            var notification = await Task.Run(() => DatabaseInfo.GetNotifications());
            if (_selectedRole == 4)
            {
                var result = await DisplayAlert("Уведомления", notification, "СОЗДАТЬ", "ОК");
                if (result)
                {
                    var text = await DisplayPromptAsync("Уведомления", "Введите текст уведомления:", "СОЗДАТЬ", "ОТМЕНА");
                    if (!string.IsNullOrEmpty(text))
                    {
                        var status = await Task.Run(() => Database.DatabaseInfo.CreateNotification(text));
                        if (!status)
                            await DisplayAlert("Ошибка", "Не удалось создать уведомление.", "ОК");
                    }
                }
            }
            else
                await DisplayAlert("Уведомления", notification, "OK");
        }
        private async void OpenFirstCategory(object sender, EventArgs e)
        {
       //      Navigation.PushAsync(new CreateMenuPage(1,_selectedDate));
            Navigation.PushAsync(new SelectDishesPage(1, _selectedDate,_selectedMenu,_userInfo,this));
        }
        private async void OpenSecondCategory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectDishesPage(2, _selectedDate, _selectedMenu, _userInfo,this));
        }
        private async void OpenThirdCategory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectDishesPage(3, _selectedDate, _selectedMenu, _userInfo,this));
        }
        private async void DisplayMenuSelection(object sender, EventArgs e)
        {
             await PopupNavigation.PushAsync(new SelectMenuPopUp(_userInfo,this));
        }
        private async void DisplayRoleSelection(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SelectRolePopUp(_userInfo, this));
        }
        public async void Pay(object sender, EventArgs e)
        {
            if (_paymentStatus)
            {
                if (DatabaseInfo.CancelOrder(_userInfo))
                    await DisplayAlert("Уведомление", "Заказ отменён.", "OK");
                else 
                {
                    await DisplayAlert("Уведомление", "Не удалось отменить заказ. Возможно, он уже подтверждён.", "OK");
                }
            }
            else
            {
                if (_selectedMenuCost != 0)
                {
                    var checkStatus = DatabaseInfo.GetTodayAndUserMenuEquality(_selectedMenu, _selectedDate);
                    if (checkStatus)
                    {
                        if (DatabaseInfo.MakeOrder(_userInfo, _selectedMenu))
                            await DisplayAlert("Уведомление", "Оплата произведена.", "OK");
                        else
                            await DisplayAlert("Уведомление", "Ошибка оплаты. Недостаточно средств, либо ошибка со стороны сервера.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Уведомление", "Невозможно сделать заказ. Блюд из вашего меню нет в наличии.", "OK");
                    }

                }
            }
            LoadChangesFromDatabase();
        }
        public void OpenServiceMenu(object sender, EventArgs e)
        {
            if (_selectedRole == 1)
                Navigation.PushAsync(new RequestPage(_userInfo));
            else
            {
                Navigation.PushAsync(new ServiceMenuPage(_userInfo));
            }
        }
        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _selectedDate = e.NewDate;
            SelectedDate = _selectedDate.Date.ToString("dd.MM.yyyy");
            _userInfo.ChangeSelectedDate(_selectedDate);
            DatabaseInfo.UpdateUserInfo(_userInfo);
            LoadChangesFromDatabase();
        }
        public void LoadChangesFromDatabase()
        {      
            _userInfo = Database.DatabaseInfo.GetUserInfo(_userInfo.Id);
            _selectedMenuCost = DatabaseInfo.GetUserMenuCost(_userInfo);
            SelectedMenuCost = _selectedMenuCost;
            _selectedMenuName = new List<UserMenu>(Database.DatabaseInfo.GetUserMenus(_userInfo)).Find(x => x.MenuID == _userInfo.SelectedMenu).Name;
            SelectedMenuName = _selectedMenuName;
            _selectedRole = _userInfo.SelectedRole;
            _selectedRoleName = Database.DatabaseInfo.GetKeyValueByParameter("roles_description", "name", "id", _selectedRole.ToString()).ToUpper();
            SelectedRoleName = _selectedRoleName;
            _paymentStatus = Int32.Parse(Database.DatabaseInfo.GetQuantityByParameter("orders", string.Format("user_id = {0} AND order_date", _userInfo.Id), "'" + _selectedDate.Date.ToString("MM.dd.yyyy") + "'")) > 0 ? true : false;
            _balance = _userInfo.Balance;
            OnPropertyChanged("PayButtonText");
            OnPropertyChanged("PayButtonColor");
            OnPropertyChanged("PayButtonTextColor");
            OnPropertyChanged("Balance");
            AdaptPageToRole();
        }
        private void AdaptPageToRole()
        {
            if (_selectedRole != 1 && _selectedRole != 4 && _selectedRole != 5)
                ServiceButton.IsVisible = false;
            else
            {
                ServiceButton.IsVisible = true;
            }
        }

    } 
}