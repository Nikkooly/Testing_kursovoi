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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Questions_teacher.xaml
    /// </summary>
    public partial class Questions_teacher : Window
    {
        public Questions_teacher()
        {
            InitializeComponent();
        }

        private void Close_click_question(object sender, RoutedEventArgs e)
        {
            CheckClose chek = new CheckClose();
            chek.Show();
        }

        private void Back_click_question(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void Create_questions_click(object sender, RoutedEventArgs e)
        {

        }

        private void With_answers_click(object sender, RoutedEventArgs e)
        {
            question.Visibility = Visibility.Visible;
            Question.Visibility = Visibility.Visible;
            answer1.Visibility = Visibility.Visible;
            answer2.Visibility = Visibility.Visible;
            answer3.Visibility = Visibility.Visible;
            answer4.Visibility = Visibility.Visible;
            Answer1.Visibility = Visibility.Visible;
            Answer2.Visibility = Visibility.Visible;
            Answer3.Visibility = Visibility.Visible;
            Answer4.Visibility = Visibility.Visible;
            Answer.Visibility = Visibility.Visible;
            answerwith.Visibility = Visibility.Visible;
            closewithanswer.Visibility = Visibility.Visible;
            Newquestionwithanswer.Visibility = Visibility.Visible;
            savequestionwithanswer.Visibility = Visibility.Visible;
            AnswerWithout.Visibility = Visibility.Hidden;
            answerwithout.Visibility = Visibility.Hidden;
            savequestion.Visibility = Visibility.Hidden;
            closequestion.Visibility = Visibility.Hidden;
            Newquestion.Visibility = Visibility.Hidden;
            teacher.Visibility = Visibility.Hidden;

        }
       
        private void WithoutAnswers_click(object sender, RoutedEventArgs e)
        {
            
            question.Visibility = Visibility.Visible;
            Question.Visibility = Visibility.Visible;
            AnswerWithout.Visibility = Visibility.Visible;
            answerwithout.Visibility = Visibility.Visible;
            Newquestion.Visibility = Visibility.Visible;
            closequestion.Visibility = Visibility.Visible;
            savequestion.Visibility = Visibility.Visible;
            answer1.Visibility = Visibility.Hidden;
            answer2.Visibility = Visibility.Hidden;
            answer3.Visibility = Visibility.Hidden;
            answer4.Visibility = Visibility.Hidden;
            Answer1.Visibility = Visibility.Hidden;
            Answer2.Visibility = Visibility.Hidden;
            Answer3.Visibility = Visibility.Hidden;
            Answer4.Visibility = Visibility.Hidden;
            Answer.Visibility = Visibility.Hidden;
            closewithanswer.Visibility = Visibility.Hidden;
            Newquestionwithanswer.Visibility = Visibility.Hidden;
            savequestionwithanswer.Visibility = Visibility.Hidden;
            answerwith.Visibility = Visibility.Hidden;
            teacher.Visibility = Visibility.Visible;

        }

        private void Save_question_click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_question_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Save_question_withanswer_click(object sender, RoutedEventArgs e)
        {

        }

        private void New_question_withanswer_click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_question_withanswer_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
