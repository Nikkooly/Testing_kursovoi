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
        //------------------------------------------------------Student--------------------------------------------------------------------------//

        private async void Students_click(object sender, RoutedEventArgs e)
        {
            
            AdminRules.Visibility = Visibility.Hidden;
            Info.Visibility = Visibility.Hidden;
            IdFind.Visibility = Visibility.Hidden;
            ThemeFindFaculty.Visibility = Visibility.Hidden;
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
            AdminRules.Visibility = Visibility.Hidden;
            Id_Faculty_Delete.Visibility = Visibility.Hidden;
            Faculty_Delete.Visibility = Visibility.Hidden;
            Insert_Faculty.Visibility = Visibility.Hidden;
            Change_Faculty_Delete.Visibility = Visibility.Hidden;
            Theme_Faculty_Delete.Visibility = Visibility.Hidden;
            ThemeFindFaculty.Visibility = Visibility.Hidden;
            //---------------------------------------------------//
            FindStudent.Visibility = Visibility.Visible;
            FindSurnameStudentBox.Visibility = Visibility.Visible;
            FindIdStudentBox.Visibility = Visibility.Visible;
            FindSurnameStudent.Visibility = Visibility.Visible;
            FindIdTeacher.Visibility = Visibility.Hidden;
            FindSurnameTeacher.Visibility = Visibility.Hidden;
            ChangeStudentLabel.Visibility = Visibility.Visible;
            ChangeStudentBox.Visibility = Visibility.Visible;
            ChangeTeacher.Visibility = Visibility.Hidden;
            FindIdStudent.Visibility = Visibility.Visible;
            ChangeStudent.Visibility = Visibility.Visible;
            LineVertical.Visibility = Visibility.Visible;
            SurnameLabel.Visibility = Visibility.Hidden;
            SurnameBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            LoginLabel.Visibility = Visibility.Hidden;
            LoginBox.Visibility = Visibility.Hidden;
            PostLabel.Visibility = Visibility.Hidden;
            PostBox.Visibility = Visibility.Hidden;
            FacultyLabel.Visibility = Visibility.Hidden;
            FacultyBox.Visibility = Visibility.Hidden;
            UpdateTeacher.Visibility = Visibility.Hidden;
            UpdateStudent.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Hidden;
            User1_Grid.Visibility = Visibility.Visible;

            try
            {
                this.tables.Rows.Clear();                             
                this.tables.Columns.Clear();
                
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select u.id,u.first_name,u.middle_name,f.name,u.login,u.email FROM users as u inner join facylties as f on u.faculty_id=f.id where role_id='{2}'", _newcon);
                adapter.Fill(tables);
                User1_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FindIdStudentClick(object sender, RoutedEventArgs e)
        {           
            int id_student = Int32.Parse(FindIdStudentBox.Text);            
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT u.first_name,u.middle_name,f.name,u.id FROM users as u inner join facylties as f on u.faculty_id=f.id where u.id='{id_student}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object a = reader.GetValue(0);
                        object x = reader.GetValue(1);
                        object z = reader.GetValue(2);
                        object s = reader.GetValue(3);
                        id_admin_panel = Int32.Parse(s.ToString());
                        ChangeStudentBox.Text = a.ToString() + " " + x.ToString() + " " + z.ToString();
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
        public static int id_admin_panel; 
        private void FindSurnameStudentClick(object sender, RoutedEventArgs e)
        {            
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT u.first_name,u.middle_name,f.name,u.id FROM users as u inner join facylties as f on u.faculty_id=f.id where u.first_name='{FindSurnameStudentBox.Text}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object a = reader.GetValue(0);
                        object x = reader.GetValue(1);
                        object z = reader.GetValue(2);
                        object s = reader.GetValue(3);
                        id_admin_panel = Int32.Parse(s.ToString());
                        ChangeStudentBox.Text = a.ToString() + " " + x.ToString() + " " + z.ToString();
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

        private void ChangeStudentClick(object sender, RoutedEventArgs e)
        {
            SurnameLabel.Visibility = Visibility.Visible;
            SurnameBox.Visibility = Visibility.Visible;
            NameLabel.Visibility = Visibility.Visible;
            NameBox.Visibility = Visibility.Visible;
            LoginLabel.Visibility = Visibility.Visible;
            LoginBox.Visibility = Visibility.Visible;
            PostLabel.Visibility = Visibility.Visible;
            PostBox.Visibility = Visibility.Visible;
            FacultyLabel.Visibility = Visibility.Visible;
            FacultyBox.Visibility = Visibility.Visible;
            UpdateTeacher.Visibility = Visibility.Hidden;
            UpdateStudent.Visibility = Visibility.Visible;
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlExpression = $"SELECT u.first_name,u.middle_name,u.login,u.email,f.name FROM users as u inner join facylties as f on u.faculty_id=f.id where u.id='{id_admin_panel}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {

                    while (reader.Read()) { 
                            object a = reader.GetValue(0);
                            object x = reader.GetValue(1);
                            object z = reader.GetValue(2);
                            object s = reader.GetValue(2);
                            object k = reader.GetValue(4);
                           
                                SurnameBox.Text = a.ToString();                          
                                NameBox.Text = x.ToString();                           
                                LoginBox.Text = z.ToString();
                                PostBox.Text = s.ToString();                          
                                FacultyBox.Text = k.ToString();
                           

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
        public static int fakk_id;
        public static string faculty;
        private void Fak(object sender, TextChangedEventArgs e)
        {
            faculty = FacultyBox.Text;
        }
        private void UpdateStudentClick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name From facylties where name='{faculty}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    
                    while (reader.Read())
                    {
                        
                        object s = reader.GetValue(0);
                        fakk_id = Convert.ToInt32(s);                            
                    }
                    reader.Close();
                    if (fakk_id == 0)
                    {
                        MessageBox.Show("Такого факультета не существует");                       
                    }
                    else
                    {
                        cm1.CommandText = "update users set first_name='" + SurnameBox.Text + "',middle_name='" + NameBox.Text + "',login='" + LoginBox.Text + "',email='" + PostBox.Text + "',faculty_id='" + fakk_id + "' where id='" + id_admin_panel + "'";
                        cm1.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SurnameBox.Clear();
                    NameBox.Clear();
                    LoginBox.Clear();
                    PostBox.Clear();
                    FacultyBox.Clear();
                    connection.Close();
                    reader.Close();
                }
            }
        }
        //------------------------------------------------------Teacher--------------------------------------------------------------------------//

        private async void Teacher_click(object sender, RoutedEventArgs e)
        {
            
            AdminRules.Visibility = Visibility.Hidden;
            Info.Visibility = Visibility.Hidden;
            IdFind.Visibility = Visibility.Hidden;
            ThemeFindFaculty.Visibility = Visibility.Hidden;
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
            AdminRules.Visibility = Visibility.Hidden;
            Id_Faculty_Delete.Visibility = Visibility.Hidden;
            Faculty_Delete.Visibility = Visibility.Hidden;
            Insert_Faculty.Visibility = Visibility.Hidden;
            Change_Faculty_Delete.Visibility = Visibility.Hidden;
            Theme_Faculty_Delete.Visibility = Visibility.Hidden;
            ThemeFindFaculty.Visibility = Visibility.Hidden;
            //---------------------------------------------------//
            FindStudent.Visibility = Visibility.Visible;
            FindIdStudentBox.Visibility = Visibility.Visible;
            FindSurnameStudent.Visibility = Visibility.Hidden;
            FindIdTeacher.Visibility = Visibility.Visible;
            FindSurnameTeacher.Visibility = Visibility.Visible;
            ChangeStudentLabel.Visibility = Visibility.Visible;
            ChangeStudentBox.Visibility = Visibility.Visible;
            ChangeTeacher.Visibility = Visibility.Visible;
            FindIdStudent.Visibility = Visibility.Hidden;
            ChangeStudent.Visibility = Visibility.Hidden;
            LineVertical.Visibility = Visibility.Visible;
            FindSurnameStudentBox.Visibility = Visibility.Visible;
            SurnameLabel.Visibility = Visibility.Hidden;
            SurnameBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            LoginLabel.Visibility = Visibility.Hidden;
            LoginBox.Visibility = Visibility.Hidden;
            PostLabel.Visibility = Visibility.Hidden;
            PostBox.Visibility = Visibility.Hidden;
            FacultyLabel.Visibility = Visibility.Hidden;
            FacultyBox.Visibility = Visibility.Hidden;
            UpdateTeacher.Visibility = Visibility.Hidden;
            UpdateStudent.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Hidden;
            User1_Grid.Visibility = Visibility.Visible;

            try
            {

                this.tables.Rows.Clear();
                this.tables.Columns.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select u.id,u.first_name,u.middle_name,f.name,u.login,u.email FROM users as u inner join facylties as f on u.faculty_id=f.id where role_id='{1}'", _newcon);
                adapter.Fill(tables);
                User1_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FindIdTeacherClick(object sender, RoutedEventArgs e)
        {
            int id_teacher = Int32.Parse(FindIdStudentBox.Text);
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT u.first_name,u.middle_name,f.name,u.id FROM users as u inner join facylties as f on u.faculty_id=f.id where u.id='{id_teacher}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object a = reader.GetValue(0);
                        object x = reader.GetValue(1);
                        object z = reader.GetValue(2);
                        object s = reader.GetValue(3);
                        id_admin_panel = Int32.Parse(s.ToString());
                        ChangeStudentBox.Text = a.ToString() + " " + x.ToString() + " " + z.ToString();
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

        private void FindSurnameTeacherClick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT u.first_name,u.middle_name,f.name,u.id FROM users as u inner join facylties as f on u.faculty_id=f.id where u.first_name='{FindSurnameStudentBox.Text}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object a = reader.GetValue(0);
                        object x = reader.GetValue(1);
                        object z = reader.GetValue(2);
                        object s = reader.GetValue(3);
                        id_admin_panel = Int32.Parse(s.ToString());
                        ChangeStudentBox.Text = a.ToString() + " " + x.ToString() + " " + z.ToString();
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
        private void ChangeTeacherClick(object sender, RoutedEventArgs e)
        {
            SurnameLabel.Visibility = Visibility.Visible;
            SurnameBox.Visibility = Visibility.Visible;
            NameLabel.Visibility = Visibility.Visible;
            NameBox.Visibility = Visibility.Visible;
            LoginLabel.Visibility = Visibility.Visible;
            LoginBox.Visibility = Visibility.Visible;
            PostLabel.Visibility = Visibility.Visible;
            PostBox.Visibility = Visibility.Visible;
            FacultyLabel.Visibility = Visibility.Visible;
            FacultyBox.Visibility = Visibility.Visible;
            UpdateTeacher.Visibility = Visibility.Visible;
            UpdateStudent.Visibility = Visibility.Hidden;

            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT u.first_name,u.middle_name,u.login,u.email,f.name FROM users as u inner join facylties as f on u.faculty_id=f.id where u.id='{id_admin_panel}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    while (reader.Read())
                    {
                        object a = reader.GetValue(0);
                        object x = reader.GetValue(1);
                        object z = reader.GetValue(2);
                        object s = reader.GetValue(2);
                        object k = reader.GetValue(4);

                        SurnameBox.Text = a.ToString();
                        NameBox.Text = x.ToString();
                        LoginBox.Text = z.ToString();
                        PostBox.Text = s.ToString();
                        FacultyBox.Text = k.ToString();


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
        private void UpdateTeacherClick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"SELECT id,name From facylties where name='{faculty}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {

                    while (reader.Read())
                    {

                        object s = reader.GetValue(0);
                        fakk_id = Convert.ToInt32(s);
                    }
                    reader.Close();
                    if (fakk_id == 0)
                    {
                        MessageBox.Show("Такого факультета не существует");
                    }
                    else
                    {
                        cm1.CommandText = "update users set first_name='" + SurnameBox.Text + "',middle_name='" + NameBox.Text + "',login='" + LoginBox.Text + "',email='" + PostBox.Text + "',faculty_id='" + fakk_id + "' where id='" + id_admin_panel + "'";
                        cm1.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SurnameBox.Clear();
                    NameBox.Clear();
                    LoginBox.Clear();
                    PostBox.Clear();
                    FacultyBox.Clear();
                    connection.Close();
                    reader.Close();
                }
            }
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
            //-------------------------------------------------------//
            FindStudent.Visibility = Visibility.Hidden;
            FindIdStudentBox.Visibility = Visibility.Hidden;
            FindSurnameStudent.Visibility = Visibility.Hidden;
            FindIdTeacher.Visibility = Visibility.Hidden;
            FindSurnameTeacher.Visibility = Visibility.Hidden;
            ChangeStudentLabel.Visibility = Visibility.Hidden;
            ChangeStudentBox.Visibility = Visibility.Hidden;
            ChangeTeacher.Visibility = Visibility.Hidden;
            FindIdStudent.Visibility = Visibility.Hidden;
            ChangeStudent.Visibility = Visibility.Hidden;
            LineVertical.Visibility = Visibility.Hidden;
            FindSurnameStudentBox.Visibility = Visibility.Hidden;
            SurnameLabel.Visibility = Visibility.Hidden;
            SurnameBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            LoginLabel.Visibility = Visibility.Hidden;
            LoginBox.Visibility = Visibility.Hidden;
            PostLabel.Visibility = Visibility.Hidden;
            PostBox.Visibility = Visibility.Hidden;
            FacultyLabel.Visibility = Visibility.Hidden;
            FacultyBox.Visibility = Visibility.Hidden;
            UpdateTeacher.Visibility = Visibility.Hidden;
            UpdateStudent.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Visible;
            User1_Grid.Visibility = Visibility.Hidden;
            try
            {

                this.tables.Rows.Clear(); 
                this.tables.Columns.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select id,name FROM facylties", _newcon);
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
            //---------------------------------------------------//
            FindStudent.Visibility = Visibility.Hidden;
            FindIdStudentBox.Visibility = Visibility.Hidden;
            FindSurnameStudent.Visibility = Visibility.Hidden;
            FindIdTeacher.Visibility = Visibility.Hidden;
            FindSurnameTeacher.Visibility = Visibility.Hidden;
            ChangeStudentLabel.Visibility = Visibility.Hidden;
            ChangeStudentBox.Visibility = Visibility.Hidden;
            ChangeTeacher.Visibility = Visibility.Hidden;
            FindIdStudent.Visibility = Visibility.Hidden;
            ChangeStudent.Visibility = Visibility.Hidden;
            LineVertical.Visibility = Visibility.Hidden;
            FindSurnameStudentBox.Visibility = Visibility.Hidden;
            SurnameLabel.Visibility = Visibility.Hidden;
            SurnameBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Hidden;
            LoginLabel.Visibility = Visibility.Hidden;
            LoginBox.Visibility = Visibility.Hidden;
            PostLabel.Visibility = Visibility.Hidden;
            PostBox.Visibility = Visibility.Hidden;
            FacultyLabel.Visibility = Visibility.Hidden;
            FacultyBox.Visibility = Visibility.Hidden;
            UpdateTeacher.Visibility = Visibility.Hidden;
            UpdateStudent.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Visible;
            User1_Grid.Visibility = Visibility.Hidden;
            try
            {

                this.tables.Rows.Clear();  
                this.tables.Columns.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select id,name FROM subjects", _newcon);
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
