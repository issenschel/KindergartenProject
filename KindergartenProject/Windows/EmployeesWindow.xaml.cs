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
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private EmployeeRepository _repository;

        public EmployeesWindow()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            EmployeeDataGrid.ItemsSource = _repository.GetList();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeСardWindow employeeСardWindow = new EmployeeСardWindow();
            employeeСardWindow.Show();
            Close();
        }
    }
}
