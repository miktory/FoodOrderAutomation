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

namespace Eat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryPage : ContentPage
    {
        public ObservableCollection<Dish> DishList { get; private set; }
        private ObservableCollection<Dish> _dishListTemp;
        public string CategoryName { get => Database.DatabaseInfo.GetKeyValueByParameter("dish_categories", "name", "id", _categoryID.ToString()); }
        private int _categoryID;
        public EditCategoryPage(int categoryID)
        {
            InitializeComponent();
            _categoryID = categoryID;
            NavigationPage.SetHasNavigationBar(this, false);
            DishList = Database.DatabaseInfo.GetDishList(categoryID);
            _dishListTemp = DishList;
            Console.WriteLine(DishList.Count());
            BindingContext = this;
        }
        private async void ShowEditPopUp(object sender, EventArgs e)
        {
            Dish dish = (Dish)((TappedEventArgs)e).Parameter;
            PopupNavigation.PushAsync(new DishEditPopUp(dish, this));
        }
        private async void ShowCreatePopUp(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new DishCreatePopUp(_categoryID, this));
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
    }
}