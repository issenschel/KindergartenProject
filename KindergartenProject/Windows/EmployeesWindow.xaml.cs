using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.QR.Infrastructure.QR;
using KindergartenProject.Infrastructure.Report;
using KindergartenProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
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

    public partial class EmployeesWindow : Window
    {
        private EmployeeRepository _repository;

        //Инициализация окна
        public EmployeesWindow()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            UpdateGrid();
        }


        //Выход в меню
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        //Открытие карточки для добавления
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem != null)
                return;
            var exampleCard = new EmployeeСardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        //Обновление таблицы
        private void UpdateGrid()
        {
            EmployeeDataGrid.ItemsSource = _repository.GetList();
        }

        //Открытие карточки для редактирования по двойному клику
        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                return;
            var exampleCard = new EmployeeСardWindow(EmployeeDataGrid.SelectedItem as EmployeeViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        //Удаление поля
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

        //Выбор поля и открытие его карточки по нажатию кнопки добавить
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                return;
            var exampleCard = new EmployeeСardWindow(EmployeeDataGrid.SelectedItem as EmployeeViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        //Генерация QR-кода
        private void GenerateQRButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
                return;

            var qrManager = new QRManager();
            var qrCode = qrManager.Generate(EmployeeDataGrid.SelectedItem as EmployeeViewModel);

            // Создание нового окна
            var qrWindow = new Window()
            {
                Title = "QR Code",
                Width = 400,
                Height = 400,
              
                Content = new Image()
                {
                    Source = qrCode
                },
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Отображение окна
            qrWindow.ShowDialog();
        }

        //Поиск по таблице
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchTextBox.Text;
            List<EmployeeViewModel> searchResult = _repository.Search(search);
            EmployeeDataGrid.ItemsSource = searchResult;
        }

        //Выгрузка в Excel
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(EmployeeDataGrid.ItemsSource as List<EmployeeViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Сотрудники_{DateTime.Now.ToShortDateString()}.xlsx");
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
    }
}
