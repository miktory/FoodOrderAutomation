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
    public partial class ReportPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<GroupOrder> Orders { get => new ObservableCollection<GroupOrder>(_orders); }
        private List<GroupOrder> _orders;
        private DateTime _selectedDate;
        private List<Dish> _dishList;
        public ReportPage(DateTime selectedDate)
        {
            InitializeComponent();
            _selectedDate = selectedDate;
            LoadCollection();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        public async void LoadCollection()
        {
            _orders = new List<GroupOrder>();
            var dishList = await Task.Run(() => GetDishList());
            _dishList = dishList;
            if (dishList.Count() > 0)
            {
                var grades = await Task.Run(() => Database.DatabaseInfo.GetAllGradeList());
                var staffIdList = await Task.Run(() => Database.DatabaseInfo.GetStaffIdList());
                if (grades.Count != 0)
                {
                    foreach (var grade in grades)
                    {
                        var groupOrderDishList = new List<Dish>();
                        foreach (var dish in _dishList)
                            groupOrderDishList.Add(dish.Clone());
                        var groupOrder = new GroupOrder(grade.Name.ToUpper(), new List<Order>(), groupOrderDishList, false);   
                        foreach (var studentID in grade.StudentsID)
                        {
                            var order = await Database.DatabaseInfo.GetUserOrder(studentID, _selectedDate);
                            if (order.UserID == -1)
                            {
                                _orders = new List<GroupOrder>();
                                OnPropertyChanged("Orders");
                                return;
                            }
                            else if (order.DishID.Count != 0)
                            {
                                groupOrder.DishList.FindAll(x => order.DishID.Contains(x.ID)).ForEach( x => x.IncreaseCount(1));
                                groupOrder.AddOrder(order);
                            }
                        }
                        if (groupOrder.Orders.Count() > 0)
                        {  
                            groupOrder.DishList.RemoveAll(x => x.Count == 0);
                            _orders.Add(groupOrder);
                        }                       
                    }
                }
                if (staffIdList != null)
                {
                    var groupOrderDishList = new List<Dish>();
                    foreach (var dish in _dishList)
                        groupOrderDishList.Add(dish.Clone());
                    var groupOrder = new GroupOrder("ПЕРСОНАЛ", new List<Order>(), groupOrderDishList, false);
                    foreach (var id in staffIdList)
                    {
                        var order = await Database.DatabaseInfo.GetUserOrder(id, _selectedDate);
                        if (order.UserID == -1)
                        {
                            _orders = new List<GroupOrder>();
                            OnPropertyChanged("Orders");
                            return;
                        }
                        else if (order.DishID.Count != 0)
                        {
                            groupOrder.DishList.FindAll(x => order.DishID.Contains(x.ID)).ForEach(x => x.IncreaseCount(1));
                            groupOrder.AddOrder(order);
                        }
                    }
                    if (groupOrder.Orders.Count() > 0)
                    {
                        groupOrder.DishList.RemoveAll(x => x.Count == 0);
                        _orders.Add(groupOrder);
                    }
                }
                else 
                {
                    _orders = new List<GroupOrder>();
                }
                OnPropertyChanged("Orders");
            }
        }
        private void ClosePage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void ViewFullReport(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            if (_dishList != null && _dishList.Count > 0 && _orders.Count > 0)
            {
                var resultDishList = new List<Dish>();
                foreach (var dish in _dishList)
                    resultDishList.Add(dish.Clone());
                foreach (var groupOrder in _orders)
                    groupOrder.DishList.ForEach(x => resultDishList.Find(y => y.ID == x.ID).IncreaseCount(x.Count));
                resultDishList.RemoveAll(x => x.Count == 0);
                foreach (var dish in resultDishList)
                    sb.Append(string.Format("{0}: {1} шт. \n", dish.Name, dish.Count));
            }
            await DisplayAlert("Полный отчёт", sb.ToString(), "ОК");
        }
        private async void OpenCategory(object sender, EventArgs e)
        {
            GroupOrder groupOrder = (GroupOrder)((TappedEventArgs)e).Parameter;
            var usersInfo = new List<BriefInfo>();
            foreach (var order in groupOrder.Orders)
            {
                var userInfo = await Task.Run(() => Database.DatabaseInfo.GetUserInfo(order.UserID));
                if (userInfo.Id != -1)
                {
                    var briefUserInfo = new BriefInfo(string.Format("{0} {1} {2}", userInfo.Surname, userInfo.Name, userInfo.Patronymic), userInfo.Id, false);
                    usersInfo.Add(briefUserInfo);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось загрузить список пользователей.", "ОК");
                    return;
                }
            }
            Navigation.PushAsync(new SelectUserPage(usersInfo, groupOrder));
        }
        private void ShowOrderInfo(object sender, EventArgs e)
        {
            GroupOrder groupOrder = (GroupOrder)((TappedEventArgs)e).Parameter;
            if (groupOrder.IsSelected)
                groupOrder.ChangeSelectionStatus(false);
            else
                groupOrder.ChangeSelectionStatus(true);
        }
        private List<Dish> GetDishList()
        {
            var finalList = new List<Dish>();
            var firstCategory = new List<Dish>(Database.DatabaseInfo.GetDishList(1));
            var secondCategory = new List<Dish>(Database.DatabaseInfo.GetDishList(2));
            var thirdCategory = new List<Dish>(Database.DatabaseInfo.GetDishList(3));
            if (firstCategory != null && secondCategory.Count() != null && thirdCategory.Count() != null)
            {
                finalList.AddRange(firstCategory);
                finalList.AddRange(secondCategory);
                finalList.AddRange(thirdCategory);
            }
            else
            {
                finalList = new List<Dish>();
            }
            return finalList;       
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}