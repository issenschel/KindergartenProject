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
    }
}
