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
    public partial class CreateUserPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        private string _login;
        private string _password;
        private string _name;
        private string _surname;
        private string _patronymic;
        public CreateUserPopUp()
        {
            InitializeComponent();;

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
                Console.WriteLine(string.Join(" ", new string[] { _name, _surname, _patronymic, _login, _password }));
            }
            else
            {
                if (Database.DatabaseInfo.CreateUser(_login, _password, _name, _surname, _patronymic))
                {
                    await DisplayAlert("Уведомление", "Профиль создан. Теперь настройте его.", "OK");
                }
                else 
                {
                    await DisplayAlert("Ошибка", "Ошибка при создании профиля. Возможно, он создался частично. Если это так - удалите его и создайте заново.", "OK");
                }
            }

        }
    }
}