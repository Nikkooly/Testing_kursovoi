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
    /// Логика взаимодействия для Admin_panel.xaml
    /// </summary>
    public partial class Admin_panel : Window
    {
        public Admin_panel()
        {
            InitializeComponent();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        private void Students_click(object sender, RoutedEventArgs e)
        {

        }

        private void Teacher_click(object sender, RoutedEventArgs e)
        {

        }

        private void Fak_click(object sender, RoutedEventArgs e)
        {

        }

        private async void Subjects_click(object sender, RoutedEventArgs e)
        {           
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * FROM subjects", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static int FindId;
        private void Find_Id_click(object sender, RoutedEventArgs e)
        {
            FindId = Convert.ToInt32(FindIdBox.Text);
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name FROM subjects where id='{FindId}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object s = reader.GetValue(1);                        
                        DeleteSubjectBox.Text= (string)s;
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

        private void Find_Theme_click(object sender, RoutedEventArgs e)
        {

        }

        private void Subject_Delete_click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Subject_click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }
    }
}
