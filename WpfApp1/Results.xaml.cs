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
using System.Configuration;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public static int id_teacher = entry.identry;
        public Results()
        {
            InitializeComponent();
            NameShow();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            CheckClose ch = new CheckClose();
            ch.Show();
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ShowResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.name_of_test,u.first_name,u.middle_name,r.result,r.date from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }

        private async void FindNameResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.name_of_test,u.first_name,u.middle_name,r.result,r.date from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{TestName.SelectedValue.ToString()}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }
        private void NameShow()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"select distinct t.name_of_test from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}'";
            string sqlSurname = $"select distinct u.middle_name from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        TestName.Items.Add((string)reader.GetValue(0));  
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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        SurnameName.Items.Add((string)reader.GetValue(0));
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

        private void AnswersShow()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"select distinct t.name_of_test from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Name.Items.Add((string)reader.GetValue(0));
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
        private void Surnam()
        {            
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSurname = $"select distinct u.middle_name from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlSurname, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Surname.Items.Add((string)reader.GetValue(0));
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
        private void NameOfStudent()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select distinct u.first_name from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        NameStudent.Items.Add((string)reader.GetValue(0));
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
        private void Datee()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select  r.date from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Date.Items.Add((string)reader.GetValue(0));
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
        private async void FindSurnameResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.name_of_test,u.first_name,u.middle_name,r.result,r.date from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and u.middle_name='{SurnameName.SelectedValue.ToString()}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }

        private void CheckClick(object sender, RoutedEventArgs e)
        {
            StudentsAnswers.Visibility = Visibility.Visible;
            AnswersShow();
        }

        private void FindAnswersClick(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Surname.Text == "" || NameStudent.Text == "" || Date.Text == "")
            {
                MessageBox.Show("Введите все данные");
            }
            else
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSurname = $"select u.middle_name,u.first_name,s.name,t.name_of_test, r.date from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}' and r.date='{Date.SelectedValue.ToString()}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlSurname, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            InfoStudent.Content = (string)reader.GetValue(0) + " " + (string)reader.GetValue(1) + " " + (string)reader.GetValue(2) + " " + (string)reader.GetValue(3) + " " + (string)reader.GetValue(4) + " ";
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

        private void Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Surnam();
        }

        private void Surname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameOfStudent();
        }

        private void NameStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Datee();
        }

        private void ChangeAnswersClick(object sender, RoutedEventArgs e)
        {
            StudentsAnswers.Visibility = Visibility.Hidden;
        }
    }
}
