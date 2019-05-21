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
    /// Логика взаимодействия для Entry.xaml
    /// </summary>
    public partial class entry : Window
    {

        public entry()
        {
            InitializeComponent();
            TextBox1.Text = "romaKsis";
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            MainWindow mwn = new MainWindow();
            mwn.Show();
            this.Close();
        }

        private void Close_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Forgot_click_entry(object sender, RoutedEventArgs e)
        {
            RecoveryPassword rp = new RecoveryPassword();
            rp.Show();
            this.Close();
        }
        public static int id = 0;
        public static string pass;
        public static string log;
        public static string people;
        public static int identry;
        
        private void Entry_btn_entry_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text.Equals("") || passwords.Password.Equals(""))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            else
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlExpression = "SELECT login,password,role_id,first_name,middle_name,id FROM users";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    bool b = false;
                    bool c = false;
                    while (reader.Read())
                    {

                        if (reader.GetValue(0).Equals(TextBox1.Text))
                        {
                            b = true;
                            if (reader.GetValue(1).Equals(passwords.Password))
                            {
                                c= true;
                                object s = reader.GetValue(2);
                                id = (int)s;
                                object ident = reader.GetValue(5);
                                identry = (int)ident;
                                object surnam = reader.GetValue(3);
                                object nam = reader.GetValue(4);
                                people = String.Concat((string)surnam+" ", (string)nam);
                                
                                if (id == 3)
                                {
                                    Admin_panel adp = new Admin_panel();
                                    adp.Show();
                                    this.Close();
                                }
                                if (id == 2)
                                {
                                    StudentPanel ss = new StudentPanel();
                                    ss.Show();
                                    this.Close();
                                }
                                if (id == 1)
                                {
                                    Teacher_panel tp = new Teacher_panel();
                                    tp.Show();
                                    this.Close();
                                }
                            }                                                      
                         break;
                        }
                    }
                    if (!b)
                    {
                        MessageBox.Show("Такого логина не существует! Проверьте введенные данные.");
                        reader.Close();
                        connection.Close();
                        return;
                    }
                    if (!c)
                    {
                        MessageBox.Show("Пароль неправильный! Проверьте введенные данные.");
                        reader.Close();
                        connection.Close();
                        return;
                    }
                    reader.Close();
                    connection.Close();
                }
            }                       
        }
    }
    
}
