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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Teacher_panel.xaml
    /// </summary>
    public partial class Teacher_panel : Window
    {
        public Teacher_panel()
        {
            InitializeComponent();
        }

        private void Create_test_click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_click_teacher(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
            //this.Close();
        }

        private void Back_click_teacher(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
            //Entry entry = new Entry();
            //entry.Show();
            //this.Close();
        }
    }
}
