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
            j = 0;
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            j = 0;
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
            CheckAnswers.Visibility = Visibility.Visible;
            AnswerStudentForChange();
            CountAnswers.Content = count;
            j++;
            CountNow.Content = j.ToString();
        }
        public static int id_question = 0;
        public static string answerss = "";
        public static int unique_id = 0;
        private void AnswerStudentForChange()
        {
            List<int> questions;
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select distinct q.id,us.count_questions,us.id from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id inner join answers_tests as a on us.id=a.user_test_id inner join questions as q on a.question_id=q.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}' and r.date='{Date.SelectedValue.ToString()}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    questions = new List<int>();
                    while (reader.Read())
                    {
                        questions.Add(Convert.ToInt32(reader.GetValue(0)));
                        count = Convert.ToInt32(reader.GetValue(1));
                        unique_id= Convert.ToInt32(reader.GetValue(2));
                    }
                    id_question = Convert.ToInt32(questions.ElementAt(j));
                    Que();
                    Ans();
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
        public void Que()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select q.question from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id inner join answers_tests as a on us.id=a.user_test_id inner join questions as q on a.question_id=q.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}' and r.date='{Date.SelectedValue.ToString()}' and q.id='{id_question}'";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    while (reader.Read())
                    {
                        Question.Text = (string)reader.GetValue(0);
                    }
                    reader.Close();

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
        public void Ans()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select a.answer_student from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id inner join answers_tests as a on us.id=a.user_test_id inner join questions as q on a.question_id=q.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}' and r.date='{Date.SelectedValue.ToString()}' and a.question_id='{id_question}'";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<string> answers;
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    answers = new List<string>();
                    while (reader.Read())
                    {
                        AnswerStudentTest.Text = (string)reader.GetValue(0);
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
        public static int a;
        private void IsTr()
        {
            string s = "Да";
            string k = IsTrue.SelectedValue.ToString();
            k = k.Substring(38);
            if (k.Equals(s))
            {
                a = 1;
            }
            else
            {
                a = 0;
            }

        }
        public void Clear()
        {
            AnswerStudentTest.Clear();
        }
        public void Update()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = $"UPDATE answers_tests SET answer_student='{AnswerStudentTest.Text}',is_true='{a}' WHERE question_id='{id_question}' and user_test_id='{unique_id}'";
                    cm1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }                
            }
        }
        public static int j = 0;
        public static int count = 0;
        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (j < Convert.ToInt32(count))
            {
                IsTr();
                Update();
                AnswerStudentForChange();
                j++;
                CountNow.Content = j.ToString();
            }
            else
            {
                MessageBox.Show("Вы проверили все вопросы");
                Update();
                Next.Visibility = Visibility.Hidden;
                Updates.Visibility = Visibility.Visible;
            }
            //Clear();
        }

        private void UpdateResultsClick(object sender, RoutedEventArgs e)
        {
            int ccc = 0;
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSurname = $"select count(answer_student) from results as r inner join users_tests as us on r.unique_id = us.id inner join tests as t on us.test_id = t.id inner join users as u on us.student_id = u.id inner join subjects as s on t.subject_id=s.id inner join answers_tests as a on us.id=a.user_test_id inner join questions as q on a.question_id=q.id where t.teacher_id = '{id_teacher}' and t.name_of_test='{Name.SelectedValue.ToString()}' and u.middle_name='{Surname.SelectedValue.ToString()}' and u.first_name='{NameStudent.SelectedValue.ToString()}' and r.date='{Date.SelectedValue.ToString()}' and a.is_true='true' and a.user_test_id='{unique_id}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSurname, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        ccc = Convert.ToInt32(reader.GetValue(0));
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
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    float a = (float)count;
                    float b = (float)ccc;
                    float Results = (b / a) * 100;
                    cm1.CommandText = $"UPDATE results SET result='{Results}' WHERE unique_id='{unique_id}'";
                    cm1.ExecuteNonQuery();
                    MessageBox.Show("Успешно обновлено");
                    CheckAnswers.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
        }
    }
}
