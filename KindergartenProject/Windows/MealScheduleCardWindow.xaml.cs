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
    /// Логика взаимодействия для MealScheduleCardWindow.xaml
    /// </summary>
    public partial class MealScheduleCardWindow : Window
    {
        private NutritionRepository _nutritionRepository = new NutritionRepository();
        private MealScheduleRepository _mealScheduleRepository = new MealScheduleRepository();
        private MealScheduleViewModel _schedule; 

        public MealScheduleCardWindow()
        {
            InitializeComponent();
        }

        public MealScheduleCardWindow(MealScheduleViewModel schedule)
        {
            InitializeComponent();

            _schedule = schedule;

            if (_schedule != null && _schedule.NutritionId != 0)
            {
                // Загрузим данные из Nutrition
                var nutrition = _nutritionRepository.GetById(_schedule.NutritionId);
                DayofWeekTextBox.Text = _schedule.DayOfTheWeekName;
                BreakFastTextBox.Text = nutrition.BreakFastName;
                BrunchTextBox.Text = nutrition.BrunchName;
                LunchTextBox.Text = nutrition.LunchName;
                AfternoonSnackTextBox.Text = nutrition.AfternoonSnackName;
                DinnerTextBox.Text = nutrition.DinnerName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NutritionViewModel nutrition;

            // Если _schedule не null и NutritionId установлен, то обновляем существующую запись.
            if (_schedule != null && _schedule.NutritionId > 0)
            {
                nutrition = _nutritionRepository.GetById(_schedule.NutritionId);
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
            if (_schedule == null)
            {
                // Создаем новое расписание, если не существует
                _schedule = new MealScheduleViewModel
                {
                    DayOfTheWeekId = _mealScheduleRepository.GetDayIdByName(DayofWeekTextBox.Text).Value,
                    NutritionId = nutrition.ID
                };
                var result = _mealScheduleRepository.Add(_schedule);
                if (result == null)
                {
                    MessageBox.Show("Не удалось добавить расписание питания.");
                    return;
                }
            }
            Close();
        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "День недели", "Завтрак", "Ланч", "Обед", "Полдник","Ужин"};
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
                { "DayofWeekTextBox", DayofWeekTextBox},
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
                        case "DayofWeekTextBox":
                            kv.Value.Text = "День недели";
                            break;
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
