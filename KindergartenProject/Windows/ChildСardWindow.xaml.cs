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
    /// Логика взаимодействия для ChildСardWindow.xaml
    /// </summary>
    public partial class ChildСardWindow : Window
    {
        public ChildСardWindow()
        {
            InitializeComponent();
        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            KidsWindow kidsWindow = new KidsWindow();
            kidsWindow.Show();
            Close();
        }

        //Очистка текста при получении фокуса
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "Фамилия", "Имя", "Отчество", "Дата рождения" };
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
                { "PatronymicTextBox", PatronymicTextBox },
                { "SurenameTextBox", SurenameTextBox },
                { "NameTextBox", NameTextBox },
                { "BirthdayTextBox", BirthdayTextBox }
            };
            foreach (KeyValuePair<String, TextBox> kv in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(kv.Value.Text))
                {
                    switch (kv.Key)
                    {
                        case "PatronymicTextBox":
                            kv.Value.Text = "Отчество";
                            break;
                        case "SurenameTextBox":
                            kv.Value.Text = "Фамилия";
                            break;
                        case "NameTextBox":
                            kv.Value.Text = "Имя";
                            break;
                        case "BirthdayTextBox":
                            kv.Value.Text = "Дата рождения";
                            break;
                    }
                }
            }

        }

    }
}
