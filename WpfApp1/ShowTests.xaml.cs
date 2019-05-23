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
    /// Логика взаимодействия для ShowTests.xaml
    /// </summary>
    public partial class ShowTests : Window
    {
        public static int id_teacher = MainWindow.identry;
        public ShowTests()
        {
            InitializeComponent();
            NameShow();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ShowResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.theme,t.name_of_test,t.time,t.date from tests as t inner join subjects as s on t.subject_id=s.id where  t.teacher_id = '{id_teacher}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }
        private void NameShow()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"select DISTINCT t.name_of_test from tests as t inner join subjects as s on t.subject_id=s.id where t.teacher_id='{id_teacher}'";
            string sqlSurname = $"select DISTINCT s.name from tests as t inner join subjects as s on t.subject_id=s.id where t.teacher_id='{id_teacher}'";
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
            private async void FindNameResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.theme,t.name_of_test,t.time,t.date from tests as t inner join subjects as s on t.subject_id=s.id where  t.teacher_id = '{id_teacher}' and t.name_of_test='{TestName.SelectedValue.ToString()}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }

        private async void FindSubjectResultClick(object sender, RoutedEventArgs e)
        {
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter($"select s.name,t.theme,t.name_of_test,t.time,t.date from tests as t inner join subjects as s on t.subject_id=s.id where  t.teacher_id = '{id_teacher}' and s.name='{SurnameName.SelectedValue.ToString()}'", _newcon);
            adapter.Fill(tables);
            User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }
    }
}
