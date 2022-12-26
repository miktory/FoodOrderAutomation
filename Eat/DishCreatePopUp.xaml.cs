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
    public partial class DishCreatePopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Dish _newDish;
        private string _name;
        private string _description;
        private double _cost = 0;
        private EditCategoryPage _parent;
        private int _categoryID;
        public DishCreatePopUp(int categoryID, EditCategoryPage parent)
        {
            InitializeComponent();;
            _parent = parent;
            _categoryID = categoryID;
            BindingContext = this;
        }
        async void EditDish(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            switch (entry.ClassId)
            {
                case "DishName":
                    _name = entry.Text;
                    break;
                case "DishDescription":
                    _description = entry.Text;
                    break;
                case "DishCost":
                    Double.TryParse(entry.Text, out _cost);
                    break;
            }
        }
        private async void SaveChanges(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_description) || _cost == 0)
            {
                await DisplayAlert("Ошибка", "Информация о блюде заполнена неверно", "OK");
            }
            else
            {
                _newDish = new Dish(_name, _description, _cost);
                Database.DatabaseInfo.CreateDish(_newDish, _categoryID);
                _parent.UpdateCollection();
                PopupNavigation.PopAsync();
            }

        }
    }
}