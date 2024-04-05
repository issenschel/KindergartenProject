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
    public partial class MealScheduleCardWindow : Window
    {
        private NutritionRepository _nutritionRepository = new NutritionRepository();
        private MealScheduleViewModel _selectedItem = null;
        private MealScheduleRepository _mealScheduleRepository = new MealScheduleRepository();

        // В конструкторе происходит инициализация компонентов окна и заполнение ComboBox данными
        public MealScheduleCardWindow()
        {
            InitializeComponent();
            var dayOfTheWeek = _mealScheduleRepository.GetDaysOfTheWeeks();
            DayofWeekComboBox.ItemsSource = dayOfTheWeek;
            DayofWeekComboBox.SelectedItem = dayOfTheWeek.FirstOrDefault();
        }

        // В конструктор передается элемент, и если он не равен null, то значения полей окна заполняются данными этого элемента
        public MealScheduleCardWindow(MealScheduleViewModel schedule)
        {
            InitializeComponent();

            _selectedItem = schedule;

            if (_selectedItem != null && _selectedItem.NutritionId != 0)
            {
                // Загрузим данные из Nutrition
                var nutrition = _nutritionRepository.GetById(_selectedItem.NutritionId);
                BreakFastTextBox.Text = nutrition.BreakFastName;
                BrunchTextBox.Text = nutrition.BrunchName;
                LunchTextBox.Text = nutrition.LunchName;
                AfternoonSnackTextBox.Text = nutrition.AfternoonSnackName;
                DinnerTextBox.Text = nutrition.DinnerName;

                var dayOfTheWeek = _mealScheduleRepository.GetDaysOfTheWeeks();
                DayofWeekComboBox.ItemsSource = dayOfTheWeek;
                DayofWeekComboBox.SelectedItem = dayOfTheWeek.FirstOrDefault(p => p.ID == _selectedItem.DayOfTheWeekId);
            }
        }

        //Сохранение данных
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NutritionViewModel nutrition;

                // Если _schedule не null и NutritionId установлен, то обновляем существующую запись.
                if (_selectedItem != null && _selectedItem.NutritionId > 0)
                {
                    nutrition = _nutritionRepository.GetById(_selectedItem.NutritionId);
                    if (nutrition == null)
                    {
                        MessageBox.Show("Информация о питании не найдена.");
                        return;
                    }

                    // Обновляем информацию о блюдах
                    nutrition.BreakFastName = BreakFastTextBox.Text;
                    nutrition.BrunchName = BrunchTextBox.Text;
                    nutrition.LunchName = LunchTextBox.Text;
                    nutrition.AfternoonSnackName = AfternoonSnackTextBox.Text;
                    nutrition.DinnerName = DinnerTextBox.Text;

                    // Вызываем метод Update репозитория
                    nutrition = _nutritionRepository.Update(nutrition);
                }
                else
                {
                    // Если _schedule null или NutritionId не установлен, создаем новую запись.
                    nutrition = new NutritionViewModel
                    {
                        BreakFastName = BreakFastTextBox.Text,
                        BrunchName = BrunchTextBox.Text,
                        LunchName = LunchTextBox.Text,
                        AfternoonSnackName = AfternoonSnackTextBox.Text,
                        DinnerName = DinnerTextBox.Text
                    };

                    nutrition = _nutritionRepository.Add(nutrition);
                }

                if (nutrition == null)
                {
                    MessageBox.Show("Ошибка при сохранении информации о питании.");
                    return;
                }

                // Теперь либо создаем новое расписание, либо обновляем существующее.
                if (_selectedItem == null)
                {
                    _selectedItem = new MealScheduleViewModel();
                }

                if (DayofWeekComboBox.SelectedValue == null || (long)DayofWeekComboBox.SelectedValue == 0)
                {
                    throw new Exception("День недели должен быть выбран");
                }
                _selectedItem.DayOfTheWeekId = (long)DayofWeekComboBox.SelectedValue;
                _selectedItem.NutritionId = nutrition.ID;

                if (_selectedItem.ID == 0)
                {
                    // Создание нового элемента
                    _mealScheduleRepository.Add(_selectedItem);
                    MessageBox.Show("Запись успешно добавлена.", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Обновление существующего элемента
                    _mealScheduleRepository.Update(_selectedItem);
                    MessageBox.Show("Запись успешно обновлена.", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


           
        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "Завтрак", "Ланч", "Обед", "Полдник","Ужин"};
            TextBox textBox = (TextBox)sender;
            foreach (string s in strings)
            {
                if (textBox.Text == s)
                    textBox.Text = "";
            }

        }

        //Возвращение исходных названий при пустом поле
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Dictionary<String, TextBox> textBoxes = new Dictionary<String, TextBox>()
            {
                { "BreakFastTextBox", BreakFastTextBox},
                { "BrunchTextBox", BrunchTextBox},
                { "LunchTextBox", LunchTextBox },
                { "AfternoonSnackTextBox", AfternoonSnackTextBox },
                { "DinnerTextBox", DinnerTextBox }
            };
            foreach (KeyValuePair<String, TextBox> kv in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(kv.Value.Text))
                {
                    switch (kv.Key)
                    {
                        case "BreakFastTextBox":
                            kv.Value.Text = "Завтрак";
                            break;
                        case "BrunchTextBox":
                            kv.Value.Text = "Ланч";
                            break;
                        case "LunchTextBox":
                            kv.Value.Text = "Обед";
                            break;
                        case "AfternoonSnackTextBox":
                            kv.Value.Text = "Полдник";
                            break;
                        case "DinnerTextBox":
                            kv.Value.Text = "Ужин";
                            break;
                    }
                }
            }

        }
    }
}

