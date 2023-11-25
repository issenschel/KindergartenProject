using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.ViewModels;
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
    /// Логика взаимодействия для GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        private GroupRepository _repository;
        public GroupsWindow()
        {
            InitializeComponent();
            _repository = new GroupRepository();
            UpdateGrid();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
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
            if (GroupsDataGrid.SelectedItem == null)
                return;
            var exampleCard = new GroupCardWindow(GroupsDataGrid.SelectedItem as GroupViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
