using KindergartenProject.Infrastructure.Consts;
using KindergartenProject.Infrastructure.Database;
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
    public partial class MealScheduleWindow : Window
    {
        private NutritionRepository _nutritionRepository;
        private DayOfTheWeekRepository _dayOfTheWeekRepository;
        private MealScheduleRepository _mealScheduleRepository;
        private DayOfTheWeekViewModel _selectedDay;
        long roleId = (long)Application.Current.Resources[UserInfoConsts.RoleId];
        //Инициализация окна
        public MealScheduleWindow()
        {
            InitializeComponent();
            _nutritionRepository = new NutritionRepository();
            _dayOfTheWeekRepository = new DayOfTheWeekRepository();
            _mealScheduleRepository = new MealScheduleRepository();
            GrantAccessByRole();
            DayOfTheWeekComboBox.ItemsSource = _dayOfTheWeekRepository.GetList();
        }
        //Открытие карточки для редактирования по двойному клику
        private void MealSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (roleId == 3)
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
        }
        //По выбранному дню обновлять датагрид
        private void DayOfTheWeekComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDay = DayOfTheWeekComboBox.SelectedItem as DayOfTheWeekViewModel;
            UpdateGrid();
        }
        //Обновление датагрида
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
        //выход в меню по ролям
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            //если гость то в гостевое
            if (roleId == 1)
            {
                GuestWindow guestWindow = new GuestWindow();
                guestWindow.Show();
                Close();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
        //Добавление
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MealScheduleDataGrid.SelectedItem != null)
                return;
            var exampleCard = new MealScheduleCardWindow();
            exampleCard.ShowDialog();
            UpdateGrid();
        }
        //Обновление
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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
        //Удаление
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

        //Выгрузка в Excel
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(MealScheduleDataGrid.ItemsSource as List<NutritionViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Питание_{DateTime.Now.ToShortDateString()}.xlsx");
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
        //Отображение по ролям
        private void GrantAccessByRole()
        {
            if (roleId == 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                UploadButton.Visibility = Visibility.Collapsed;
            }

            else if (roleId == 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                UploadButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
