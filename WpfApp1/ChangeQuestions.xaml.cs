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
    /// Логика взаимодействия для ChangeQuestions.xaml
    /// </summary>
    public partial class ChangeQuestions : Window
    {
        public ChangeQuestions()
        {
            InitializeComponent();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        public static int id_teacher = entry.identry;
        private async void Show_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Visible;
            User_Grid_theme.Visibility = Visibility.Hidden;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q inner join answers as a on q.id=a.question_id where q.teacher_id='{id_teacher}'", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                s = DeleteBox.Text;
                id_question = Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Showid_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Visible;
            User_Grid.Visibility = Visibility.Hidden;
            User_Grid_theme.Visibility = Visibility.Hidden;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter =
 new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q inner join answers as a on q.id=a.question_id where q.teacher_id='{id_teacher}' and q.id in (select id from questions where id = '{id_question}')", _newcon);
                adapter.Fill(tables);
                User_Grid_id.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Showtheme_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Hidden;
            User_Grid_theme.Visibility = Visibility.Visible;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q inner join answers as a on q.id=a.question_id where q.theme='{str}'", _newcon);
                adapter.Fill(tables);
                User_Grid_theme.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string str;
        private void x_TextChanged(object sender, TextChangedEventArgs e)
        {
            str = Box.Text;
        }

        private void Update_info_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = $"UPDATE questions SET question='{QuestionBox.Text}' WHERE id='{id_question}'";
                    cm1.ExecuteNonQuery();
                    MessageBox.Show("Вопрос успешно обновлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Find_question_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,question FROM questions where id='{id_question}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0).Equals(id_question))
                        {
                            object z = reader.GetValue(1);
                            question = (string)z;
                            QuestionBox.Text = question;                                                                                
                        }
                        else
                        {
                            MessageBox.Show("Вопроса с таким id не существует!");
                            
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
                    connection.Close();
                }
            }
        }

        private void FindId_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                s = IdBox.Text;
                id_question = Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string s;
        public static string ass;
        public static int id_question;
        public static int id_answer1;
        public static int id_answer2;
        public static int id_answer3;
        public static int id_answer4;
        public static string question;
        public static string answers;

        private void Answer_about_question_click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Hidden;
            IdBox.Visibility = Visibility.Visible;
            Line.Visibility = Visibility.Visible;
            IdInput.Visibility = Visibility.Visible;
            InputQuestion.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Visible;
            FindQuestion.Visibility = Visibility.Visible;
            QuestionBox.Visibility = Visibility.Visible;
        }

        private void Change_answers_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,question_id,answer FROM answers where question_id='{id_question}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                   
                        for (int i = 0; reader.Read(); i++)
                        {
                            object a = reader.GetValue(0);
                            object x = reader.GetValue(1);
                            object z = reader.GetValue(2);
                            
                            if (i == 0)
                            {
                                Answer1Box.Text = z.ToString();
                                id_answer1 = (int)a;
                            }
                            if (i == 1)
                            {
                                Answer2Box.Text = z.ToString();
                                id_answer2 = (int)a;
                            }
                            if (i == 2)
                            {
                            Answer3Box.Text = z.ToString();
                                id_answer3 = (int)a;
                                  
                            }
                            if (i == 3)
                            {
                                Answer4Box.Text = z.ToString();
                                id_answer4 = (int)a;                            
                            }
                           
                        }            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show(Answer3Box.Text);
                    reader.Close();
                    connection.Close();
                }
            }
        }

        private void Update_answers_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
