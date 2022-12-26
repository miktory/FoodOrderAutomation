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
    public partial class SelectDishesPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<Dish> DishList { get; private set; }
        public string CategoryName { get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", _categoryID.ToString()); }
        private ObservableCollection<Dish> _dishListTemp;
        private int _categoryID;
        private DateTime _selectedDate;
        private UserMenu _userMenu;
        private UserInfo.UserInfo _userInfo;
        private MainPage1 _parent;
        public SelectDishesPage(int categoryID, DateTime selectedDate, UserMenu userMenu, UserInfo.UserInfo userInfo, MainPage1 parent)
        {
            InitializeComponent();
            _parent = parent;
            _categoryID = categoryID;
            _userMenu = userMenu;
            _userInfo = userInfo;
            NavigationPage.SetHasNavigationBar(this, false);
            DishList = Database.DatabaseInfo.GetTodayDishList(categoryID, selectedDate);
            _selectedDate = selectedDate;
            foreach (var dish in DishList)
                dish.SetAvailabilityStatus(Database.DatabaseInfo.IsDishInUserMenu(_userInfo,userMenu, dish));
            _dishListTemp = DishList;
            Console.WriteLine("test");
            Console.WriteLine(DishList.Count());
            Console.WriteLine(userMenu.Name);
            BindingContext = this;
        }
        private async void SelectDish(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var dish = (Dish)btn.BindingContext;
            //   if (dish.IsInSelectedDateMenu)
             //  {
               //   btn.BackgroundColor = Color.White;
                 //  dish.SetAvailabilityStatus(false);
                 // Database.DatabaseInfo.DeleteDishFromMenu(_userInfo, _userMenu, dish);
             // }
              // else
              // {
               //   btn.BackgroundColor = Color.Green;
               //  dish.SetAvailabilityStatus(true);
               // Database.DatabaseInfo.AddDishToMenu(_userInfo, _userMenu, dish);
              // }
            PopupNavigation.PushAsync(new ShowDishPopUp(dish,_userInfo,_userMenu,this));

        }
        private void FilterItems(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            var filter = entry.Text;
            if (string.IsNullOrEmpty(filter))
                DishCollectionView.ItemsSource = new ObservableCollection<Dish>(_dishListTemp);
            else
                DishCollectionView.ItemsSource = new ObservableCollection<Dish>(DishList.Where((dish) => dish.Name.ToLower().Contains(filter.ToLower())));
        }
        public void UpdateCollection()
        {

            DishList = Database.DatabaseInfo.GetTodayDishList(_categoryID, _selectedDate);
            foreach (var dish in DishList)
                dish.SetAvailabilityStatus(Database.DatabaseInfo.IsDishInUserMenu(_userInfo, _userMenu, dish));
            _dishListTemp = DishList;
            DishCollectionView.ItemsSource = DishList;
        }
        private void ClosePage(object sender, EventArgs e)
        {
            _parent.LoadChangesFromDatabase();
            Navigation.PopAsync();
        }

        private async void DishCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var previous = e.PreviousSelection;
            var current = e.CurrentSelection;
            await DisplayAlert("Ошибка", current.ToString(), "OK");
        }

    }
}