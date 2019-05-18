using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Логика взаимодействия для EndTest.xaml
    /// </summary>
    public partial class EndTest : Window
    {
        public static int unique_id = Test.unique_id;
        public EndTest()
        {
            InitializeComponent();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        
        private async void ShowClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tables.Rows.Clear();
                this.tables.Columns.Clear();

                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select q.id,q.question,a.answer_student from questions as q inner join answers_tests as a on q.id=a.question_id where user_test_id='{unique_id}'", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static int id = 0;
        private void FindClick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSubject = $"select q.question,a.answer_student,a.id from answers_tests as a inner join questions as q on a.question_id=q.id where question_id='{Convert.ToInt32(TextBox1.Text)}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSubject, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Question.Text = (string)reader.GetValue(0);
                        TextBox2.Text = (string)reader.GetValue(1);
                        id = Convert.ToInt32(reader.GetValue(2));
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

        private void EndClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {

                        cm1.CommandText = "update answers_tests set answer_student='" + TextBox2.Text + "' where id='" + id + "'";
                        cm1.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно обновлены!");
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
        }
    }
}
