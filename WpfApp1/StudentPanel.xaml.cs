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
    /// Логика взаимодействия для StudentPanel.xaml
    /// </summary>
    public partial class StudentPanel : Window
    {
        public StudentPanel()
        {
            InitializeComponent();
            FunnyImage.Visibility = Visibility.Visible;
        }

        private void PassTestclick(object sender, RoutedEventArgs e)
        {
            FunnyImage.Visibility = Visibility.Hidden;
            Results.Visibility = Visibility.Hidden;
            Test test = new Test();
            test.Show();
        }

        private void CheckResultTestclick(object sender, RoutedEventArgs e)
        {
            FunnyImage.Visibility = Visibility.Hidden;
            Results.Visibility = Visibility.Visible;
        }

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void ResultTestclick(object sender, RoutedEventArgs e)
        {

        }

        private void BackTestclick(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardTestclick(object sender, RoutedEventArgs e)
        {

        }
    }
}
