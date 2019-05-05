using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static int id_teacher = entry.identry;
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

        private void InsertVariantClick(object sender, RoutedEventArgs e)
        {
            if (VariantBox.Text == "")
            {
                MessageBox.Show("Введите значение в поле вариант");
            }
        }
        public static int id_subject;
        public static string result;
        public static int id_questions;
        public static int count;
        private void CreateTestclick(object sender, RoutedEventArgs e)
        {
            
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"select q.id,count(a.id) from questions as q inner join answers as a " +
                $"on q.id = a.question_id  " +
                $"where q.teacher_id = '{id_teacher}' and q.theme in" +
                $" (select q.theme from questions where q.theme = '{ThemeBox.Text}') " +
                $"group by q.id";
            string sqlExpSubject = $"Select id from subjects where name='{SubjectBox.Text}'";
            if (MixButton.IsChecked == true)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    List<int> questions;
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlCommand cm1 = connection.CreateCommand();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        questions = new List<int>();
                        while (reader.Read())
                        {
                            questions.Add(Convert.ToInt32(reader.GetValue(0)));
                            
                        }
                        reader.Close();
                       foreach(int n in questions)
                        {
                            cm1.CommandText += $"Insert into questions_tests(question_id) values('{n}')";
                            cm1.ExecuteNonQuery();
                        }                        
                        reader.Close();
                        MessageBox.Show("Успешно добавлено");
                    }                                         
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            //check extins subject in database
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpSubject, connection);
                SqlCommand cm1 = connection.CreateCommand();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0)!=null)
                        {
                            object x = reader.GetValue(0);
                            int a = Convert.ToInt32(x);
                            cm1.CommandText = $"Insert into questions_tests(subject_id) values('{a}')";
                            cm1.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Такого предмета не существует!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    reader.Close();
                }
            }
           
        }

        private void YesButton_Checked(object sender, RoutedEventArgs e)
        {
            if (YesButton.IsChecked == true)
            {
                VariantBox.Visibility = Visibility.Visible;
                VariantButton.Visibility = Visibility.Visible;
                VariantOfTestChange.Visibility = Visibility.Visible;
            }
        }

        private void NoButton_Checked(object sender, RoutedEventArgs e)
        {
            if (NoButton.IsChecked == true)
            {
                VariantBox.Visibility = Visibility.Hidden;
                VariantButton.Visibility = Visibility.Hidden;
                VariantOfTestChange.Visibility = Visibility.Hidden;
            }
        }
    }
}
