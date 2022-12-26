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
    public partial class DishEditPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Dish SelectedDish { get => _selectedDish; }
        private Dish _selectedDish;
        private EditCategoryPage _parent;
        public DishEditPopUp(Dish dish, EditCategoryPage parent)
        {
            InitializeComponent();
            _selectedDish = dish;
            _parent = parent;
            BindingContext = this;
        }
        async void EditDish(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var popUpText = "Введите";
            var popUpInitialValue = "";
            var popUpKeyboardType = Keyboard.Text;
            var result = "";
            switch (label.ClassId)
            {
                case "DishName":
                    popUpText += " название:";
                    popUpInitialValue = _selectedDish.Name;
                    result = await DisplayPromptAsync("Редактирование", popUpText, initialValue: popUpInitialValue, maxLength: 50, keyboard: popUpKeyboardType);
                    if (!string.IsNullOrEmpty(result))
                        _selectedDish.SetName(result);
                    break;
                case "DishDescription":
                    popUpText += " описание:";
                    popUpInitialValue = _selectedDish.Description;
                    result = await DisplayPromptAsync("Редактирование", popUpText, initialValue: popUpInitialValue, maxLength: 50, keyboard: popUpKeyboardType);
                    if (!string.IsNullOrEmpty(result))
                        _selectedDish.SetDescription(result);
                    break;
                case "DishCost":
                    popUpText += " цену:";
                    popUpInitialValue = _selectedDish.Cost.ToString();
                    popUpKeyboardType = Keyboard.Numeric;
                    result = await DisplayPromptAsync("Редактирование", popUpText, initialValue: popUpInitialValue, maxLength: 50, keyboard: popUpKeyboardType);
                    if (!string.IsNullOrEmpty(result))
                    {
                        _selectedDish.SetCost(Double.Parse(result));
                        result = _selectedDish.CostString;
                    }
                    break;
            }
            if (!string.IsNullOrEmpty(result))
                label.Text = result;
        }
        private void SaveChanges(object sender, EventArgs e)
        {
            Database.DatabaseInfo.UpdateDish(_selectedDish);
            _parent.UpdateCollection();
            PopupNavigation.PopAsync();

        }
        private async void DeleteDish(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Удаление", "Вы уверены, что хотите удалить данное блюдо?", "ДА", "НЕТ");
            if (answer)
            {
                Database.DatabaseInfo.DeleteDish(_selectedDish);
                _parent.UpdateCollection();
                PopupNavigation.PopAsync();
            }
        }
    }
}