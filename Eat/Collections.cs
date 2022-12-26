using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin;
using System.Globalization;
using System.Drawing;
using System.ComponentModel;

namespace Eat.CollectionItems
{
    public class Dish
    {
        public int ID { get => _id; }
        public int Count { get => _count; }
        public string Name { get => _name; }
        public string Description { get => _description; }
        public double Cost { get => _cost; }
        public string CostString { get => Cost + " " + "рублей"; }
        public bool IsInSelectedDateMenu { get => _isInSelectedDateMenu; }
        public Color SelectionColor { get => _isInSelectedDateMenu == true ? Color.Green : Color.White; }
        private int _id = 0;
        private string _name = "";
        private string _description = "";
        private double _cost = 0;
        private int _count = 0;
        private bool _isInSelectedDateMenu = false;
        public Dish(string name, string description, double cost)
        {
            _name = name;
            _description = description;
            _cost = cost;
            _count = 0;
        }
        public void SetID(int ID)
        {
            _id = ID;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public void SetDescription(string description)
        {
            _description = description;
        }
        public void SetCost(double cost)
        {
            _cost = cost;
        }
        public void SetAvailabilityStatus(bool status)
        {
            _isInSelectedDateMenu = status;
        }
        public void IncreaseCount(int value)
        {
            _count += value;
        }
        public void RefreshCount()
        {
            _count = 0;
        }
        public Dish Clone()
        {
            var dish = new Dish(this.Name, this.Description, this.Cost);
            dish.SetID(this.ID);
            return dish;
        }
    }
    public class UserMenu
    {
        private int _menuID = 0;
        private bool _isSelected = false;
        private string _name = "";
        private List<int> _dishID;
        public string Name { get => _name; }
        public List<int> DishID { get => _dishID; }
        public int MenuID { get => _menuID; }
        public bool IsSelected { get => _isSelected; }
        public Color SelectionColor { get => _isSelected == true ? Color.Blue: Color.DarkGray; }
        public UserMenu(string name, List<int> dishID, int menuID, bool isSelected)
        {
            _name = name;
            _dishID = dishID;
            _menuID = menuID;
            _isSelected = isSelected;
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
        }
        public void AddDish(int dishId)
        {
            _dishID.Add(dishId);
        }
    }
    public class Role : INotifyPropertyChanged
    {
        private string _name = "Default";
        private int _id = 2;
        private bool _isSelected = false;
        public string Name { get => _name; }
        public int ID { get => _id; }
        public bool IsSelected { get => _isSelected; }
        public Color SelectionColor { get => _isSelected == true ? Color.Blue : Color.DarkGray; }
        public Role(int id, string name, bool isSelected)
        {
            _id = id;
            _name = name;
            _isSelected = isSelected;
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("SelectionColor");
        }
        public void ChangeName(string name)
        {
            _name = name;
            OnPropertyChanged("Name");

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
    public class Grade : INotifyPropertyChanged
    {
        private string _name = "Default";
        private int _id = -1;
        private bool _isSelected = false;
        private List<int> _studentsID;
        private int _teacherID;
        public int TeacherID { get =>_teacherID; }
        public List<int> StudentsID { get => _studentsID; }
        public string Name { get => _name; }
        public int ID { get => _id; }
        public bool IsSelected { get => _isSelected; }
        public Color SelectionColor { get => _isSelected == true ? Color.Blue : Color.DarkGray; }

        public Grade(int id, string name, bool isSelected, List<int> studentsID, int teacherID)
        {
            _id = id;
            _name = name;
            _isSelected = isSelected;
            _studentsID = studentsID;
            _teacherID = teacherID;

        }
        public void ChangeStudentsList(List<int> studentsID)
        {
            _studentsID = studentsID;
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("SelectionColor");
        }
        public void ChangeTeacher(int teacherID)
        {
            _teacherID = teacherID;
            OnPropertyChanged("Teacher");
        }
        public void ChangeName(string name)
        {
            _name = name;
            OnPropertyChanged("Name");

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
    public class BriefInfo : INotifyPropertyChanged
    {
        private string _name = "Default";
        private int _id = -1;
        private bool _isSelected = false;
        public string Name { get => _name; }
        public int ID { get => _id; }
        public bool IsSelected { get => _isSelected; }
        public Color SelectionColor { get => _isSelected == true ? Color.Green : Color.DarkGray; }
        public BriefInfo(string name, int id, bool isSelected)
        {
            _id = id;
            _name = name;
            _isSelected = isSelected;
        }
        public void ChangeName(string name)
        {
            _name = name;
            OnPropertyChanged("Name");
            
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("SelectionColor");
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
    public class Order
    {
        private int _userID = -1;
        private bool _isSelected = false;
        private List<int> _dishID;
        public List<int> DishID { get => _dishID; }
        public int UserID { get => _userID; }
        public bool IsSelected { get => _isSelected; }
        public Color SelectionColor { get => _isSelected == true ? Color.Green : Color.DarkGray; }
        public Order(int userID, List<int> dishID,  bool isSelected)
        {
            _userID = userID;
            _dishID = dishID;
            _isSelected = isSelected;
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
        }
    }
    public class GroupOrder : INotifyPropertyChanged
    {
        private string _groupName;
        private bool _isSelected;
        private List<Order> _orders;
        private List<Dish> _dishList;
        public List<Order> Orders { get => _orders; }
        public List<Dish> DishList { get => _dishList; }
        public bool IsSelected { get => _isSelected; }
        public string GroupName { get => _groupName; }
        public string Info 
        {
            get
            {
                if (_isSelected)
                {
                    var sb = new StringBuilder();
                    DishList.FindAll(x => x.Count > 0).ForEach(x => sb.Append(string.Format("{0}: {1} шт. \n", x.Name, x.Count)));
                    return sb.ToString();
                } 
                else
                    return "↓";
            }
        }
        public Color SelectionColor { get => _isSelected == true ? Color.Black : Color.Black; }
        public GroupOrder(string groupName, List<Order> orders, List<Dish> dishList , bool isSelected)
        {
            _groupName = groupName;
            _orders = orders;
            _isSelected = isSelected;
            _dishList = dishList;
        }
        public void ChangeSelectionStatus(bool selectionStatus)
        {
            _isSelected = selectionStatus;
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("Info");
            OnPropertyChanged("SelectionColor");
        }
        public void AddOrder(Order studentOrder)
        {
            _orders.Add(studentOrder);
        }
        public void ChangeDishList(List<Dish> newDishList)
        {
            _dishList = newDishList;
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
