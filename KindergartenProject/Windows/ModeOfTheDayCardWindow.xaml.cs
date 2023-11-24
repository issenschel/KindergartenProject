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
using KindergartenProject.Infrastructure.Database;
using KindergartenProject.Infrastructure.ViewModels;

namespace KindergartenProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для ModeOfTheDayCardWindow.xaml
    /// </summary>
    public partial class ModeOfTheDayCardWindow : Window
    {
        private ModeOfTheDayViewModel _selectedItem = null;
        private ModeOfTheDayRepository _repository = new ModeOfTheDayRepository();
        public ModeOfTheDayCardWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Заполняем или обновляем данные в _selectedItem
                if (_selectedItem == null)
                {
                    _selectedItem = new ModeOfTheDayViewModel();
                    // Тут может потребоваться установка начальных значений для новой записи
                }

                // Обновляем значения свойств объекта из текстовых полей
                _selectedItem.GroupName = GroupTextBox.Text;
                _selectedItem.OccupationName = OccupationTextBox.Text;
                _selectedItem.StartTime = StartTimeTextBox.Text;
                _selectedItem.EndTime = EndTimeTextBox.Text;

                var groupId = _repository.GetGroupIdByName(GroupTextBox.Text);
                if (!groupId.HasValue) // Если группа не найдена
                {
                    MessageBox.Show("Такой группы нет.", "Ошибка");
                    return; // Выход из обработчика, чтобы предотвратить сохранение
                }

                _selectedItem.GroupId = groupId.Value;
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ModeOfTheDayCardWindow(ModeOfTheDayViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
                GroupTextBox.Text = _selectedItem.GroupName;
                OccupationTextBox.Text = _selectedItem.OccupationName;
                StartTimeTextBox.Text = _selectedItem.StartTime;
                EndTimeTextBox.Text = _selectedItem.EndTime;
            }
        }


        private void SectionButton_Click(object sender, RoutedEventArgs e)
        {
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
