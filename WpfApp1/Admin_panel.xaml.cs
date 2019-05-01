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
            Info.Visibility = Visibility.Hidden;
            IdFind.Visibility = Visibility.Hidden;
            ThemeFind.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Hidden;
            FindIdBox.Visibility = Visibility.Hidden;
            FindThemeBox.Visibility = Visibility.Hidden;
            Id_Subjects_Delete.Visibility = Visibility.Hidden;
            Theme_Subjects_Delete.Visibility = Visibility.Hidden;
            DeleteSubjectBox.Visibility = Visibility.Hidden;
            Insert_Subject.Visibility = Visibility.Hidden;
            Subject_Delete.Visibility = Visibility.Hidden;
            Change_Subject_Delete.Visibility = Visibility.Hidden;
            Znak.Visibility = Visibility.Hidden;
            AdminRules.Visibility = Visibility.Visible;
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        private void Students_click(object sender, RoutedEventArgs e)
        {

        }

        private void Teacher_click(object sender, RoutedEventArgs e)
        {

        }
        //------------------------------------------------------Faculty--------------------------------------------------------------------------//

        private async void Fak_click(object sender, RoutedEventArgs e)
        {
            Info.Visibility = Visibility.Visible;
            IdFind.Visibility = Visibility.Visible;
            ThemeFindFaculty.Visibility = Visibility.Visible;
            ThemeFind.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Visible;
            FindIdBox.Visibility = Visibility.Visible;
            FindThemeBox.Visibility = Visibility.Visible;
            Id_Subjects_Delete.Visibility = Visibility.Visible;
            Theme_Subjects_Delete.Visibility = Visibility.Visible;
            DeleteSubjectBox.Visibility = Visibility.Visible;
            Insert_Subject.Visibility = Visibility.Hidden;
            Subject_Delete.Visibility = Visibility.Hidden;
            Change_Subject_Delete.Visibility = Visibility.Hidden;
            Znak.Visibility = Visibility.Visible;
            AdminRules.Visibility = Visibility.Hidden;
            Id_Faculty_Delete.Visibility = Visibility.Visible;
            Faculty_Delete.Visibility = Visibility.Visible;
            Insert_Faculty.Visibility = Visibility.Visible;
            Change_Faculty_Delete.Visibility = Visibility.Visible;
            Theme_Faculty_Delete.Visibility = Visibility.Visible;
            ThemeFindFaculty.Visibility = Visibility.Visible;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * FROM facylties", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static int fak_id;
        private void Find_Id_Faculty_click(object sender, RoutedEventArgs e)
        {
            FindId = Convert.ToInt32(FindIdBox.Text);
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name FROM facylties where id='{FindId}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object k = reader.GetValue(0);
                        object s = reader.GetValue(1);
                        DeleteSubjectBox.Text = (string)s;
                        fak_id = (int)k;
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
        private void Find_Faculty_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = "SELECT id,name FROM facylties where name='" + FindThemeBox.Text + "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object k = reader.GetValue(0);
                        object s = reader.GetValue(1);
                        fak_id = (int)k;
                        DeleteSubjectBox.Text = (string)s;
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

        private void Faculty_Insert_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name From facylties";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand cm1 = connection.CreateCommand();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(1).Equals(DeleteSubjectBox.Text))
                        {
                            object k = reader.GetValue(0);
                            object s = reader.GetValue(1);
                            fak_id = Convert.ToInt32(k);
                            insert = (string)s;
                        }
                    }
                    reader.Close();
                    if (insert != DeleteSubjectBox.Text)
                    {
                        cm1.CommandText = $"INSERT INTO facylties(name) VALUES ('{DeleteSubjectBox.Text}');";
                        cm1.ExecuteNonQuery();
                        MessageBox.Show("Факультет успешно добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Такой Факультет уже существует");
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

        private void Faculty_Delete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlExpression = "delete from facylties where name='" + DeleteSubjectBox.Text + "'";

                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, cn);
                    command.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Факультет успешно удален!");
            }
        }

        private void Change_Faculty_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = "update facylties set name='" + DeleteSubjectBox.Text + "'where id='" + fak_id + "'";
                    cm1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Предмет успешно изменен");
                    connection.Close();
                }
            }
        }
        //------------------------------------------------------Subjects--------------------------------------------------------------------------//
        private async void Subjects_click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Info.Visibility = Visibility.Visible;
            IdFind.Visibility = Visibility.Visible;
            ThemeFind.Visibility = Visibility.Visible;
            Result.Visibility = Visibility.Visible;
            FindIdBox.Visibility = Visibility.Visible;
            FindThemeBox.Visibility = Visibility.Visible;
            Id_Subjects_Delete.Visibility = Visibility.Visible;
            Theme_Subjects_Delete.Visibility = Visibility.Visible;
            DeleteSubjectBox.Visibility = Visibility.Visible;
            Insert_Subject.Visibility = Visibility.Visible;
            Subject_Delete.Visibility = Visibility.Visible;
            Change_Subject_Delete.Visibility = Visibility.Visible;
            Znak.Visibility = Visibility.Visible;
            AdminRules.Visibility = Visibility.Hidden;
            Id_Faculty_Delete.Visibility = Visibility.Hidden;
            Faculty_Delete.Visibility = Visibility.Hidden;
            Insert_Faculty.Visibility = Visibility.Hidden;
            Change_Faculty_Delete.Visibility = Visibility.Hidden;
            Theme_Faculty_Delete.Visibility = Visibility.Hidden;
            ThemeFindFaculty.Visibility = Visibility.Hidden;
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
        public static int id_subject;
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
                        object k = reader.GetValue(0);
                        object s = reader.GetValue(1);                        
                        DeleteSubjectBox.Text= (string)s;
                        id_subj = (int)k;
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
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = "SELECT id,name FROM subjects where name='"+FindThemeBox.Text+"'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object k = reader.GetValue(0);
                        object s = reader.GetValue(1);
                        id_subj = (int)k;
                        DeleteSubjectBox.Text = (string)s;
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

        private void Subject_Delete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlExpression = "delete from subjects where name='" + DeleteSubjectBox.Text + "'";

                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, cn);
                    command.ExecuteNonQuery();                    
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Предмет успешно удален!");
            }
        }

        private void Change_Subject_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = "update subjects set name='" + DeleteSubjectBox.Text + "'where id='" + id_subj + "'";
                    cm1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Предмет успешно изменен");
                    connection.Close();
                }
            }
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void Subject_Insert_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name From subjects";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand cm1 = connection.CreateCommand();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(1).Equals(DeleteSubjectBox.Text))
                        {
                            object k = reader.GetValue(0);
                            object s = reader.GetValue(1);
                            id_subj = Convert.ToInt32(k);
                            insert = (string)s;
                        }
                    }
                    reader.Close();
                    if (insert != DeleteSubjectBox.Text)
                    {
                        cm1.CommandText = $"INSERT INTO subjects(name) VALUES ('{DeleteSubjectBox.Text}');";
                        cm1.ExecuteNonQuery();
                        MessageBox.Show("Предмет успешно добавлен");
                    }
                    else
                        {
                            MessageBox.Show("Такой предмет уже существует");
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
        public static string insert;
        public static int id_subj;

        
    }
}
