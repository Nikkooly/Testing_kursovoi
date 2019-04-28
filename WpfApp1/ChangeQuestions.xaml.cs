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
    /// Логика взаимодействия для ChangeQuestions.xaml
    /// </summary>
    public partial class ChangeQuestions : Window
    {
        public ChangeQuestions()
        {
            InitializeComponent();
        }
        private readonly SqlConnection _newcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly DataTable tables = new DataTable();
        public static int id_teacher = entry.identry;
        private async void Show_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Visible;
            User_Grid_theme.Visibility = Visibility.Hidden;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q, answers as a where q.teacher_id='{id_teacher}'", _newcon);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string s;
        public static int id_question;
        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                s = DeleteBox.Text;
                id_question = Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Showid_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Visible;
            User_Grid.Visibility = Visibility.Hidden;
            User_Grid_theme.Visibility = Visibility.Hidden;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter =
 new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q, answers as a where q.teacher_id='{id_teacher}' and q.id in (select id from questions where id = '{id_question}')", _newcon);
                adapter.Fill(tables);
                User_Grid_id.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_click_questionshow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Showtheme_question_clickAsync(object sender, RoutedEventArgs e)
        {
            User_Grid_id.Visibility = Visibility.Hidden;
            User_Grid.Visibility = Visibility.Hidden;
            User_Grid_theme.Visibility = Visibility.Visible;
            try
            {
                tables.Clear();
                await _newcon.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select q.id,q.question,q.theme,a.answer from questions as q, answers as a where q.theme='{str}'", _newcon);
                adapter.Fill(tables);
                User_Grid_theme.DataContext = tables.DefaultView;
                _newcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string str;
        private void x_TextChanged(object sender, TextChangedEventArgs e)
        {
            str = Box.Text;
        }
    }
}
