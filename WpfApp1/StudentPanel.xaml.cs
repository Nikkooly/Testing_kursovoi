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
    /// Логика взаимодействия для StudentPanel.xaml
    /// </summary>
    public partial class StudentPanel : Window
    {
        public static int id_student = MainWindow.identry;
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
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlName = $"select distinct t.name_of_test from users_tests as u  inner join tests as t  on u.test_id = t.id where student_id = '{id_student}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlName, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        NameOfTest.Items.Add((string)reader.GetValue(0));
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

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }


        public static string Name = "";
        private void Showclick(object sender, RoutedEventArgs e)
        {
            if ( DateOfTest.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSubject = $"select r.result,t.theme,s.name,us.first_name,us.middle_name from users_tests as u  inner join results as r  on u.id = r.unique_id  inner join tests as t  on u.test_id = t.id  inner join subjects as s  on t.subject_id = s.id  inner join users as us  on t.teacher_id = us.id where student_id = '{id_student}' and r.date='{DateOfTest.SelectedValue.ToString()}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlSubject, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            ThemeLabel.Content = (string)reader.GetValue(1);
                            SubjectLabel.Content = (string)reader.GetValue(2);
                            Name = (string)reader.GetValue(4);
                            Name = Name.Substring(0, 1);
                            TeacherLabel.Content = (string)reader.GetValue(3) + " " + Name + ".";
                            ResultLabel.Content = (string)reader.GetValue(0) + "%";
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
        }
        public static string name = "";
        private void NameOfTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateOfTest.Items.Clear();
            name = NameOfTest.SelectedValue.ToString();
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlDate = $"select r.date,t.name_of_test from users_tests as u inner join results as r on u.id = r.unique_id inner join tests as t on u.test_id=t.id where student_id = '{id_student}'";
            string sqlSubject = $"select t.name_of_test,u.student_id,r.result,t.theme,s.name,us.first_name,us.middle_name from users_tests as u  inner join results as r  on u.id = r.unique_id  inner join tests as t  on u.test_id = t.id  inner join subjects as s  on t.subject_id = s.id  inner join users as us  on t.teacher_id = us.id where student_id = '{id_student}'";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlDate, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (name.Equals(reader.GetValue(1)))
                        {
                            DateOfTest.Items.Add((string)reader.GetValue(0));
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
    }

    
}

