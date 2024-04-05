using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.Report;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        private GroupRepository _repository;
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];

        //Инициализация окна
        public GroupsWindow()
        {
            InitializeComponent();
            _repository = new GroupRepository();
            GrantAccessByRole();
            UpdateGrid();
        }

        //Выход в меню
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            //Если гость то выход в меню гостя
            if (roleId == 1)
            {
                GuestWindow guestWindow = new GuestWindow();
                guestWindow.Show();
                Close();
            }
            // Если нет то выход в обычное меню
            else 
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
        //Открытие карточки для добавления
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem != null)
                return;
            var exampleCard = new GroupCardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        //Обновление таблицы
        private void UpdateGrid()
        {
            GroupsDataGrid.ItemsSource = _repository.GetList();
        }
        //Открытие карточки для редактирования по двойному клику
        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (roleId == 3)
            {
                if (GroupsDataGrid.SelectedItem == null)
                    return;
                var exampleCard = new GroupCardWindow(GroupsDataGrid.SelectedItem as GroupViewModel);
                exampleCard.ShowDialog();
                UpdateGrid();
            }
        }
        //Выбор поля и открытие его карточки по нажатию кнопки добавить
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem == null)
                return;
            var exampleCard = new GroupCardWindow(GroupsDataGrid.SelectedItem as GroupViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }
        //Удаление поля
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem == null)
                MessageBox.Show("Ничего не выбрано");
            var item = GroupsDataGrid.SelectedItem as GroupViewModel;
            if (item == null)
                MessageBox.Show("Не удалось получить данные");
            else
            {
                _repository.Delete(item.ID);
                UpdateGrid();
                MessageBox.Show("Удаление успешно");
            }
        }

        //Выгрузка в Excel
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(GroupsDataGrid.ItemsSource as List<GroupViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Группы_{DateTime.Now.ToShortDateString()}.xlsx");
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }
                MessageBox.Show("Выгрузка успешна");
            }
            catch 
            {
                MessageBox.Show("Выгрузка неуспешна");
            }
        }

        //Скрытие интерфейса в зависимости от роли
        private void GrantAccessByRole()
        {
            if (roleId == 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                UploadButton.Visibility = Visibility.Collapsed;
            }

            else if (roleId == 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                UploadButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
