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
    /// Логика взаимодействия для Show_questions.xaml
    /// </summary>
    public partial class Info_about_questions : Window
    {

       
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        public static int id_teacher = MainWindow.identry;
        public Info_about_questions()
        {
            InitializeComponent();
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private async void Show_question_clickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter =
 new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer,a.is_true from questions as q inner join answers as a on q.id=a.question_id where q.teacher_id='{id_teacher}'", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void Change_questions_click(object sender, RoutedEventArgs e)
        {
           
            DeleteLabel.Visibility = Visibility.Hidden;
            Line.Visibility = Visibility.Hidden;
            ChangeQuestions chn = new ChangeQuestions();
            chn.Show();
        }

        
        public static int id_question;

        private void Back_click_entry(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
