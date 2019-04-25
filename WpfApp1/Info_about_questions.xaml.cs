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
        public Info_about_questions()
        {
            InitializeComponent();
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Show_question_clickAsync(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            tables.Clear();
            await _newcon.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter("Select q.question,q.theme,a.answers,a.is_true from questions as q,ansewers as a", _newcon);
            adapter.Fill(tables);
           User_Grid.DataContext = tables.DefaultView;
            _newcon.Close();
        }

        private void Delete_question_click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_questions_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
