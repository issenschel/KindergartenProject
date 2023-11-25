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
    /// Логика взаимодействия для GroupCardWindow.xaml
    /// </summary>
    public partial class GroupCardWindow : Window
    {
        private GroupViewModel _selectedItem = null;
        private GroupRepository _repository = new GroupRepository();

        public GroupCardWindow()
        {
            InitializeComponent();
            var employees = _repository.GetEmployees();
            EmployeeComboBox.ItemsSource = employees;
        }

        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public GroupCardWindow(GroupViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
                GroupTextBox.Text = _selectedItem.Name;
                PlacesTextBox.Text = _selectedItem.AvailableSeats.ToString();

                var employees = _repository.GetEmployees();
                EmployeeComboBox.ItemsSource = employees;
                EmployeeComboBox.SelectedItem = employees.FirstOrDefault(p => p.ID == _selectedItem.EmployeeId);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Заполняем или обновляем данные в _selectedItem
                if (_selectedItem == null)
                {
                    _selectedItem = new GroupViewModel();
                }

                // Обновляем значения свойств объекта из текстовых полей
                _selectedItem.Name = GroupTextBox.Text;

                if (string.IsNullOrWhiteSpace(PlacesTextBox.Text) || !long.TryParse(PlacesTextBox.Text, out long places))
                {
                    throw new Exception("Места должны быть заполнены числом");
                }
                if (EmployeeComboBox.SelectedValue == null || (long)EmployeeComboBox.SelectedValue == 0)
                {
                    throw new Exception("Сотрудник должен быть выбран");
                }

                _selectedItem.AvailableSeats =long.Parse(PlacesTextBox.Text);
                _selectedItem.EmployeeId = (long)EmployeeComboBox.SelectedValue;


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



        //Очистка текста при получении фокуса
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> strings = new List<string>() { "Группа", "Количество свободных мест"};
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
                { "PlacesTextBox", PlacesTextBox},
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
                        case "PlacesTextBox":
                            kv.Value.Text = "Количество свободных мест";
                            break;
                    }
                }
            }

        }
    }
}
