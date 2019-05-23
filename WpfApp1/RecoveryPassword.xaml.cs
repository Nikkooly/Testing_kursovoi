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
using System.Net.Mail;
using System.Web;
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
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }       
        private void recover_click(object sender, TextChangedEventArgs e)
        {

        }
        public static string log="";
        public static string pass="";
        private void Check()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlexp = $"SELECT u.login,u.password,u.email from users as u where u.email='{TextBox1.Text}'";
            using (SqlConnection reg = new SqlConnection(ConnectionString))
            {
                reg.Open();
                SqlCommand command = new SqlCommand(sqlexp, reg);
                SqlDataReader reader = command.ExecuteReader();
                SqlCommand cm1 = reg.CreateCommand();


                while (reader.Read())
                {
                    object x = reader.GetValue(0);
                    object z = reader.GetValue(1);
                    log = (string)x;
                    pass = (string)z;
                }
                reader.Close();
            }
        }
        private void btn_recovery(object sender, RoutedEventArgs e)
        {
            Check();
            if (log == "" && pass=="")
            {
                MessageBox.Show("Данной почты нету в базе. Пожалуйста проверьте данные.");
            }
            else
            {
                    SendEmailAsync(TextBox1.Text, log, pass).GetAwaiter();
                    MessageBox.Show("Письмо успешно отправлено");
                    MainWindow ee = new MainWindow();
                    ee.Show();
                    this.Close();
                    pass = "";
                    log = "";
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
            SmtpClient smtp = new SmtpClient("smtp.yandex.by", 587);
            smtp.Credentials = new NetworkCredential("Wblitz1@yandex.by", "10012000nick");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
    }
}
