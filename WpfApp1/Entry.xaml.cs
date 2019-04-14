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
    /// Логика взаимодействия для Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        public Entry()
        {
            InitializeComponent();
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            MainWindow mwn = new MainWindow();
            mwn.Show();
            this.Close();
        }

        private void Close_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Forgot_click_entry(object sender, RoutedEventArgs e)
        {
            RecoveryPassword rp = new RecoveryPassword();
            rp.Show();
            this.Close();
        }
    }
}
