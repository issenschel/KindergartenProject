using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KindergartenProject
{
    public partial class MainWindow : Window
    {
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];
        //Инициализцая окна
        public MainWindow()
        {
            InitializeComponent();
            UserTextBlock.Text = $"Пользователь: {Application.Current.Resources[UserInfoConsts.UserName]}";
            RoleTextBlock.Text = $"Роль: {Application.Current.Resources[UserInfoConsts.RoleName]}";
            GrantAccessByRole();

        }
        //Выход к окну авторизации и удаление ключей 
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Remove(UserInfoConsts.UserId);
            Application.Current.Resources.Remove(UserInfoConsts.UserName);
            Application.Current.Resources.Remove(UserInfoConsts.RoleId);
            Application.Current.Resources.Remove(UserInfoConsts.RoleName);

            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
        //Открытие окна с категорией дети
        private void KidsButton_Click(object sender, RoutedEventArgs e)
        {
            KidsWindow kidsWindow = new KidsWindow();
            kidsWindow.Show();
            Close();
        }
        //Открытие окна с категорией группы
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupsWindow groupsWindow = new GroupsWindow();
            groupsWindow.Show();
            Close();
        }
        //Открытие окна с категорией сотрудники
        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
            Close();
        }
        //Открытие окна с категорией расписание
        private void ModeOfTheDayButton_Click(object sender, RoutedEventArgs e)
        {
            ModeOfTheDayWindow modeOfTheDayWindow = new ModeOfTheDayWindow();
            modeOfTheDayWindow.Show();
            Close();
        }
        //Открытие окна с категорией питание
        private void MealScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MealScheduleWindow mealScheduleWindow = new MealScheduleWindow();
            mealScheduleWindow.Show();
            Close();
        }
        //Если роль сотрудник то категория сотрудники не отображаются 
        private void GrantAccessByRole()
        {
            if (roleId == 2)
            {
                EmployeeButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
