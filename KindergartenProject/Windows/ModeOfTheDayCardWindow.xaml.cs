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
    /// Логика взаимодействия для ModeOfTheDayCardWindow.xaml
    /// </summary>
    public partial class ModeOfTheDayCardWindow : Window
    {
        public ModeOfTheDayCardWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            ModeOfTheDayWindow modeOfTheDay = new ModeOfTheDayWindow();
            modeOfTheDay.Show();
            Close();
        }

        //Очистка текста при получении фокуса
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "Группа", "Занятие", "Время начала", "Время окончания" };
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
                { "GroupTextBox", GroupTextBox},
                { "OccupationTextBox", OccupationTextBox},
                { "StartTimeTextBox", StartTimeTextBox},
                { "EndTimeTextBox", EndTimeTextBox }
            };
            foreach (KeyValuePair<String, TextBox> kv in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(kv.Value.Text))
                {
                    switch (kv.Key)
                    {
                        case "GroupTextBox":
                            kv.Value.Text = "Группа";
                            break;
                        case "OccupationTextBox":
                            kv.Value.Text = "Занятие";
                            break;
                        case "StartTimeTextBox":
                            kv.Value.Text = "Время начала";
                            break;
                        case "EndTimeTextBox":
                            kv.Value.Text = "Время окончания";
                            break;
                    }
                }
            }

        }
    }
}
