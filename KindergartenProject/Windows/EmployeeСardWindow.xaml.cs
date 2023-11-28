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
    public partial class EmployeeСardWindow : Window
    {
        private EmployeeViewModel _selectedItem = null;
        private EmployeeRepository _repository = new EmployeeRepository();

        // В конструкторе происходит инициализация компонентов окна и заполнение ComboBox данными
        public EmployeeСardWindow()
        {
            InitializeComponent();
            var posts = _repository.GetPosts();
            PostComboBox.ItemsSource = posts;
            PostComboBox.SelectedItem = posts.FirstOrDefault();
        }

        // В конструктор передается элемент, и если он не равен null, то значения полей окна заполняются данными этого элемента
        public EmployeeСardWindow(EmployeeViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
                SurenameTextBox.Text = _selectedItem.Surname;
                NameTextBox.Text = _selectedItem.Name;
                PatronymicTextBox.Text = _selectedItem.Patronymic;
                BirthdayTextBox.Text = _selectedItem.DateOfBirth;

                var posts = _repository.GetPosts();
                PostComboBox.ItemsSource = posts;
                PostComboBox.SelectedItem = posts.FirstOrDefault(p => p.ID == _selectedItem.PostId);

                ExperienceTextBox.Text = _selectedItem.Experience.ToString();
                LoginTextBox.Text = _selectedItem.UserName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Заполняем или обновляем данные в _selectedItem
                if (_selectedItem == null)
                {
                    _selectedItem = new EmployeeViewModel();
                }

                // Обновляем значения свойств объекта из текстовых полей
                _selectedItem.Name = NameTextBox.Text;
                _selectedItem.Surname = SurenameTextBox.Text;
                _selectedItem.Patronymic = PatronymicTextBox.Text;
                _selectedItem.DateOfBirth = BirthdayTextBox.Text;
                _selectedItem.UserName = LoginTextBox.Text;

                if (string.IsNullOrWhiteSpace(ExperienceTextBox.Text) || !long.TryParse(ExperienceTextBox.Text, out long experience))
                {
                    throw new Exception("Опыт должен быть заполнен числом");
                }
                if (PostComboBox.SelectedValue == null || (long)PostComboBox.SelectedValue == 0)
                {
                    throw new Exception("Пост должен быть выбран");
                }


                _selectedItem.PostId = (long)PostComboBox.SelectedValue;
                _selectedItem.Experience = long.Parse(ExperienceTextBox.Text);


                var loginId = _repository.GetLoginIdByName(LoginTextBox.Text);
                if (!loginId.HasValue) // Если Логин не найден
                {
                    MessageBox.Show("Такого логина нет.", "Ошибка");
                    return; // Выход из обработчика, чтобы предотвратить сохранение
                }

                _selectedItem.UserId = loginId.Value;

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

        //Закрытие карточки
        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Очистка текста при получении фокуса
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "Фамилия", "Имя", "Отчество", "Дата рождения", "Пост","Логин","Опыт работы" };
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
                { "ExperienceTextBox", ExperienceTextBox },
                { "LoginTextBox", LoginTextBox }
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
                        case "ExperienceTextBox":
                            kv.Value.Text = "Опыт работы";
                            break;
                        case "LoginTextBox":
                            kv.Value.Text = "Логин";
                            break;
                    }
                }
            }



        }

    }

}
