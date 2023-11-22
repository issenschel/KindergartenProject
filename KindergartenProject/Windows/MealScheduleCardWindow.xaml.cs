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
        public MealScheduleCardWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            MealScheduleWindow mealScheduleWindow = new MealScheduleWindow();
            mealScheduleWindow.Show();
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
