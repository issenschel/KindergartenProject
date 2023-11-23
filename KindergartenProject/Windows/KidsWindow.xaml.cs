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
using KindergartenProject.Infrastructure.ViewModels;
using KindergartenProject.Infrastructure.Mappers;
using KindergartenProject.Infrastructure.Database;



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
            KidDataGrid.ItemsSource = _repository.GetList();


        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ChildСardWindow childСardWindow = new ChildСardWindow();
            childСardWindow.Show();
            Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
