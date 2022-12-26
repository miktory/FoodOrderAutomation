using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.Effects;
using Npgsql;

namespace Eat
{
    public partial class MainPage : ContentPage
    {
        NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
         //   Navigation.PushAsync(new MainPage1(14));
         // Navigation.PushAsync(new RequestPage(Database.DatabaseInfo.GetUserInfo(14),1));
            // Navigation.PushAsync(new ParentHomePage(12));
            //  Navigation.PushAsync(new EditCategoryPage(1));

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var userId = await Task.Run(() => Database.DatabaseInfo.LogIn(loginBar.Text, passwordBar.Text));
            if (userId != -1)
            {
                var userInfo = await Task.Run(() => Database.DatabaseInfo.GetUserInfo(userId));
                if (userInfo.Id != -1)
                {
                    var selectedRole = userInfo.SelectedRole;
                    if (selectedRole == 3)
                        await Navigation.PushAsync(new ParentHomePage(userId));
                    else
                        await Navigation.PushAsync(new MainPage1(userId));
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось выполнить вход.", "OK");
                }

            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось выполнить вход.", "OK");
            }
        }

    }
}

