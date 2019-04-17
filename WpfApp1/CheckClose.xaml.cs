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
    /// Логика взаимодействия для CheckClose.xaml
    /// </summary>
    public partial class CheckClose : Window
    {
        public CheckClose()
        {
            InitializeComponent();
        }

        private void Close_click_registration(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void no_click(object sender, RoutedEventArgs e)
        {

        }

        private void yes_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
