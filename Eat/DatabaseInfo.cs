using System;
using System.Collections.Generic;
using Eat.UserInfo;
using Eat.CollectionItems;
using Xamarin.Forms;
using Xamarin.Essentials;
using Npgsql;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Eat.Database
{
    public static class DatabaseInfo
    {
        private static string _server = "";
        private static string _databaseName = "";
        private static string _port = "";
        private static string _userId = "";
        private static string _password = "";
        public static string ConnectionString = string.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4}", _server, _port, _userId, _password, _databaseName);
        public static UserInfo.UserInfo GetUserInfo(int userId)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            string name = "";
            string surname = "";
            string patronymic = "";
            double balance = 0;
            string[] roles = new string[] { "" };
            int[] relatedID = new int[] { };
            DateTime selectedDate = DateTime.Now;
            int selectedMenu = 0;
            int selectedRole = 2;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT name,surname,patronymic,id FROM user_info WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    name = reader[0].ToString();
                    surname = reader[1].ToString();
                    patronymic = reader[2].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT balance FROM balance WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    Double.TryParse(reader[0].ToString(), out balance);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT role_id FROM user_roles WHERE user_id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                var list = new List<string>();
                while (reader.Read() != false)
                {
                    list.Add(reader[0].ToString());
                    Console.WriteLine(reader[0].ToString());
                }
                roles = list.ToArray();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT related_id FROM related_id WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                var list = new List<int>();
                while (reader.Read() != false)
                {
                    list.Add(Int32.Parse(reader[0].ToString()));
                    Console.WriteLine(reader[0].ToString());
                }
                relatedID = list.ToArray();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT selected_date FROM user_info WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    DateTime.TryParse(reader[0].ToString(), out selectedDate);
                    Console.WriteLine(reader[0].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT selected_menu FROM user_info WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    Int32.TryParse(reader[0].ToString(), out selectedMenu);
                    Console.WriteLine(reader[0].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT selected_role FROM user_info WHERE id={0};", userId.ToString());
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    Int32.TryParse(reader[0].ToString(), out selectedRole);
                    Console.WriteLine(reader[0].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return new UserInfo.UserInfo(name, surname, patronymic, roles, balance, userId, relatedID, selectedDate, selectedMenu, selectedRole);
        }
        public static void UpdateUserInfo(UserInfo.UserInfo userInfo)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE user_info SET selected_date='{0}', selected_menu='{1}', selected_role='{2}' WHERE id={3};", userInfo.SelectedDate.ToString("yyyy-MM-dd"), userInfo.SelectedMenu, userInfo.SelectedRole, userInfo.Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
        }
        public static string GetKeyValueByParameter(string table, string key, string parameter, string parameterValue)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            string value = "-1";
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT {0} FROM {1} WHERE {2}={3};", key, table, parameter, parameterValue);
                Console.WriteLine(command.CommandText);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    value = reader[0].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return value;
        }

        public static string GetQuantityByParameter(string table, string parameter, string parameterValue)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            string value = "-1";
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}={2}", table, parameter, parameterValue);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    value = reader[0].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return value;
        }
        public static ObservableCollection<Dish> GetDishList(int categoryID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            ObservableCollection<Dish> DishList = new ObservableCollection<Dish>();
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id,name,description,cost FROM dish_list WHERE category_id={0};", categoryID);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var id = Int32.Parse(reader[0].ToString());
                    var name = reader[1].ToString();
                    var description = reader[2].ToString();
                    var cost = Double.Parse(reader[3].ToString());
                    var dishInfo = new Dish(name, description, cost);
                    dishInfo.SetID(id);
                    DishList.Add(dishInfo);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                return null;
                Console.WriteLine(ex.ToString());
            }
            return DishList;
        }
        public static ObservableCollection<Dish> GetTodayDishList(int categoryID, DateTime selectedDate)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            ObservableCollection<Dish> DishList = GetDishList(categoryID);
            ObservableCollection<Dish> TodayDishList = new ObservableCollection<Dish>();
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT dish_id FROM date_menu WHERE category_id={0} AND date='{1}';", categoryID, selectedDate.Date.ToString("yyyy-MM-dd"));
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var id = Int32.Parse(reader[0].ToString());
                    var dishList = new List<Dish>(DishList);
                    var dish = dishList.Find(x => x.ID == id);
                    if (dish != null)
                    {
                        TodayDishList.Add(dish);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return TodayDishList;
        }
        public static void UpdateDish(Dish dish)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE dish_list SET name='{0}',description='{1}',cost={2} WHERE id={3};", dish.Name, dish.Description, dish.Cost.ToString(), dish.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }

        }
        public static void DeleteDish(Dish dish)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM dish_list WHERE id={0};", dish.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }

        }
        public static void CreateDish(Dish dish, int categoryID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO dish_list (category_id,name,description,cost) VALUES({0},'{1}','{2}',{3})", categoryID, dish.Name, dish.Description, dish.Cost.ToString());
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }

        }
        public static bool GetAvailabilityStatus(DateTime selectedDate, Dish dish)
        {
            bool result = false;
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT * FROM date_menu WHERE dish_id={0} and date='{1}'", dish.ID, selectedDate.Date.ToString("yyyy-MM-dd"));
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    result = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        public static void SetAvailabilityStatus(DateTime selectedDate, Dish dish, int categoryID, bool status)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                if (status)
                    command.CommandText = string.Format("INSERT INTO date_menu (dish_id,category_id,date) VALUES({0},{1},'{2}')", dish.ID, categoryID, selectedDate.Date.ToString("yyyy-MM-dd"));
                else
                    command.CommandText = string.Format("DELETE FROM date_menu WHERE dish_id = {0} AND date ='{1}'", dish.ID, selectedDate.Date.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.ToString());
            }
        }
        public static string GetNotifications()
        {
            string notification = "Нет уведомлений";
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT notification FROM notifications");
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    notification = reader[0].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                notification = "-1";
                connection.Close();
            }
            return notification;
        }
        public static ObservableCollection<UserMenu> GetUserMenus(UserInfo.UserInfo userInfo)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            List<UserMenu> UserMenus = new List<UserMenu>();
            int menuId = 0;
            int dishId = 0;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT menu_id,menu_name FROM user_menus WHERE user_id={0};", userInfo.Id);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var menu_id = Int32.Parse(reader[0].ToString());
                    var menu_name = reader[1].ToString();
                    var userMenu = new UserMenu(menu_name, new List<int>(), menu_id, false);
                    if (userInfo.SelectedMenu == userMenu.MenuID)
                        userMenu.ChangeSelectionStatus(true);
                    UserMenus.Add(userMenu);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT menu_id,dish_id FROM user_menus_dishes WHERE user_id={0};", userInfo.Id);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    Int32.TryParse(reader[0].ToString(), out menuId);
                    Int32.TryParse(reader[1].ToString(), out dishId);
                    UserMenus.Find(x => x.MenuID == menuId).AddDish(dishId);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return new ObservableCollection<UserMenu>(UserMenus);
        }
        public static void CreateUserMenu(UserInfo.UserInfo userInfo, string menuName)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var userMenus = new List<UserMenu>(GetUserMenus(userInfo));
            int newId = 1;
            while (userMenus.Find(x => x.MenuID == newId) != null)
            {
                newId += 1;
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO user_menus (user_id,menu_id,menu_name) VALUES({0},{1},'{2}')", userInfo.Id, newId, menuName);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            userInfo.ChangeSelectedMenu(newId);
            UpdateUserInfo(userInfo);
        }
        public static void DeleteUserMenu(UserInfo.UserInfo userInfo, UserMenu userMenu)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM user_menus WHERE user_id={0} AND menu_id={1}", userInfo.Id, userMenu.MenuID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM user_menus_dishes WHERE user_id={0} AND menu_id={1}", userInfo.Id, userMenu.MenuID);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch {
                    connection.Close();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            try
            {
                if (userInfo.SelectedMenu == userMenu.MenuID)
                {
                    var menus = GetUserMenus(userInfo);
                    userInfo.ChangeSelectedMenu(menus[0].MenuID);
                    UpdateUserInfo(userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }

        }
        public static void AddDishToMenu(UserInfo.UserInfo userInfo, UserMenu userMenu, Dish dish)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO user_menus_dishes (user_id,menu_id,dish_id) VALUES({0},{1},{2})", userInfo.Id, userMenu.MenuID, dish.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
        }
        public static void DeleteDishFromMenu(UserInfo.UserInfo userInfo, UserMenu userMenu, Dish dish)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM user_menus_dishes WHERE user_id={0} AND menu_id={1} AND dish_id={2}", userInfo.Id, userMenu.MenuID, dish.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
        }
        public static bool IsDishInUserMenu(UserInfo.UserInfo userInfo, UserMenu userMenu, Dish dish)
        {
            int count = 0;
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT COUNT(*) FROM user_menus_dishes WHERE user_id={0} AND menu_id={1} AND dish_id={2}", userInfo.Id, userMenu.MenuID, dish.ID);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    Int32.TryParse(reader[0].ToString(), out count);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            if (count == 0)
                return false;
            else
                return true;
        }
        public static double GetUserMenuCost(UserInfo.UserInfo userInfo)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            double finalCost = 0;
            var dish_id_list = new List<int>(new List<UserMenu>(GetUserMenus(userInfo)).Find(x => x.MenuID == userInfo.SelectedMenu).DishID);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id,cost FROM dish_list");
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var id = Int32.Parse(reader[0].ToString());
                    var cost = Double.Parse(reader[1].ToString());
                    if (dish_id_list.Contains(id))
                        finalCost += cost;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return finalCost;
        }
        public static bool GetTodayAndUserMenuEquality(UserMenu userMenu, DateTime selectedDate)
        {
            bool result = false;
            int differences = 0;
            var finalList = new List<Dish>();
            var firstDishIdList = new List<Dish>(GetTodayDishList(1, selectedDate));
            var secondDishIdList = new List<Dish>(GetTodayDishList(2, selectedDate));
            var thirdDishIdList = new List<Dish>(GetTodayDishList(3, selectedDate));
            if (firstDishIdList != null && secondDishIdList != null && thirdDishIdList != null)
            {
                finalList.AddRange(firstDishIdList);
                finalList.AddRange(secondDishIdList);
                finalList.AddRange(thirdDishIdList);
            }
            foreach (var dishId in userMenu.DishID)
            {
                if (finalList.Find(x => x.ID == dishId) == null)
                    differences += 1;
            }
            if (differences == 0)
                result = true;
            return result;
        }
        public static bool UpdateUserBalance(UserInfo.UserInfo userInfo, double value)
        {
            int affectedRows = 0;
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE balance SET balance='{0}' WHERE id={1};", value, userInfo.Id);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return affectedRows == 0 ? false : true;
        }
        public static double GetUserBalance(UserInfo.UserInfo userInfo)
        {
            double balance = -1;
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT balance FROM balance WHERE id={0};", userInfo.Id);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                {
                    balance = Double.Parse(reader[0].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
            }
            return balance;
        }

        public static bool MakeOrder(UserInfo.UserInfo userInfo, UserMenu userMenu)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var status = userInfo.SelectedRole == 2 ? false : true;
            var ordersCount = Int32.Parse(Database.DatabaseInfo.GetQuantityByParameter("orders", string.Format("user_id = {0} AND order_date", userInfo.Id), "'" + userInfo.SelectedDate.Date.ToString("MM.dd.yyyy") + "'"));
            var userMenuCost = GetUserMenuCost(userInfo);
            var userBalance = GetUserBalance(userInfo);
            if (ordersCount == 0 && userMenuCost != 0 && userBalance != -1)
            {
                if (userBalance < userMenuCost)
                    return false;
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("INSERT INTO orders (user_id,status,order_date,cost) VALUES({0},'{1}','{2}','{3}')", userInfo.Id, status, userInfo.SelectedDate.ToString("yyyy-MM-dd"), userMenuCost);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO order_details (user_id, dish_id, order_date, approved) VALUES");
                    for (int i = 0; i < userMenu.DishID.Count; i++)
                    {
                        sb.Append(string.Format("({0},'{1}','{2}','{3}')", userInfo.Id, userMenu.DishID[i], userInfo.SelectedDate.ToString("yyyy-MM-dd"), false));
                        if (i != userMenu.DishID.Count - 1)
                            sb.Append(", ");
                    }
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                if (UpdateUserBalance(userInfo, userBalance - userMenuCost))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public static bool CancelOrder(UserInfo.UserInfo userInfo)
        {
            var balance = GetUserBalance(userInfo);
            double orderCost = -1;
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            if (balance != -1)
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("SELECT cost FROM orders WHERE user_id={0} AND order_date='{1}'", userInfo.Id, userInfo.SelectedDate.ToString("yyyy-MM-dd"));
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() != false)
                    {
                        orderCost = Double.Parse(reader[0].ToString());
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Found exception in 26");
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                try
                {
                    bool approved = false;
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("SELECT approved FROM order_details WHERE user_id={0} AND order_date='{1}'", userInfo.Id, userInfo.SelectedDate.ToString("yyyy-MM-dd"));
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() != false)
                    {
                        approved = Boolean.Parse(reader[0].ToString());
                    }
                    if (approved)
                    {
                        connection.Close();
                        return false;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("DELETE FROM orders WHERE user_id={0} AND order_date='{1}'", userInfo.Id, userInfo.SelectedDate.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("DELETE FROM order_details WHERE user_id={0} AND order_date='{1}'", userInfo.Id, userInfo.SelectedDate.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return false;
                }
                if (orderCost != -1)
                    return UpdateUserBalance(userInfo, balance + orderCost);
                else
                    return false;
            }
            else
                return false;
        }
        public static List<Grade> GetTeacherGradeList(UserInfo.UserInfo userInfo)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var GradeList = new List<Grade>();
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id,name,teacher_id FROM grades WHERE teacher_id={0}", userInfo.Id);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var id = Int32.Parse(reader[0].ToString());
                    var name = reader[1].ToString();
                    var teacher_id = Int32.Parse(reader[2].ToString());
                    GradeList.Add(new Grade(id, name, false, new List<int>(), teacher_id));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<Grade>();
            }
            try
            {
                connection.Open();
                foreach (var grade in GradeList)
                {
                    var studentsID = new List<int>();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("SELECT id FROM students WHERE grade_id={0}", grade.ID);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read() != false)
                            {
                                var id = Int32.Parse(reader[0].ToString());
                                studentsID.Add(id);
                            }
                            grade.ChangeStudentsList(studentsID);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<Grade>();
            }
            return GradeList;
        }
        public static List<Grade> GetAllGradeList()
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var GradeList = new List<Grade>();
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id,name,teacher_id FROM grades");
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read() != false)
                {
                    var id = Int32.Parse(reader[0].ToString());
                    var name = reader[1].ToString();
                    var teacher_id = Int32.Parse(reader[2].ToString());
                    GradeList.Add(new Grade(id, name, false, new List<int>(), teacher_id));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<Grade>();
            }
            try
            {
                connection.Open();
                foreach (var grade in GradeList)
                {
                    var studentsID = new List<int>();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("SELECT id FROM students WHERE grade_id={0}", grade.ID);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read() != false)
                            {
                                var id = Int32.Parse(reader[0].ToString());
                                studentsID.Add(id);
                            }
                            grade.ChangeStudentsList(studentsID);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<Grade>();
            }
            return GradeList;
        }
        public static bool TagStudent(int userID, DateTime orderDate, bool status)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var orderStockStatus = Int32.Parse(Database.DatabaseInfo.GetQuantityByParameter("orders", string.Format("user_id={0} AND order_date", userID), string.Format("'{0}'", orderDate.ToString("yyyy-MM-dd")))) > 0 ? true : false;
            if (!orderStockStatus)
            {
                return false;
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE orders SET status = '{0}' WHERE user_id={1} AND order_date ='{2}'", status.ToString(), userID, orderDate.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool ChangeOrdersStatus(DateTime orderDate, bool status)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var useriD = new List<int>();
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT user_id FROM orders WHERE status='True' AND order_date ='{0}'", orderDate.ToString("yyyy-MM-dd"));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() != false)
                        {
                            var id = Int32.Parse(reader[0].ToString());
                            useriD.Add(id);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            try
            {
                connection.Open();
                foreach (var id in useriD)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("UPDATE order_details SET approved = '{0}' WHERE order_date ='{1}' AND user_id ='{2}'", status.ToString(), orderDate.ToString("yyyy-MM-dd"), id);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool СlearUserOrders(DateTime orderDate)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM order_details WHERE order_date ='{0}'", orderDate.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM orders WHERE order_date ='{0}'", orderDate.ToString("yyyy-MM-dd"));
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static List<BriefInfo> GetTeacherList()
        {
            var teacherList = new List<BriefInfo>();
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT user_id FROM user_roles WHERE role_id = '{0}'", 1);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() != false)
                    {
                        var id = Int32.Parse(reader[0].ToString());
                        var briefUserInfo = new BriefInfo("Default", id, false);
                        teacherList.Add(briefUserInfo);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<BriefInfo>();
            }
            foreach (var teacher in teacherList)
            {
                var userInfo = GetUserInfo(teacher.ID);
                teacher.ChangeName(string.Format("{0} {1}", userInfo.Surname, userInfo.Name));
            }
            return teacherList;
        }
        public static bool ChangeGradeName(Grade grade, string name)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            var affectedRows = 0;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE grades SET name='{0}' WHERE id = '{1}'", name, grade.ID);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return affectedRows == -1 ? false : true;
        }
        public static bool ChangeGradeTeacher(Grade grade, int teacherId)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE grades SET teacher_id='{0}' WHERE id = '{1}'", teacherId, grade.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool CreateGrade(string name, int teacherId)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO grades (name,teacher_id) VALUES('{0}','{1}')", name, teacherId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool DeleteGrade(Grade grade)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM grades WHERE id='{0}'", grade.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool DeleteStudentFromGrade(int studentId)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM students WHERE id='{0}'", studentId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool AddStudentToGrade(int studentId, Grade grade)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO students (id,grade_id) VALUES('{0}','{1}')", studentId, grade.ID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static List<BriefInfo> GetAllStudentsList()
        {
            var studentsList = new List<BriefInfo>();
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT user_id FROM user_roles WHERE role_id = '{0}'", 2);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() != false)
                    {
                        var id = Int32.Parse(reader[0].ToString());
                        var briefUserInfo = new BriefInfo("Default", id, false);
                        studentsList.Add(briefUserInfo);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<BriefInfo>();
            }
            foreach (var student in studentsList)
            {
                var userInfo = GetUserInfo(student.ID);
                student.ChangeName(string.Format("{0} {1} {2}", userInfo.Surname, userInfo.Name, userInfo.Patronymic));
            }
            return studentsList;
        }
        public static List<BriefInfo> GetAllUsersList()
        {
            var users = new List<BriefInfo>();
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id,name,surname,patronymic FROM user_info");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() != false)
                    {
                        var id = Int32.Parse(reader[0].ToString());
                        var name = reader[1].ToString();
                        var surname = reader[2].ToString();
                        var patronymic = reader[3].ToString();
                        var briefUserInfo = new BriefInfo(string.Format("{0} {1} {2}", surname, name, patronymic), id, false);
                        users.Add(briefUserInfo);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new List<BriefInfo>();
            }
            return users;
        }
        public static bool CreateUser(string login, string password, string name, string surname, string patronymic)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            bool status = true;
            var id = -1;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO user_info (login,password,name,surname,patronymic,selected_date,selected_menu,selected_role) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", login, password, name, surname, patronymic, DateTime.Now.ToString("yyyy-MM-dd"), 1, 2);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                status = false;
            }
            if (status)
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("SELECT id FROM user_info WHERE login='{0}'", login);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() != false)
                        {
                            id = Int32.Parse(reader[0].ToString());
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    status = false;
                }
            }
            if (status && id != -1)
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("INSERT INTO balance (id,balance) VALUES('{0}','{1}')", id, 0);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    status = false;
                }
            }
            if (status && id != -1)
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("INSERT INTO user_menus (user_id,menu_id,menu_name) VALUES('{0}','{1}','{2}')", id, 1, "Основное меню");
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    status = false;
                }
            }
            if (status && id != -1)
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand command = connection.CreateCommand();
                    command.CommandText = string.Format("INSERT INTO user_roles (user_id,role_id) VALUES('{0}','{1}')", id, 2, "Основное меню");
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    status = false;
                }
            }
            return status;
        }
        public static bool DeleteUser(int userID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            int affectedRows = 0;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM user_info WHERE id='{0}'", userID);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return affectedRows == 0 ? false : true;
        }
        public static bool UpdateUser(int userID, string login, string password, string name, string surname, string patronymic)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            int affectedRows = 0;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("UPDATE user_info SET login='{0}',password='{1}',name='{2}',surname='{3}',patronymic='{4}' WHERE id='{5}'", login, password, name, surname, patronymic, userID);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return affectedRows == 0 ? false : true;
        }
        public static string GetUserLogin(int userID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            string login = "";
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT login FROM user_info WHERE id='{0}'", userID);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read() != false)
                    {
                        login = reader[0].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                login = "";
            }
            return login;
        }
        public static string GetUserPassword(int userID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            string password = "";
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT password FROM user_info WHERE id='{0}'", userID);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read() != false)
                    {
                        password = reader[0].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                password = "";
            }
            return password;
        }
        public static bool AddRoleToUser(int userID, int roleID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO user_roles (user_id,role_id) VALUES('{0}','{1}')", userID, roleID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool DeleteRoleFromUser(int userID, int roleID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            int affectedRows = 0;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM user_roles WHERE user_id='{0}' AND role_id='{1}'", userID, roleID);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return affectedRows == 0 ? false : true;
        }
        public static List<int> GetStaffIdList()
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            List<int> staffId = new List<int>();
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT user_id FROM user_roles WHERE role_id='1' OR role_id='4' OR role_id='5'");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read() != false)
                    {
                        var id = Int32.Parse(reader[0].ToString());
                        if (staffId.Contains(id) == false)
                            staffId.Add(id);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return null;
            }
            return staffId;
        }
        public static async Task<Order> GetUserOrder(int userID, DateTime selectedDate)
        {
            Order order = new Order(userID, new List<int>(), false);
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                await connection.OpenAsync();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT dish_id FROM order_details WHERE user_id='{0}' AND order_date='{1}' AND approved='True'", userID, selectedDate.ToString("yyyy-MM-dd"));
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync() != false)
                    {
                        int dishID = Int32.Parse(reader[0].ToString());
                        order.DishID.Add(dishID);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return new Order(-1, new List<int>(), false);
            }
            return order;
        }
        public static bool CreateUsersRelation(int parentUserID, int childUserID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("INSERT INTO related_id (id,related_id) VALUES('{0}','{1}')", parentUserID, childUserID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool DeleteUsersRelation(int parentUserID, int childUserID)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("DELETE FROM related_id WHERE id='{0}' AND related_id='{1}'", parentUserID, childUserID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static bool CreateNotification(string notification)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM notifications");
                    command.ExecuteNonQuery();
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("INSERT INTO notifications VALUES('{0}')", notification);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                connection.Close();
                return false;
            }
            return true;
        }
        public static int LogIn(string login, string password)
        {
            NpgsqlConnection connection = new NpgsqlConnection(Eat.Database.DatabaseInfo.ConnectionString);
            int userId = -1;
            try
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = string.Format("SELECT id FROM user_info WHERE login='{0}' and password='{1}';", login, password);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read() == false)
                {
                    connection.Close();
                    return -1;
                }
                else
                {
                    userId = Int32.Parse(reader[0].ToString());
                    connection.Close();
                }
                connection.Close();
                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
                connection.Close();
            }
        }
    }
}


