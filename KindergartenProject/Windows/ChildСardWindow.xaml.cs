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
    /// Логика взаимодействия для ChildСardWindow.xaml
    /// </summary>
    public partial class ChildСardWindow : Window
    {
        private KidViewModel _selectedItem = null;
        private KidRepository _repository = new KidRepository();

        public ChildСardWindow()
        {
            InitializeComponent();
            var groups = _repository.GetGroups();
            GroupComboBox.ItemsSource = groups;
        }

        public ChildСardWindow(KidViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
                NameTextBox.Text = _selectedItem.Name;
                SurenameTextBox.Text = _selectedItem.Surname;
                PatronymicTextBox.Text = _selectedItem.Patronymic;
                BirthdayTextBox.Text = _selectedItem.DateOfBirth;

                var groups = _repository.GetGroups();
                GroupComboBox.ItemsSource = groups;
                GroupComboBox.SelectedItem = groups.FirstOrDefault(p => p.ID == _selectedItem.GroupId);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (_selectedItem == null)
                {
                    _selectedItem = new KidViewModel();
                }

                if (GroupComboBox.SelectedValue == null || (long)GroupComboBox.SelectedValue == 0)
                {
                    throw new Exception("Группа должна быть выбрана");
                }
                // Заполняем или обновляем данные в _selectedItem
                _selectedItem.GroupId = (long)GroupComboBox.SelectedValue;
                _selectedItem.Surname = SurenameTextBox.Text;
                _selectedItem.Patronymic = PatronymicTextBox.Text;
                _selectedItem.Name = NameTextBox.Text;
                _selectedItem.DateOfBirth = BirthdayTextBox.Text;



                // Операция создания или обновления
                if (_selectedItem.ID == 0)
                {
                    // Создание нового элемента
                    _repository.Add(_selectedItem);
                    MessageBox.Show("Запись успешно добавлена.", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Обновление существующего элемента
                    _repository.Update(_selectedItem);
                    MessageBox.Show("Запись успешно обновлена.", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Закрытие формы после сохранения данных
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
                { "BirthdayTextBox", BirthdayTextBox },
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
