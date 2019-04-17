using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data.Linq;
//using System.Linq;
//using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    /// 
    
    public partial class Registration : Window
    {
     // SqlConnection reg = new SqlConnection(@"Data Source=DESKTOP-15P21ID; Initial Catalog=kursovoi; Integrated Sequrity=True;");
        public Registration()
        {
            InitializeComponent();
        }

        private void Close_click_registration(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void Back_click_registration(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Registration_click_reg(object sender, RoutedEventArgs e)
        {
            string pass=password.Password;
            string passchecked = PasswordChecked.Password;
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == ""  || TextBox5.Text == ""  || TextBox8.Text == "")
            {
                MessageBox.Show("Пожалуйста заполните все поля");
            }
            else
            {
                if (TextBox1.Text.Length > 25)
                {
                    MessageBox.Show("Слишком длинное имя (не более 25 символов). Проверьте введенные данные");
                }
                else
                {
                    if(TextBox3.Text.Length>20 || TextBox3.Text.Length<3)
                    {
                        MessageBox.Show("Некорректный логин (не более 20 символов и не менее 3)");
                    }
                    else
                    {
                        if(pass.Length<2 || pass.Length>20)
                        {
                            MessageBox.Show("Пароль должен состоять как минимум из 2 символов");
                        }
                        else {
                            if (TextBox2.Text.Length < 2)
                            {
                                MessageBox.Show("Слишком короткое имя (не менее 2 символов)");
                            }
                            else
                            {
                                if (!pass.Equals(passchecked))
                                {
                                    MessageBox.Show("Пароли не совпадают");
                                }
                                else
                                {
                                    ////if (!TextBox8.Text.Equals("Student") || !TextBox8.Text.Equals("Teacher"))
                                    ////{
                                    ////    MessageBox.Show("В этом поле можно ввести только Student или Teacher "+"X" +TextBox8.Text+"X");
                                    ////}
                                    ////else
                                    ////{
                                        string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                                        string sqlexp = "SELECT f.id,f.name,r.id,r.name from facylties as f,roles as r";
                                        using (SqlConnection reg = new SqlConnection(ConnectionString))
                                        {
                                            reg.Open();
                                            SqlCommand command = new SqlCommand(sqlexp, reg);
                                            SqlDataReader reader = command.ExecuteReader();
                                            SqlCommand cm1 = reg.CreateCommand();

                                            try
                                            {
                                                int fak = 0;
                                                int rol = 0;
                                                while (reader.Read())
                                                {
                                                    if (reader.GetValue(1).Equals(TextBox5.Text) && reader.GetValue(3).Equals(TextBox8.Text))
                                                    {
                                                        object x = reader.GetValue(0);
                                                        object z = reader.GetValue(2);
                                                        fak = (int)x;
                                                        rol = (int)z;
                                                    }

                                                }
                                                reader.Close();
                                                cm1.CommandText = $"INSERT INTO users(first_name, middle_name, login, email, password, faculty_id,role_id) VALUES ('{surname}', '{name}','{login}','{email}','{pass}', '{fak}', '{rol}');";
                                                cm1.ExecuteNonQuery();


                                                MessageBox.Show("Вы успешно зарегестрированы!");

                                                Entry mn = new Entry();
                                                mn.Show();
                                                this.Close();
                                            }
                                            catch (Exception ex)
                                            {

                                                MessageBox.Show(ex.Message);

                                            }
                                            reader.Close();

                                        }
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("В данное поле можно вводить только буквы.");
            }
        }
        public static string name;
        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = TextBox1.Text;
        }
        public static string surname;
        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname = TextBox2.Text;
        }
        public static string login;
        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            login = TextBox3.Text;
        }
        public static string email;
        private void TextBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = TextBox4.Text;
        }
        public static string faculty;
        private void TextBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            faculty = TextBox5.Text;
        }
        
        public static string role;
        private void TextBox8_TextChanged(object sender, TextChangedEventArgs e)
        {
            role = TextBox8.Text;
        }
    }
}
