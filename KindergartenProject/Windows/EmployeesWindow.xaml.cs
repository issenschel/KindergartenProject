using KindergartenProject.Infrastructure.Consts;
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
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private EmployeeRepository _repository;

        public EmployeesWindow()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
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
            if (EmployeeDataGrid.SelectedItem != null)
                return;
            var exampleCard = new EmployeeСardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            EmployeeDataGrid.ItemsSource = _repository.GetList();
        }

        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                return;
            var exampleCard = new EmployeeСardWindow(EmployeeDataGrid.SelectedItem as EmployeeViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                MessageBox.Show("Ничего не выбрано");
            var item = EmployeeDataGrid.SelectedItem as EmployeeViewModel;
            if (item == null)
                MessageBox.Show("Не удалось получить данные");
            else
            {
                _repository.Delete(item.ID);
                UpdateGrid();
                MessageBox.Show("Удаление успешно");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                return;
            var exampleCard = new EmployeeСardWindow(EmployeeDataGrid.SelectedItem as EmployeeViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

    }
}
