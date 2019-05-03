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
    /// Логика взаимодействия для NewTest.xaml
    /// </summary>
    public partial class NewTest : Window
    {
        public NewTest()
        {
            InitializeComponent();
            TimeBox.Text = "1";
            NumberBox.Text = "1";
        }

        private void Back_click_question(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void Upclick(object sender, RoutedEventArgs e)
        {
            if (TimeBox.Text == null)
            {
                TimeBox.Text = "1";
            }
            int s = Convert.ToInt32(TimeBox.Text);
            if (s < 500)
            {
                s++;
                TimeBox.Text = s.ToString();

            }
            else
            {
                MessageBox.Show("Ошибка! Не более 500 минут.");
            }

        }

        private void Downclick(object sender, RoutedEventArgs e)
        {
            
            int s = Convert.ToInt32(TimeBox.Text);
            if (s > 1)
            {
                s--;
                TimeBox.Text = s.ToString();
            }
            else
            {
                MessageBox.Show("Ошибка! Не менее 1 минуты.");
            }

        }

        private void UpNumberclick(object sender, RoutedEventArgs e)
        {
            int s = Convert.ToInt32(NumberBox.Text);
            if (s < 1000)
            {
                s++;
                NumberBox.Text = s.ToString();

            }
            else
            {
                MessageBox.Show("Ошибка! Не более 1000 вопросов");
            }
        }

        private void DownNumberclick(object sender, RoutedEventArgs e)
        {
            int s = Convert.ToInt32(NumberBox.Text);
            if (s > 1)
            {
                s--;
                NumberBox.Text = s.ToString();
            }
            else
            {
                MessageBox.Show("Ошибка! Не менее 1 вопроса.");
            }
        }

        private void TimeBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("В данное поле можно вводить только цифры.");
            }
        }
    }
}
