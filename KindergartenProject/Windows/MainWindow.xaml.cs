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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];
        public MainWindow()
        {
            InitializeComponent();
            UserTextBlock.Text = $"Пользователь: {Application.Current.Resources[UserInfoConsts.UserName]}";
            RoleTextBlock.Text = $"Роль: {Application.Current.Resources[UserInfoConsts.RoleName]}";
            GrantAccessByRole();

        }

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

        private void KidsButton_Click(object sender, RoutedEventArgs e)
        {
            KidsWindow kidsWindow = new KidsWindow();
            kidsWindow.Show();
            Close();
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupsWindow groupsWindow = new GroupsWindow();
            groupsWindow.Show();
            Close();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
            Close();
        }

        private void ModeOfTheDayButton_Click(object sender, RoutedEventArgs e)
        {
            ModeOfTheDayWindow modeOfTheDayWindow = new ModeOfTheDayWindow();
            modeOfTheDayWindow.Show();
            Close();
        }

        private void MealScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MealScheduleWindow mealScheduleWindow = new MealScheduleWindow();
            mealScheduleWindow.Show();
            Close();
        }

        private void GrantAccessByRole()
        {
            if (roleId == 2)
            {
                EmployeeButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
