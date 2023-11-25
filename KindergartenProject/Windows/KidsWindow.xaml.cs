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
using KindergartenProject.Infrastructure;
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.ViewModels;


namespace KindergartenProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для KidsWindow.xaml
    /// </summary>
    public partial class KidsWindow : Window
    {
        private KidRepository _repository;
        public KidsWindow()
        {
            InitializeComponent();
            _repository = new KidRepository();
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
            if (KidDataGrid.SelectedItem != null)
                return;
            var exampleCard = new ChildСardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            KidDataGrid.ItemsSource = _repository.GetList();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (KidDataGrid.SelectedItem == null)
                return;
            var exampleCard = new ChildСardWindow(KidDataGrid.SelectedItem as KidViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
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
    }
}
