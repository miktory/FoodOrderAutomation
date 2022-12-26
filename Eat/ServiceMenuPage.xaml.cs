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
using System.ComponentModel;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceMenuPage : ContentPage, INotifyPropertyChanged
    {
        public class Setting
        {
            public int ID { get => _id; }
            public string Name { get => _name; }
            private int _id;
            private string _name;
            public Setting(int id, string name)
            {
                _name = name;
                _id = id;
            }
        }
        public List<Setting> Settings { get => _settings; }
        private List<Setting> _settings;
        private List<Setting> _settingsTemp;
        private int _roleID;
        private UserInfo.UserInfo _userInfo;
        public ServiceMenuPage( UserInfo.UserInfo userInfo)
        {
            InitializeComponent();
            _roleID = userInfo.SelectedRole;
            _settings = new List<Setting>();
            _settingsTemp = new List<Setting>(_settings);
            _userInfo = userInfo;
            NavigationPage.SetHasNavigationBar(this, false);
            if (_roleID == 4)
            {
                var setting1 = new Setting(1, "Редактировать список блюд");
                var setting2 = new Setting(2, "Сформировать меню на " + _userInfo.SelectedDate.ToString("dd.MM.yyyy"));
                var setting3 = new Setting(3, "Отчёт");
                var setting4 = new Setting(4, "Подтвердить все заказы на " + _userInfo.SelectedDate.ToString("dd.MM.yyyy"));
                var setting5 = new Setting(5, "Отменить подтверждение заказов на " + _userInfo.SelectedDate.ToString("dd.MM.yyyy"));
                var setting6 = new Setting(6, "Очистить список заказов на " + _userInfo.SelectedDate.ToString("dd.MM.yyyy"));
                _settings.Add(setting1);
                _settings.Add(setting2);
                _settings.Add(setting3);
                _settings.Add(setting4);
                _settings.Add(setting5);
                _settings.Add(setting6);
            }
            if (_roleID == 5)
            {
                var setting1 = new Setting(1, "Управление классами");
                var setting2 = new Setting(2, "Редактирование пользователей ");
                _settings.Add(setting1);
                _settings.Add(setting2);
            }
            BindingContext = this;
        }
        public async void OnTapped(object sender, EventArgs e)
        {
            Setting setting = (Setting)(((Button)sender).CommandParameter);
            if (_roleID == 4)
            {
                switch (setting.ID)
                {
                    case 1:
                        _settings = new List<Setting>() { new Setting(7, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "1")),
                            new Setting(8, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "2")),
                            new Setting(9, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "3"))};
                        OnPropertyChanged("Settings");
                        break;
                    case 2:
                        _settings = new List<Setting>() { new Setting(10, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "1")),
                            new Setting(11, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "2")),
                            new Setting(12, Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", "3"))};
                        OnPropertyChanged("Settings");
                        break;
                    case 3:
                        Navigation.PushAsync(new ReportPage(_userInfo.SelectedDate));
                        break;
                    case 4:
                        if (Database.DatabaseInfo.ChangeOrdersStatus(_userInfo.SelectedDate, true))
                            await DisplayAlert("Уведомление", "Статус заказов изменён на: подтверждено.", "ОК");
                        else
                            await DisplayAlert("Уведомление", "Ошибка. Не удалось изменить статус заказов.", "ОК");
                        break;
                    case 5:
                        if (Database.DatabaseInfo.ChangeOrdersStatus(_userInfo.SelectedDate, false))
                            await DisplayAlert("Уведомление", "Статус заказов изменён на: не подтверждено.", "ОК");
                        else
                            await DisplayAlert("Уведомление", "Ошибка. Не удалось изменить статус заказов.", "ОК");
                        break;
                    case 6:
                        var result = await DisplayAlert("Уведомление", "Вы действительно хотите очистить список заказов на " + _userInfo.SelectedDate.ToString("dd.MM.yyyy"), "ДА", "НЕТ");
                        if (result)
                        {
                            if (Database.DatabaseInfo.СlearUserOrders(_userInfo.SelectedDate))
                                await DisplayAlert("Уведомление", "Список заказов очищен.", "ОК");
                            else
                                await DisplayAlert("Уведомление", "Ошибка. Не удалось очистить список заказов.", "ОК");
                        }
                        break;
                    case 7:
                        Navigation.PushAsync(new EditCategoryPage(1));
                        break;
                    case 8:
                        Navigation.PushAsync(new EditCategoryPage(2));
                        break;
                    case 9:
                        Navigation.PushAsync(new EditCategoryPage(3));
                        break;
                    case 10:
                        Navigation.PushAsync(new CreateMenuPage(1,_userInfo.SelectedDate));
                        break;
                    case 11:
                        Navigation.PushAsync(new CreateMenuPage(2, _userInfo.SelectedDate));
                        break;
                    case 12:
                        Navigation.PushAsync(new CreateMenuPage(3, _userInfo.SelectedDate));
                        break;

                }
            }
            if (_roleID == 5)
            {
                switch (setting.ID)
                {
                    case 1:
                        Navigation.PushAsync(new EditGradePage(_userInfo));
                        break;
                    case 2:
                        Navigation.PushAsync(new SelectUserPage(Database.DatabaseInfo.GetAllUsersList()));
                        break;
                }
            }
        }
        private void ClosePage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}