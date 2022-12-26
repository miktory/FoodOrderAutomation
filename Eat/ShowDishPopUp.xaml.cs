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
    public partial class ShowDishPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Dish SelectedDish { get => _selectedDish; }
        private Dish _selectedDish;
        private SelectDishesPage _parent;
        private UserInfo.UserInfo _userInfo;
        private UserMenu _userMenu;
        public string SaveButtonText { get => _selectedDish.IsInSelectedDateMenu ? "Удалить" : "Выбрать"; }
        public ShowDishPopUp(Dish dish, UserInfo.UserInfo userInfo, UserMenu userMenu, SelectDishesPage parent)
        {
            InitializeComponent();
            _selectedDish = dish;
            _parent = parent;
            _userInfo = userInfo;
            _userMenu = userMenu;
            BindingContext = this;
        }
        private void SaveChanges(object sender, EventArgs e)
        {
            if (_selectedDish.IsInSelectedDateMenu)
            {
                _selectedDish.SetAvailabilityStatus(false);
                Database.DatabaseInfo.DeleteDishFromMenu(_userInfo, _userMenu, _selectedDish);
            }
            else
            {
                _selectedDish.SetAvailabilityStatus(true);
                Database.DatabaseInfo.AddDishToMenu(_userInfo, _userMenu, _selectedDish);
            }
            _parent.UpdateCollection();
            PopupNavigation.PopAsync();

        }

        
    }
}