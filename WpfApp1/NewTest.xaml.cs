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
    /// Логика взаимодействия для NewTest.xaml
    /// </summary>
    public partial class NewTest : Window
    {
        public static int id_teacher = entry.identry;
        public NewTest()
        {
            InitializeComponent();
            TimeBox.Text = "1";
            NumberBox.Text = "1";
        }

        private void Back_click_question(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void Upclick(object sender, RoutedEventArgs e)
        {
            if (TimeBox.Text == "")
            {
                TimeBox.Text = "1";
            }
            else
            {
                int s = Convert.ToInt32(TimeBox.Text);
                if (s < 500)
                {
                    s++;
                    TimeBox.Text = s.ToString();

                }
                else
                {
                    MessageBox.Show("Ошибка! Не более 500 минут.");
                }
            }
        }

        private void Downclick(object sender, RoutedEventArgs e)
        {
            
            int s = Convert.ToInt32(TimeBox.Text);
            if (s > 1)
            {
                s--;
                TimeBox.Text = s.ToString();
            }
            else
            {
                MessageBox.Show("Ошибка! Не менее 1 минуты.");
            }

        }

        private void UpNumberclick(object sender, RoutedEventArgs e)
        {
            int s = Convert.ToInt32(NumberBox.Text);
            if (s < count)
            {
                s++;
                NumberBox.Text = s.ToString();

            }
            else
            {
                MessageBox.Show("В базе данных всего "+count+" вопроса(ов) по теме "+ThemeBox.Text+"."+"\n"+"Дальнейшее увеличение числа вопросов невозможно!");
            }
        }

        private void DownNumberclick(object sender, RoutedEventArgs e)
        {
            int s = Convert.ToInt32(NumberBox.Text);
            if (s > 1)
            {
                s--;
                NumberBox.Text = s.ToString();
            }
            else
            {
                MessageBox.Show("Ошибка! Не менее 1 вопроса.");
            }
        }

        private void TimeBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("В данное поле можно вводить только цифры.");
            }
        }            
        public static string result;
        public static int id_questions;
        public static int tests_id;
       
        private void CreateTestclick(object sender, RoutedEventArgs e)
        {
            
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExpression = $"select q.id,count(a.id)" +
                $" from questions as q inner join answers as a " +
                $"on q.id = a.question_id " +                
                $"where q.teacher_id = '{id_teacher}' and q.theme in" +
                $"(select q.theme from questions " +            
                $"where q.theme = '{ThemeBox.Text}')" +
                $"group by q.id";
            string sqlWithAnswer = $"select q.id,count(a.question_id) " +
                $"from questions as q inner join answers as a " +
                $"on q.id = a.question_id " +
                $"inner join subjects as s " +
                $"on q.subject_id = s.id " +
                $"where q.teacher_id = '{id_teacher}' and s.name in" +
                $"(select s.name from subjects where name = '{SubjectBox.Text}' and q.theme in" +
                $"(select q.theme from questions where q.theme ='{ThemeBox.Text}'))" +
                $" group by q.id,q.theme " +
                $"having count(*) = 4";
            string sqlId = $"select id from tests where name_of_test='{NameBox.Text}' and theme in (select theme from tests where theme='{ThemeBox.Text}')";

            if (MixButton.IsChecked == true)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();                    
                    SqlCommand cm1 = connection.CreateCommand();
                    try
                    {
                        if (subject_id != 0)
                        {
                            cm1.CommandText = $"insert into tests(name_of_test,info,date,time,theme,subject_id,teacher_id) values('{NameBox.Text}','{InfoBox.Text}','{DateTime.Now}','{Convert.ToInt32(TimeBox.Text)}','{ThemeBox.Text}','{subject_id}','{id_teacher}')";
                            cm1.ExecuteNonQuery();
                        }
                        else
                        {
                            InfoBox.Clear();
                            ThemeBox.Clear();
                            SubjectBox.Clear();
                            MessageBox.Show("такого предмета не существует");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                    
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {                    
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlId, connection);
                    SqlDataReader reader = command.ExecuteReader();
                                      
                    try
                    {                       
                        while (reader.Read())
                        {
                            object s = reader.GetValue(0);
                            tests_id = Convert.ToInt32(s);
                        }
                        reader.Close();                       
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
                    Random rnd = new Random();
                    List<int> questions;
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlCommand cm1 = connection.CreateCommand();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        questions = new List<int>();
                        while (reader.Read())
                        {                            
                            questions.Add(Convert.ToInt32(reader.GetValue(0)));                            
                        }
                        reader.Close();
                        questions = questions.OrderBy(n => rnd.Next()).ToList();
                        IEnumerable<int> quest = questions.Take(Convert.ToInt32(NumberBox.Text));
                        foreach(int n in quest)
                        { 
                            cm1.CommandText = $"Insert into questions_tests(test_id,question_id) values('{tests_id}','{n}')";
                            cm1.ExecuteNonQuery();
                        }
                        reader.Close();
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
            //----------------------------С вариантами ответа------------------------------------------------------//
            if (WithAnswersButton.IsChecked == true)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cm1 = connection.CreateCommand();
                    try
                    {
                        if (subject_id != 0)
                        {
                            cm1.CommandText = $"insert into tests(name_of_test,info,date,time,theme,subject_id,teacher_id) values('{NameBox.Text}','{InfoBox.Text}','{DateTime.Now}','{Convert.ToInt32(TimeBox.Text)}','{ThemeBox.Text}','{subject_id}','{id_teacher}')";
                            cm1.ExecuteNonQuery();
                        }
                        else
                        {
                            InfoBox.Clear();
                            ThemeBox.Clear();
                            SubjectBox.Clear();
                            MessageBox.Show("такого предмета не существует");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlId, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            object s = reader.GetValue(0);
                            tests_id = Convert.ToInt32(s);
                        }
                        reader.Close();
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
                    Random rnd = new Random();
                    List<int> questions;
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlWithAnswer, connection);
                    SqlCommand cm1 = connection.CreateCommand();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        questions = new List<int>();
                        while (reader.Read())
                        {
                            questions.Add(Convert.ToInt32(reader.GetValue(0)));
                        }
                        reader.Close();
                        questions = questions.OrderBy(n => rnd.Next()).ToList();
                        IEnumerable<int> quest = questions.Take(Convert.ToInt32(NumberBox.Text));
                        foreach (int n in quest)
                        {
                            cm1.CommandText = $"Insert into questions_tests(test_id,question_id) values('{tests_id}','{n}')";
                            cm1.ExecuteNonQuery();
                        }
                        reader.Close();
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
            MessageBox.Show("Успешно добавлено");
        }
        //-----------------------------проверка на предмет тему и название теста----------------------------------------------//
        public static int results;
        public static string name;
        public static int subject_id;
        public static string theme;
        public static string a;
        public static int b;
        public static int count;
       public static string s = "";
        private void CheckTestclick(object sender, RoutedEventArgs e)
        {
            //if (SubjectBox.Text == "" || ThemeBox.Text == "")
            //{
            //    MessageBox.Show("заполните все поля");                
            //}
            //else
            //{
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSubject = $"select s.id,q.theme,count(q.id) from subjects as s inner join questions as q on s.id=q.subject_id where name='{SubjectBox.Text}' and q.theme in (select q.theme from questions as q  where q.theme='{ThemeBox.Text}' and q.teacher_id in(select q.teacher_id from questions as q where teacher_id='{id_teacher}')) group by s.id,q.theme";
            string sqlName = $"select name_of_test from tests";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
                {               
                connection.Open();
                    SqlCommand command = new SqlCommand(sqlSubject, connection);
                    SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object s = reader.GetValue(0);
                        subject_id = Convert.ToInt32(s);
                        object d = reader.GetValue(1);
                        theme = (string)d;
                        object k = reader.GetValue(2);
                        count = Convert.ToInt32(k);
                    }
                    reader.Close();

                    if (count != 0)
                    {
                        if (subject_id != 0 && theme != "")
                        {                            
                            
                        }
                    }
                    else
                    {
                        ThemeBox.Clear();
                        SubjectBox.Clear();
                        MessageBox.Show("Ошибка. Проверьте правильность значений" + "\n" + " введенных в поле предмет и тема!");
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
                        if (reader.GetValue(0).Equals(NameBox.Text))
                        {
                            MessageBox.Show("Ошибка. Тест с таким именем уже существует уже существует!");
                        }
                        else
                        {
                            MessageBox.Show("Количество вопросов по данной теме : " + count.ToString());
                            Test.Visibility = Visibility.Visible;

                        }
                    }
                    reader.Close();

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
}
