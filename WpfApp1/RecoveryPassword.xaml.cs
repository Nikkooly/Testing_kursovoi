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
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Net.Mime;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RecoveryPassword.xaml
    /// </summary>
    public partial class RecoveryPassword : Window
    {
        public RecoveryPassword()
        {
            InitializeComponent();
        }

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            MainWindow mww = new MainWindow();
            mww.Show();
            this.Close();
        }

        private void Close_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registration_click_forgotpassord(object sender, RoutedEventArgs e)
        {
            Registration rgg = new Registration();
            rgg.Show();
            this.Close();
        }

        private void Entry_click_forgotpassword(object sender, RoutedEventArgs e)
        {
            Entry entr = new Entry();
            entr.Show();
            this.Close();
        }       
        private void recover_click(object sender, TextChangedEventArgs e)
        {

        }
        public static string log;
        public static string pass;
        private void btn_recovery(object sender, RoutedEventArgs e)
        {
            try
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlexp = "SELECT u.login,u.password,u.email from users as u";
                using (SqlConnection reg = new SqlConnection(ConnectionString))
                {
                    reg.Open();
                    SqlCommand command = new SqlCommand(sqlexp, reg);
                    SqlDataReader reader = command.ExecuteReader();
                    SqlCommand cm1 = reg.CreateCommand();
                    bool b = false;


                    while (reader.Read())
                    {
                        if (reader.GetValue(2).Equals(TextBox1.Text))
                        {
                            b = true;
                            object x = reader.GetValue(0);
                            object z = reader.GetValue(1);
                            log = (string)x;
                            pass = (string)z;
                        }
                        break;
                    }
                    if (!b)
                    {
                        MessageBox.Show("Данной почты нету в базе. Пожалуйста проверьте данные.");
                        reader.Close();
                        reg.Close();
                        return;
                    }
                    reader.Close();
                }
                    SendEmailAsync(TextBox1.Text, log, pass).GetAwaiter();
                    MessageBox.Show("Письмо успешно отправлено");
                    MessageBox.Show(TextBox1.Text);
                    MessageBox.Show(log);
                    MessageBox.Show(pass);
                    Entry ee = new Entry();
                    ee.Show();                    
                    this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                                          
        }
        private static async Task SendEmailAsync(string text,string login,string password)
        {

            MailAddress from = new MailAddress("Wblitz1@yandex.by", "TestingStudents");
            MailAddress to = new MailAddress(text);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Восстановление пароля";
            m.Body = $"Ваш логин {login} ваш пароль {password}.</br> Удалите это сообщение!.</br> Хорошего дня =)";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("Wblitz1@yandex.by", "mypassword");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
    }
}
