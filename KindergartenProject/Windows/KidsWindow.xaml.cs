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
using KindergartenProject.Infrastructure;
using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.Report;
using KindergartenProject.Infrastructure.ViewModels;


namespace KindergartenProject.Windows
{
    public partial class KidsWindow : Window
    {
        private KidRepository _repository;
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];
        //Инициализация окна
        public KidsWindow()
        {
            InitializeComponent();
            _repository = new KidRepository();
            GrantAccessByRole();
            UpdateGrid();


        }
        //Выход в меню
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        //Добавление(открытие карточки)
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (KidDataGrid.SelectedItem != null)
                return;
            var exampleCard = new ChildСardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }
        //Обновление датагрида
        private void UpdateGrid()
        {
            KidDataGrid.ItemsSource = _repository.GetList();
        }

        //Поиск
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchTextBox.Text;
            List<KidViewModel> searchResult = _repository.Search(search);
            KidDataGrid.ItemsSource = searchResult;
        }
        //Открытие карточки для редактирования по двойному клику
        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (roleId == 3)
            {
                if (KidDataGrid.SelectedItem == null)
                    return;
                var exampleCard = new ChildСardWindow(KidDataGrid.SelectedItem as KidViewModel);
                exampleCard.ShowDialog();
                UpdateGrid();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (KidDataGrid.SelectedItem == null)
                MessageBox.Show("Ничего не выбрано");
            var item = KidDataGrid.SelectedItem as KidViewModel;
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
            if (KidDataGrid.SelectedItem == null)
                return;
            var exampleCard = new ChildСardWindow(KidDataGrid.SelectedItem as KidViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void GrantAccessByRole()
        {
            if (roleId == 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                UploadButton.Visibility = Visibility.Collapsed;
            }
        }

        //Выгрузка в Excel
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(KidDataGrid.ItemsSource as List<KidViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Дети_{DateTime.Now.ToShortDateString()}.xlsx");
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
