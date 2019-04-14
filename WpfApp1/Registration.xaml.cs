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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
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

            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox6.Text == "")// || TextBox5.Text == "" || TextBox6.Text == "" || TextBox7.Text == "" || TextBox8.Text == "")
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
                        if(TextBox2.Text.Length<2)
                        {
                            MessageBox.Show("Слишком короткое имя (не менее 2 символов)");
                        }
                        else
                        {
                            if (TextBox6.Text.Length < 4)
                            {
                                MessageBox.Show("Пароль слишком короткий(не менее 4 символов)");
                            }
                            else
                            {
                                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                                using (SqlConnection reg = new SqlConnection(ConnectionString))
                                {
                                    reg.Open();
                                    SqlTransaction transaction = reg.BeginTransaction();
                                    SqlCommand cm1 = reg.CreateCommand();
                                    SqlCommand cm2 = reg.CreateCommand();
                                    
                                    cm1.Transaction = transaction;
                                    try
                                    {
                                        cm1.CommandText = $"INSERT INTO users(first_name,middle_name,login,email,password) VALUES ('{name}', '{surname}','{email}','{login}','{password}');";
                                        //cm2.CommandText= $"INSERT INTO facylties(name) VALUES ('{faculty}');";
                                      
                                        cm1.ExecuteNonQuery();
                                       // cm2.ExecuteNonQuery();
                                        transaction.Commit();
                                        MessageBox.Show("Вы успешно зарегестрированы!");
                                       
                                        Entry mn = new Entry();
                                        mn.Show();
                                        this.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        transaction.Rollback();
                                    }
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
        public static string password;
        private void TextBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = TextBox6.Text;
        }
        public static string faculty;
        private void TextBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            faculty = TextBox5.Text;
        }
    }
}
