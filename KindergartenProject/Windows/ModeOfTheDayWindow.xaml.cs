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
        public ModeOfTheDayWindow()
        {
            InitializeComponent();
            _modeOfTheRepository = new ModeOfTheDayRepository();
            _groupRepository = new GroupRepository();

            GroupComboBox.ItemsSource = _groupRepository.GetList();
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGroup = GroupComboBox.SelectedItem as GroupViewModel;
            if (selectedGroup != null)
            {
                // Получение ID выбранной группы
                long selectedGroupID = selectedGroup.ID;

                // Получение расписания для выбранной группы
                var modeForSelectedGroup = _modeOfTheRepository.GetByGroupId(selectedGroupID);

                // Установить источник объектов для DataGrid
                ModeOfTheDayDataGrid.ItemsSource = modeForSelectedGroup;
            }
        }


        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MealScheduleCardWindow mealScheduleCardWindow = new MealScheduleCardWindow();
            mealScheduleCardWindow.Show();
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
