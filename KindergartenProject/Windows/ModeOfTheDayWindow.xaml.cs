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
    /// Логика взаимодействия для ModeOfTheDayWindow.xaml
    /// </summary>
    public partial class ModeOfTheDayWindow : Window
    {
        private ModeOfTheDayRepository _modeOfTheRepository;
        private GroupRepository _groupRepository;
        private GroupViewModel _selectedGroup;

        public ModeOfTheDayWindow()
        {
            InitializeComponent();
            _modeOfTheRepository = new ModeOfTheDayRepository();
            _groupRepository = new GroupRepository();

            GroupComboBox.ItemsSource = _groupRepository.GetList();
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedGroup = GroupComboBox.SelectedItem as GroupViewModel;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            // Проверяем, выбрана ли группа
            if (_selectedGroup != null)
            {
                // Получение расписания только для выбранной группы
                var modeForSelectedGroup = _modeOfTheRepository.GetByGroupId(_selectedGroup.ID);
                ModeOfTheDayDataGrid.ItemsSource = modeForSelectedGroup;
            }
            else
            {
                // Если группа не выбрана, можно очистить DataGrid
                ModeOfTheDayDataGrid.ItemsSource = null;

            }
        }

        private void ModeOfTheDayDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ModeOfTheDayDataGrid.SelectedItem == null)
                return;
            var exampleCard = new ModeOfTheDayCardWindow(ModeOfTheDayDataGrid.SelectedItem as ModeOfTheDayViewModel);
            exampleCard.ShowDialog();
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
            if (ModeOfTheDayDataGrid.SelectedItem != null)
                return;
            var exampleCard = new ModeOfTheDayCardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModeOfTheDayDataGrid.SelectedItem == null)
                return;
            var exampleCard = new ModeOfTheDayCardWindow(ModeOfTheDayDataGrid.SelectedItem as ModeOfTheDayViewModel);
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModeOfTheDayDataGrid.SelectedItem == null)
                MessageBox.Show("Ничего не выбрано");
                var item = ModeOfTheDayDataGrid.SelectedItem as ModeOfTheDayViewModel;
            if (item == null)
                MessageBox.Show("Не удалось получить данные");
            else
            {
                _modeOfTheRepository.Delete(item.ID);
                UpdateGrid();
                MessageBox.Show("Удаление успешно");
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
