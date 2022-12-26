using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eat.CollectionItems;
using Rg.Plugins.Popup.Services;

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        private string _login;
        private string _password;
        private string _name;
        private string _surname;
        private string _patronymic;
        private UserInfo.UserInfo _selectedUser;
        public UserInfo.UserInfo SelectedUser { get => _selectedUser; }
        public string Password { get => _password; }
        public string Login { get => _login; }
        public EditUserPopUp(UserInfo.UserInfo userInfo)
        {
            InitializeComponent();;
            _selectedUser = userInfo;
            _login = Database.DatabaseInfo.GetUserLogin(userInfo.Id);
            _password = Database.DatabaseInfo.GetUserPassword(userInfo.Id);
            BindingContext = this;
        }
        private async void EditUser(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            switch (entry.ClassId)
            {
                case "UserLogin":
                    _login = entry.Text;
                    break;
                case "UserPassword":
                    _password = entry.Text;
                    break;
                case "UserName":
                    _name = entry.Text;
                    break;
                case "UserSurname":
                    _surname = entry.Text;
                    break;
                case "UserPatronymic":
                    _patronymic = entry.Text;
                    break;
            }
        }
        private async void CreateUser(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_surname) || string.IsNullOrWhiteSpace(_patronymic) || string.IsNullOrWhiteSpace(_login) || string.IsNullOrWhiteSpace(_password))
            {
                await DisplayAlert("Ошибка", "Информация о человеке заполнена неверно", "OK");
            }
            else
            {
                if (Database.DatabaseInfo.CreateUser(_login, _password, _name, _surname, _patronymic))
                {
                    await DisplayAlert("Уведомление", "Профиль создан. Теперь настройте его.", "OK");
                    PopupNavigation.PopAsync();
                }
                else 
                {
                    await DisplayAlert("Ошибка", "Ошибка при создании профиля. Возможно, он создался частично. Если это так - удалите его и создайте заново.", "OK");
                }
            }

        }
        private async void DeleteUser(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Удаление", "Вы действительно хотите удалить выбранного пользователя?", "ДА", "НЕТ");
            if (result)
            {
                if (Database.DatabaseInfo.DeleteUser(_selectedUser.Id))
                {
                    await DisplayAlert("Уведомление", "Выбранный пользователь удалён.", "ОК");
                    PopupNavigation.PopAsync();
                }
                else
                    await DisplayAlert("Ошибка", "Не удалось удалить выбранного пользователя. Если пользователь является классным руководителем какого-либо класса, то, перед удалением, нужно изменить классного руководителя.", "ОК");
            }
        }
        private async void ModifyUsersRelations(object sender, EventArgs e)
        {
            var users = await Task.Run(() => Database.DatabaseInfo.GetAllUsersList());
            PopupNavigation.PopAsync();
            Navigation.PushAsync(new SelectUserPage(users,_selectedUser));
        }
        private async void UpdateBalance(object sender, EventArgs e)
        {
            var balance = Database.DatabaseInfo.GetUserBalance(_selectedUser);
            if (balance != -1)
            {
                var result = await DisplayPromptAsync("Редактирование", "Укажите баланс", "ОК", "ОТМЕНА", initialValue: balance.ToString(), keyboard: Keyboard.Numeric);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (Database.DatabaseInfo.UpdateUserBalance(_selectedUser, Double.Parse(result)))
                        await DisplayAlert("Уведомление", "Баланс обновлён.", "ОК");
                    else
                        await DisplayAlert("Ошибка", "Не удалось обновить баланс.", "ОК");
                }
            }
            else
                await DisplayAlert("Ошибка", "Не удалось загрузить баланс.", "ОК");

        }
        private async void UpdateUser(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Редактирование", "Вы действительно хотите обновить данные выбранного пользователя?", "ДА", "НЕТ");
            if (result)
            {
                if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_surname) || string.IsNullOrWhiteSpace(_patronymic) || string.IsNullOrWhiteSpace(_login) || string.IsNullOrWhiteSpace(_password))
                {
                    await DisplayAlert("Ошибка", "Информация о человеке заполнена неверно", "OK");
                }
                else
                {
                    if (Database.DatabaseInfo.UpdateUser(_selectedUser.Id, _login, _password, _name, _surname, _patronymic))
                    {
                        await DisplayAlert("Уведомление", "Данные обновлены.", "ОК");
                        PopupNavigation.PopAsync();
                    }
                    else
                        await DisplayAlert("Ошибка", "Не удалось обновить данные.", "ОК");
                }
            }
        }
        private async void ChangeRoles(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new EditRolesPopUp(_selectedUser));
        }
    }
}