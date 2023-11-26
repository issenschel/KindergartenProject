using KindergartenProject.Infrastructure.Consts;
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
using System.Windows.Shapes;

namespace KindergartenProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {
        public GuestWindow()
        {
            InitializeComponent();

            UserTextBlock.Text = $"Пользователь: {Application.Current.Resources[UserInfoConsts.UserName]}";
            RoleTextBlock.Text = $"Роль: {Application.Current.Resources[UserInfoConsts.RoleName]}";

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Remove(UserInfoConsts.UserName);
            Application.Current.Resources.Remove(UserInfoConsts.RoleId);
            Application.Current.Resources.Remove(UserInfoConsts.RoleName);
            1
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        private void GroupsButton_Click(object sender, RoutedEventArgs e)
        {
            GroupsWindow groupsWindow = new GroupsWindow();
            groupsWindow.Show();
            Close();
        }

        private void MealScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MealScheduleWindow mealScheduleWindow = new MealScheduleWindow();
            mealScheduleWindow.Show();
            Close();
        }

        private void ModeOfTheDayButton_Click(object sender, RoutedEventArgs e)
        {
            ModeOfTheDayWindow modeOfTheDayWindow = new ModeOfTheDayWindow();
            modeOfTheDayWindow.Show();
            Close();
        }
    }
}
