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
    /// Логика взаимодействия для RecoveryPassword.xaml
    /// </summary>
    public partial class RecoveryPassword : Window
    {
        public RecoveryPassword()
        {
            InitializeComponent();
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            MainWindow mww = new MainWindow();
            mww.Show();
            this.Close();
        }

        private void Close_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registration_click_forgotpassord(object sender, RoutedEventArgs e)
        {
            Registration rgg = new Registration();
            rgg.Show();
            this.Close();
        }

        private void Entry_click_forgotpassword(object sender, RoutedEventArgs e)
        {
            Entry entr = new Entry();
            entr.Show();
            this.Close();
        }
    }
}
