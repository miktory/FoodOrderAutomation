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
    public partial class SelectUserPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<BriefInfo> Users { get => new ObservableCollection<BriefInfo>(_users); }
        public bool IsEditable { get => _isEditable; }
        private List<BriefInfo> _users;
        private ObservableCollection<BriefInfo> _usersTemp;
        private GroupOrder _groupOrder;
        private Grade _grade;
        private UserInfo.UserInfo _parentUser;
        private bool _isEditable;
        private int _actionType;
        public SelectUserPage(List<BriefInfo> userList, Grade grade)
        {
            InitializeComponent();
            _users = userList;
            _usersTemp = Users;
            _grade = grade;
            _isEditable = false;
            _actionType = 0;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        public SelectUserPage(List<BriefInfo> userList, UserInfo.UserInfo parentUser)
        {
            InitializeComponent();
            _users = userList;
            _usersTemp = Users;
            _parentUser = parentUser;
            _isEditable = false;
            _actionType = 2;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        public SelectUserPage(List<BriefInfo> userList)
        {
            InitializeComponent();
            _users = userList;
            _usersTemp = Users;
            _actionType = 1;
            _isEditable = true;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        public SelectUserPage(List<BriefInfo> userList, GroupOrder groupOrder)
        {
            InitializeComponent();
            _users = userList;
            _usersTemp = Users;
            _groupOrder = groupOrder;
            _isEditable = false;
            _actionType = 3;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }
        private async void SelectUser(object sender, EventArgs e)
        {
            BriefInfo selectedUser = (BriefInfo)((TappedEventArgs)e).Parameter;
            // Action types: 0 - Добавление ученика в выбранный класс, 1 - Редактирование пользователей, 2 - Установка связи родитель - ребёнок, 3 - Просмотр пользовательского заказа
            switch (_actionType)
            {
                case 0:
                    var result = await DisplayAlert("Редактирование", "Вы действительно хотите добавить этого ученика в класс?", "ДА", "НЕТ");
                    if (result)
                    {
                        if (Database.DatabaseInfo.AddStudentToGrade(selectedUser.ID, _grade))
                        {
                            await DisplayAlert("Уведомление", "Ученик добавлен в выбранный класс.", "ОК");
                            _grade.StudentsID.Add(selectedUser.ID);
                        }
                        else
                            await DisplayAlert("Ошибка", "Не удалось добавить ученика в выбранный класс. Возможно, он уже был добавлен в другой класс.", "ОК");
                    }
                    break;
                case 1:
                    PopupNavigation.PushAsync(new EditUserPopUp(Database.DatabaseInfo.GetUserInfo(selectedUser.ID)));
                    break;
                case 2:
                    ModifyUsersRelations(selectedUser);
                    break;
                case 3:
                    ShowUserOrder(selectedUser);
                    break;
            }
        }
        private async void CreateUser(object sender, EventArgs e)
        {
            if (_isEditable)
            {
                PopupNavigation.PushAsync(new CreateUserPopUp());
            }
        }
        private async void ShowUserOrder(BriefInfo selectedUser)
        {
            var sb = new StringBuilder();
            foreach (var dishID in _groupOrder.Orders.Find(x => x.UserID == selectedUser.ID).DishID)
            {
                var dish = _groupOrder.DishList.Find(x => x.ID == dishID);
                sb.Append(string.Format("{0}: {1} шт. \n", dish.Name, dish.Count));
            }
            await DisplayAlert(selectedUser.Name, sb.ToString(),"ОК");
        }
        private async void ModifyUsersRelations(BriefInfo selectedUser)
        {
            var relationsCount = Int32.Parse(await Task.Run(() => Database.DatabaseInfo.GetQuantityByParameter("related_id", string.Format("id='{0}' AND related_id", _parentUser.Id), selectedUser.ID.ToString())));
            if (relationsCount == -1)
            {
                await DisplayAlert("Ошибка", "Не удалось проверить связь между пользователями.", "ОК");
            }
            else if (relationsCount == 0)
            {
                var result = await DisplayAlert("Создание связи", "Вы действительно хотите создать связь с выбранным профилем?", "ДА", "НЕТ");
                if (result)
                {
                    var status = await Task.Run(() => Database.DatabaseInfo.CreateUsersRelation(_parentUser.Id, selectedUser.ID));
                    if (status)
                        await DisplayAlert("Уведомление", "Cвязь создана.", "ОК");
                    else
                        await DisplayAlert("Ошибка", "Не удалось создать связь.", "ОК");
                }
            }
            else if (relationsCount > 0)
            {
                var result = await DisplayAlert("Удаление связи", "Вы действительно хотите удалить связь с выбранным профилем?", "ДА", "НЕТ");
                if (result)
                {
                    var status = await Task.Run(() => Database.DatabaseInfo.DeleteUsersRelation(_parentUser.Id, selectedUser.ID));
                    if (status)
                        await DisplayAlert("Уведомление", "Cвязь удалена.", "ОК");
                    else
                        await DisplayAlert("Ошибка", "Не удалось удалить связь.", "ОК");
                }
            }
        }
        private void FilterItems(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            var filter = entry.Text;
            if (string.IsNullOrEmpty(filter))
                UserCollectionView.ItemsSource = new ObservableCollection<BriefInfo>(_usersTemp);
            else
                UserCollectionView.ItemsSource = new ObservableCollection<BriefInfo>(Users.Where((user) => user.Name.ToLower().Contains(filter.ToLower()) || user.ID.ToString() == filter));
        }
        private void ClosePage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}