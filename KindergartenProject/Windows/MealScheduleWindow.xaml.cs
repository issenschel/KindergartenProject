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
        private DayOfTheWeekViewModel _selectedDay;

        public MealScheduleWindow()
        {
            InitializeComponent();
            _nutritionRepository = new NutritionRepository();
            _dayOfTheWeekRepository = new DayOfTheWeekRepository();
            _mealScheduleRepository = new MealScheduleRepository();

            DayOfTheWeekComboBox.ItemsSource = _dayOfTheWeekRepository.GetList();
        }

        private void MealSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedNutrition = MealScheduleDataGrid.SelectedItem as NutritionViewModel;
            if (selectedNutrition == null)
                return;

            var mealSchedule = _mealScheduleRepository.GetByNutritionId(selectedNutrition.ID);
            if (mealSchedule == null)
            {
                MessageBox.Show("Не удалось найти связанное расписание.");
                return;
            }

            var mealScheduleCardWindow = new MealScheduleCardWindow(mealSchedule);
            mealScheduleCardWindow.ShowDialog();
            UpdateGrid();
        }

        private void DayOfTheWeekComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDay = DayOfTheWeekComboBox.SelectedItem as DayOfTheWeekViewModel;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (_selectedDay != null)
            {
                // Получение расписаний питания, которые совпадают с выбранным днем недели
                var mealSchedules = _mealScheduleRepository.GetByDayId(_selectedDay.ID);
                // Получение питания для этих расписаний
                var nutritionForDay = mealSchedules.Select(ms => _nutritionRepository.GetById(ms.NutritionId)).ToList();
                // Установить источник объектов для DataGrid
                MealScheduleDataGrid.ItemsSource = nutritionForDay;
            }
            else
            {
                MealScheduleDataGrid.ItemsSource = null;
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
            if (MealScheduleDataGrid.SelectedItem != null)
                return;
            var exampleCard = new MealScheduleCardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MealScheduleDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Ничего не выбрано");
                return;
            }

            var selectedNutritionItem = MealScheduleDataGrid.SelectedItem as NutritionViewModel;
            if (selectedNutritionItem == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }

            var mealScheduleToDelete = _mealScheduleRepository.GetByDayId(_selectedDay.ID)
                .FirstOrDefault(ms => ms.NutritionId == selectedNutritionItem.ID);

            if (mealScheduleToDelete == null)
            {
                MessageBox.Show("Не удалось найти расписание питания для удаления");
                return;
            }

            _mealScheduleRepository.Delete(mealScheduleToDelete.ID);
            UpdateGrid();
            MessageBox.Show("Удаление успешно");
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
