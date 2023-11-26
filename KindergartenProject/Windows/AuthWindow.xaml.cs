using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private UserRepository _repository;
        public AuthWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {

            var authenticatedUser = _repository.Login(LoginTextBox.Text, PasswordBox.Password);

            if (authenticatedUser != null)
            {
                Application.Current.Resources[UserInfoConsts.UserId] = authenticatedUser.ID;
                Application.Current.Resources[UserInfoConsts.UserName] = authenticatedUser.Login;
                Application.Current.Resources[UserInfoConsts.RoleId] = authenticatedUser.RoleId;
                Application.Current.Resources[UserInfoConsts.RoleName] = authenticatedUser.RoleName;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }


        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources[UserInfoConsts.RoleId] = (long)1;
            Application.Current.Resources[UserInfoConsts.RoleName] = "Гость";
            Application.Current.Resources[UserInfoConsts.UserName] = "Гость";

            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            Close();
        }
    }
}
