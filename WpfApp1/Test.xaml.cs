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
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Windows.Media.TextFormatting;
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private DispatcherTimer _timer;

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(TimeSpan), typeof(Test), new PropertyMetadata(default(TimeSpan)));

        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(
            "Seconds", typeof(int), typeof(Test), new PropertyMetadata(default(int)));
        public static string s;
        public static string checkName;
        public Test()
        {
            InitializeComponent();

            TestStudent.Visibility = Visibility.Visible;
            Testik.Visibility = Visibility.Hidden;
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
        public int Seconds
        {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }

        }
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {           
            Time = Time.Subtract(TimeSpan.FromSeconds(1));
            
            if (Time == TimeSpan.Zero)
            {                
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                
                MessageBox.Show("Тест закончен");
                this.Close();
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
                        InfoOf.Text = info;
                        TimeOf.Content = time.ToString();
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
            Testik.Visibility = Visibility.Visible;
            Seconds = time * 60;
            Time = TimeSpan.FromSeconds(Seconds);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlCount = $"select count(qt.question_id)  from questions as q inner join questions_tests as qt on q.id = qt.question_id inner join tests as t on qt.test_id = t.id where t.name_of_test = '{NameList.SelectedValue.ToString()}' ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCount, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    while (reader.Read())
                    {
                        CountTest.Content = Convert.ToInt32(reader.GetValue(0));
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
        public void MyMethod()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSubjectWithAnswers = $"select q.question,a.answer from questions as q inner join questions_tests as qt on q.id = qt.question_id inner join tests as t on qt.test_id = t.id inner join answers as a on q.id = a.question_id where t.name_of_test = '{NameList.SelectedValue.ToString()}'  and q.id in (select a.question_id from answers as a group by a.question_id having count(a.question_id) = 4)";
            string sqlCount = $"select count(qt.question_id)  from questions as q inner join questions_tests as qt on q.id = qt.question_id inner join tests as t on qt.test_id = t.id where t.name_of_test = '{NameList.SelectedValue.ToString()}' ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<string> questions;
                List<string> answers;
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSubjectWithAnswers, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    questions = new List<string>();
                    answers = new List<string>();
                    while (reader.Read())
                    {
                        questions.Add((string)reader.GetValue(0));
                        answers.Add((string)reader.GetValue(1));
                    }
                    reader.Close();
                    reader = command.ExecuteReader();
                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i == 0)
                            Answer1Test.Content = (string)reader.GetValue(1);
                        if (i == 1)
                            Answer2Test.Content = (string)reader.GetValue(1);
                        if (i == 2)
                            Answer3Test.Content = (string)reader.GetValue(1);
                        if (i == 3)
                            Answer4Test.Content = (string)reader.GetValue(1);
                    }
                    foreach (string x in questions)
                    {
                        Question.Text = x;
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

      
        public static int j = 0;
        private void UpQuestionclick(object sender, RoutedEventArgs e)
        {
            if (j < Convert.ToInt32(CountTest.Content))
            {
                MyMethod();
                j++;
                CountFirstTest.Content = j.ToString();
            }
            else
            {
                MessageBox.Show("С вас хватит");
            }
                    
        }
    }
}
