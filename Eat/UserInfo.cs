using System;
using System.Collections.Generic;
using System.Text;

namespace Eat.UserInfo
{
    public class UserInfo
    {
        private string[] _roles = {""};
        private string _name = "";
        private string _surname = "";
        private string _patronymic = "";
        private double _balance = 0;
        private int _id = 0;
        private int[] _relatedID = new int[]{ };
        private DateTime _selectedDate;
        private int _selectedMenu = 1;
        private int _selectedRole = 2;
        public int SelectedRole { get => _selectedRole; }
        public int[] RelatedID { get => _relatedID; }
        public string[] Roles { get => _roles; }
        public string Name { get => _name; }
        public string Surname { get => _surname; }
        public string Patronymic { get => _patronymic; }
        public double Balance { get => _balance; }
        public string NameAndSurname{ get => Name + " " + Surname; }
        public DateTime SelectedDate { get => _selectedDate; }
        public int Id { get => _id; }
        public int SelectedMenu { get => _selectedMenu; }
        public UserInfo(string name, string surname, string patronymic, string[] roles, double balance, int id, int[] relatedID, DateTime selectedDate, int selectedMenu, int selectedRole)
        {
            _name = name; 
            _surname = surname;
            _patronymic = patronymic;
            _roles = roles;
            _balance = balance;
            _id = id;
            _relatedID = relatedID;
            _selectedDate = selectedDate;
            _selectedMenu = selectedMenu;
            _selectedRole = selectedRole;
        }
        public void ChangeSelectedDate(DateTime newDate)
        {
            _selectedDate = newDate;
        }
        public void ChangeSelectedMenu(int selectedMenu)
        {
            _selectedMenu = selectedMenu;
        }
        public void ChangeSelectedRole(int selectedRole)
        {
            _selectedRole = selectedRole;
        }
    }
}
