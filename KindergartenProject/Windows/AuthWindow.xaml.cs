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
    public partial class AuthWindow : Window
    {
        private UserRepository _repository;

        // Инициализирует новый экземпляр класса AuthWindow и создает объект UserRepository для работы с пользователями.
        public AuthWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
        }

        // Обработчик события при нажатии кнопки аутентификации.
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяет введенные учетные данные пользователя с помощью метода Login класса UserRepository.
            var authenticatedUser = _repository.Login(LoginTextBox.Text, PasswordBox.Password);

            // Если аутентификация успешна, сохраняет информацию о пользователе в ресурсы текущего приложения и открывает главное окно.
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
            // В противном случае выводит сообщение о неверном логине или пароле.
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        // Обработчик события при нажатии кнопки входа в систему как гость.
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливает информацию о пользователе в ресурсы текущего приложения и открывает окно гостя.
            Application.Current.Resources[UserInfoConsts.RoleId] = (long)1;
            Application.Current.Resources[UserInfoConsts.RoleName] = "Гость";
            Application.Current.Resources[UserInfoConsts.UserName] = "Гость";

            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            Close();
        }
    }
}
