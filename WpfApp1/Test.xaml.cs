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
        
        public static int id_student = entry.identry;
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
                Result();
                App.Current.Shutdown();
                j = 0;
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
        public static int test_id = 0;
        private void PassTestclick(object sender, RoutedEventArgs e)
        {
            if(SubjectList.Text=="" || NameList.Text == "")
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
                string sqlSubject = $"select t.name_of_test,s.name,t.theme,u.first_name,u.middle_name,t.time,t.info,t.id from tests as t inner join subjects as s on t.subject_id=s.id inner join users as u on t.teacher_id=u.id where t.name_of_test='{checkName}' and t.subject_id='{id_subj}'";
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
                            test_id= Convert.ToInt32(reader.GetValue(7));
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
            TestCreate();           
            MyMethod();
            j++;
            CountFirstTest.Content = j.ToString();
        }
        public static int j = 0;
        public static int id_question=0;
        public static int unique_id=0;
        public static int is_true = 0;
        public void MyMethod()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlSubjectWithAnswers = $"select distinct q.id from questions as q inner join questions_tests as qt on q.id = qt.question_id inner join tests as t on qt.test_id = t.id inner join answers as a on q.id = a.question_id where t.name_of_test = '{NameList.SelectedValue.ToString()}'";
            string sqlCount = $"select count(qt.question_id)  from questions as q inner join questions_tests as qt on q.id = qt.question_id inner join tests as t on qt.test_id = t.id where t.name_of_test = '{NameList.SelectedValue.ToString()}' ";
            string sqlQuestion = $"select question from questions where id='{id_question}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<int> questions;
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSubjectWithAnswers, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    questions = new List<int>();
                    while (reader.Read())
                    {
                        questions.Add(Convert.ToInt32(reader.GetValue(0)));
                    }
                    reader.Close();
                    id_question = Convert.ToInt32(questions.ElementAt(j));
                    Que();
                    Ans();
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
        public void Clear()
        {
            Answer.Clear();
            Answer1Test.IsChecked = false;
            Answer2Test.IsChecked = false;
            Answer3Test.IsChecked = false;
            Answer4Test.IsChecked = false;
        }
        public void Que()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlQuestion = $"select question from questions where id='{id_question}'";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuestion, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    while (reader.Read())
                    {
                        Question.Text = (string)reader.GetValue(0);
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

        public void Ans()
        {
            int count = 0;
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlCount = $"select count(answer) from answers where question_id='{id_question}'";
            string sqlAnswer = $"select answer from answers where question_id='{id_question}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {                
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCount, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                   
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader.GetValue(0));
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
                List<string> answers;
                connection.Open();
                SqlCommand command = new SqlCommand(sqlAnswer, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    answers = new List<string>();
                    while (reader.Read())
                    {
                        answers.Add((string)reader.GetValue(0));
                        //Question.Text = (string)reader.GetValue(0);
                    }
                    reader.Close();
                    if (count < 2)
                    {
                        WithAnswer.Visibility = Visibility.Hidden;
                        WithoutAnswer.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        WithAnswer.Visibility = Visibility.Visible;
                        WithoutAnswer.Visibility = Visibility.Hidden;
                        Answer1Test.Content = answers.ElementAt(0);
                        Answer2Test.Content = answers.ElementAt(1);
                        Answer3Test.Content = answers.ElementAt(2);
                        Answer4Test.Content = answers.ElementAt(3);
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
        public static string answer = "";
        public static int rightanswer;
        public static string ans = "";
        public void AnswersTest()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlId = $"select max(id) from users_tests";
            string sqlAnswer = $"select answer from answers where question_id='{id_question}' and is_true='true'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cm1 = connection.CreateCommand();
                if (WithAnswer.Visibility == Visibility.Visible)
                {
                    if (Answer1Test.IsChecked == true)
                    {
                        answer = (string)Answer1Test.Content;
                    }
                    else
                    {
                        if (Answer2Test.IsChecked == true)
                        {
                            answer = (string)Answer2Test.Content;
                        }
                        else
                        {
                            if (Answer3Test.IsChecked == true)
                            {
                                answer = (string)Answer3Test.Content;
                            }
                            else
                            {
                                if (Answer4Test.IsChecked == true)
                                {
                                    answer = (string)Answer4Test.Content;
                                }
                            }
                        }
                    }
                }
                else
                {
                    answer = Answer.Text;
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
                        unique_id = Convert.ToInt32(reader.GetValue(0));
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
                connection.Open();
                SqlCommand command = new SqlCommand(sqlAnswer, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        ans = (string)reader.GetValue(0);
                        if (answer.Equals(ans))
                        {
                            rightanswer = 1;
                        }
                        else
                        {
                            rightanswer = 0;
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
            //значения
            
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                connection.Open();
                    SqlCommand cm1 = connection.CreateCommand();
                    cm1.CommandText = $"insert into answers_tests(question_id,user_test_id,answer_student,is_true,right_answer) values('{id_question}','{unique_id}','{answer}','{rightanswer}','{ans}')";
                    cm1.ExecuteNonQuery();
                }                            
            
        }
        public static int id_subj;
        public void TestCreate()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
               
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {                 
                  cm1.CommandText=$"Insert into users_tests(student_id,test_id,count_questions) values('{id_student}','{test_id}','{CountTest.Content}')";
                  cm1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                   
                }
            }
        }
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

      
        
        private void UpQuestionclick(object sender, RoutedEventArgs e)
        {
            AnswersTest();
            if (j < Convert.ToInt32(CountTest.Content))
            {                
                MyMethod();
                j++;
                CountFirstTest.Content = j.ToString();
            }
            else
            {
                MessageBox.Show("С вас хватит");
                UpClick.Visibility = Visibility.Hidden;
                EndClick.Visibility = Visibility.Visible;
                EditClick.Visibility = Visibility.Visible;
            }
            Clear();
        }

        private void EndQuestionclick(object sender, RoutedEventArgs e)
        {
            Result();
            App.Current.Shutdown();
        }
        public static int CountRightQuestions = 0;
        public static int CountQuestions = 0;
        public static float Results = 0;
        private void Result()
        {
            string ConnectionString = @"Data Source=DESKTOP-15P21ID;Initial Catalog=kursovoi;Integrated Security=True";
            string SqlResult = $"select count(a.answer_student) from answers_tests as a where is_true='true' and user_test_id='{unique_id}'";
            string SqlCount = $"select count_questions from users_tests where id='{unique_id}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {                
                connection.Open();
                SqlCommand command = new SqlCommand(SqlResult, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        CountRightQuestions = Convert.ToInt32(reader.GetValue(0));
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
                SqlCommand command = new SqlCommand(SqlCount, connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        CountQuestions = Convert.ToInt32(reader.GetValue(0));
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
            Results = (CountRightQuestions / CountQuestions)*100;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = $"Insert into results(unique_id,result) values('{unique_id}','{Results}')";
                    cm1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
        }
        private void EditQuestionclick(object sender, RoutedEventArgs e)
        {
            EndTest end = new EndTest();
            end.Show();
            if (Time == TimeSpan.Zero)
            {
            }
        }
    }
}
