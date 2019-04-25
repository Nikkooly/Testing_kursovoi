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
    /// Логика взаимодействия для CheckTest.xaml
    /// </summary>
    public partial class CheckTest : Window
    {
        public CheckTest()
        {
            InitializeComponent();
        }

        private void Have_test_Click(object sender, RoutedEventArgs e)
        {

        }

        private void New_test_click(object sender, RoutedEventArgs e)
        {
            NewTest newtest = new NewTest();
            newtest.Show();
            this.Close();
        }

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
