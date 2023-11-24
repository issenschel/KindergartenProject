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
    /// Логика взаимодействия для MealScheduleWindow.xaml
    /// </summary>
    public partial class MealScheduleWindow : Window
    {
        private NutritionRepository _nutritionRepository;
        private DayOfTheWeekRepository _dayOfTheWeekRepository;
        private MealScheduleRepository _mealScheduleRepository;
        public MealScheduleWindow()
        {
            InitializeComponent();
            _nutritionRepository = new NutritionRepository();
            _dayOfTheWeekRepository = new DayOfTheWeekRepository();
            _mealScheduleRepository = new MealScheduleRepository();

            DayOfTheWeekComboBox.ItemsSource = _dayOfTheWeekRepository.GetList();
        }

        private void DayOfTheWeekComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDay = DayOfTheWeekComboBox.SelectedItem as DayOfTheWeekViewModel;
            if (selectedDay != null)
            {
                // Получение ID выбранного дня недели
                long selectedDayId = selectedDay.ID;

                // Получение расписаний питания, которые совпадают с выбранным днем недели
                var mealSchedules = _mealScheduleRepository.GetByDayId(selectedDayId);

                // Получение питания для этих расписаний
                var nutritionForDay = mealSchedules.Select(ms => _nutritionRepository.GetById(ms.NutritionId)).ToList();

                // Установить источник объектов для DataGrid
                MealScheduleDataGrid.ItemsSource = nutritionForDay;
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
