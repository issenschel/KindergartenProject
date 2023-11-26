using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        private GroupRepository _repository;
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];

        public GroupsWindow()
        {
            InitializeComponent();
            _repository = new GroupRepository();
            GrantAccessByRole();
            UpdateGrid();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (roleId == 1)
            {
                GuestWindow guestWindow = new GuestWindow();
                guestWindow.Show();
                Close();
            }

            else 
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem != null)
                return;
            var exampleCard = new GroupCardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            GroupsDataGrid.ItemsSource = _repository.GetList();
        }

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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsDataGrid.SelectedItem == null)
                return;
            var exampleCard = new GroupCardWindow(GroupsDataGrid.SelectedItem as GroupViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

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

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GrantAccessByRole()
        {
            if (roleId == 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
            }

            else if (roleId == 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
