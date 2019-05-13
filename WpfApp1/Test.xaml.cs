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
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public static string s;
        public static string checkName;
        public Test()
        {
            InitializeComponent();            
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSubject = $"select * from subjects";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSubject, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        SubjectList.Items.Add((string)reader.GetValue(1));
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
            CheckClose chec = new CheckClose();
            chec.Show();
        }
        public static int time = 0;
        public static string name = "";
        public static string subject = "";
        public static string theme = "";
        public static string teacher = "";
        public static string info = "";
        private void PassTestclick(object sender, RoutedEventArgs e)
        {
            //string checkSubject = SubjectList.SelectedValue.ToString();
            
            if(SubjectList.Text=="" || NameList.Text == "")
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSubject = $"select t.name_of_test,s.name,t.theme,u.first_name,u.middle_name,t.time,t.info from tests as t inner join subjects as s on t.subject_id=s.id inner join users as u on t.teacher_id=u.id where t.name_of_test='{checkName}' and t.subject_id='{id_subj}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlSubject, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            name = (string)reader.GetValue(0);
                            subject = (string)reader.GetValue(1);
                            theme = (string)reader.GetValue(2);
                            teacher = (string)reader.GetValue(3) +" "+ (string)reader.GetValue(4);
                            info = (string)reader.GetValue(6);
                            time = Convert.ToInt32(reader.GetValue(5));
                        }
                        //reader.Close();
                        NameOf.Content = name;
                        SubjectOf.Content = subject;
                        ThemeOf.Content = theme;
                        TeacherOf.Content = teacher;
                        InfoOf.Content = info;
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

        private void StartTestclick(object sender, RoutedEventArgs e)
        {
            TestStudent.Visibility = Visibility.Hidden;
        }
      
        public static int id_subj;
        private void ComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {

            s = SubjectList.SelectedValue.ToString();
            try
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSubject = $"select * from subjects where name='{s}'";
                string sqlName = $"select name_of_test,subject_id from tests";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlSubject, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            if (s.Equals(reader.GetValue(1)))
                            {
                                id_subj = Convert.ToInt32(reader.GetValue(0));
                            }
                            else
                            {
                                MessageBox.Show("ошибка ");
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
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlName, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            if (id_subj.Equals(reader.GetValue(1)))
                            {
                                NameList.Items.Add(reader.GetValue(0));
                            }
                            else
                            {
                                MessageBox.Show("Ошибка! По данному предмету нету тестов");
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NameSelected(object sender, SelectionChangedEventArgs e)
        {
            checkName = NameList.SelectedValue.ToString();
        }

        private void Back_click_question(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
