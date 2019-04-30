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
    /// Логика взаимодействия для Show_questions.xaml
    /// </summary>
    public partial class Info_about_questions : Window
    {

       
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        public static int id_teacher = entry.identry;
        public Info_about_questions()
        {
            InitializeComponent();
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Show_question_clickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter =
 new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer,a.is_true from questions as q inner join answers as a on q.id=a.question_id where q.teacher_id='{id_teacher}'", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_question_click(object sender, RoutedEventArgs e)
        {
            Delete.Visibility = Visibility.Visible;
            DeleteLabel.Visibility = Visibility.Visible;
            DeleteBox.Visibility = Visibility.Visible;
            Line.Visibility = Visibility.Visible;
        }

        private void Change_questions_click(object sender, RoutedEventArgs e)
        {
            Delete.Visibility = Visibility.Hidden;
            DeleteLabel.Visibility = Visibility.Hidden;
            DeleteBox.Visibility = Visibility.Hidden;
            Line.Visibility = Visibility.Hidden;
            ChangeQuestions chn = new ChangeQuestions();
            chn.Show();
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlExpression = $"DELETE FROM questions WHERE id = {id_question};";

                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, cn);
                    command.BeginExecuteNonQuery();
                    MessageBox.Show("Вопрос успешно удален!");
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static string s;
        public static int id_question;
        private void DeleteBox_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
