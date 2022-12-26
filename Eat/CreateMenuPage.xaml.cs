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
    public partial class CreateMenuPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<Dish> DishList { get; private set; }
        public string CategoryName { get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", _categoryID.ToString()); }
        private ObservableCollection<Dish> _dishListTemp;
        private int _categoryID;
        private DateTime _selectedDate;
        public CreateMenuPage(int categoryID, DateTime selectedDate)
        {
            InitializeComponent();
            _categoryID = categoryID;
            NavigationPage.SetHasNavigationBar(this, false);
            DishList = Database.DatabaseInfo.GetDishList(categoryID);
            _selectedDate = selectedDate;
            foreach (var dish in DishList)
                dish.SetAvailabilityStatus(Database.DatabaseInfo.GetAvailabilityStatus(_selectedDate, dish));
            _dishListTemp = DishList;
            Console.WriteLine("test");
            Console.WriteLine(DishList.Count());
            BindingContext = this;
        }
        private async void SelectDish(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var dish = (Dish)btn.BindingContext;
            if (dish.IsInSelectedDateMenu)
            {
                btn.BackgroundColor = Color.White;
                dish.SetAvailabilityStatus(false);
                Database.DatabaseInfo.SetAvailabilityStatus(_selectedDate, dish, _categoryID, false);
            }
            else
            {
                btn.BackgroundColor = Color.Green;
                dish.SetAvailabilityStatus(true);
                Database.DatabaseInfo.SetAvailabilityStatus(_selectedDate, dish, _categoryID, true);
            }

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
            _dishListTemp = Database.DatabaseInfo.GetDishList(_categoryID);
            DishList = _dishListTemp;
            DishCollectionView.ItemsSource = DishList;
        }
        private void ClosePage(object sender, EventArgs e)
        {
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