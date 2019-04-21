﻿using System;
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
            Question_Without.Visibility = Visibility.Hidden;
            answer1.Visibility = Visibility.Visible;
            answer2.Visibility = Visibility.Visible;
            answer3.Visibility = Visibility.Visible;
            answer4.Visibility = Visibility.Visible;
            Answer1.Visibility = Visibility.Visible;
            Answer2.Visibility = Visibility.Visible;
            Answer3.Visibility = Visibility.Visible;
            Answer4.Visibility = Visibility.Visible;
            AnswerWith.Visibility = Visibility.Visible;
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
            Question.Visibility = Visibility.Hidden;
            Question_Without.Visibility = Visibility.Visible;
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
            AnswerWith.Visibility = Visibility.Hidden;
            closewithanswer.Visibility = Visibility.Hidden;
            Newquestionwithanswer.Visibility = Visibility.Hidden;
            savequestionwithanswer.Visibility = Visibility.Hidden;
            answerwith.Visibility = Visibility.Hidden;
            teacher.Visibility = Visibility.Visible;

        }

        private void Save_question_click(object sender, RoutedEventArgs e)
        {
            if (Subject.Text == "" || Question_Without.Text == "" || Theme_question.Text == "" || AnswerWithout.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void Close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_question_click(object sender, RoutedEventArgs e)
        {

            Question.Clear();
            Answer1.Clear();
            Answer2.Clear();
            Answer3.Clear();
            Answer4.Clear();
            AnswerWithout.Clear();

        }

        private void Save_question_withanswer_click(object sender, RoutedEventArgs e)
        {
            if (Subject.Text == "" || Theme_question.Text == "" || Question.Text == "" || Answer1.Text == "" || Answer2.Text == "" || Answer3.Text == "" || Answer4.Text == "" || AnswerWith.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void New_question_withanswer_click(object sender, RoutedEventArgs e)
        {
            Question.Clear();
            Answer1.Clear();
            Answer2.Clear();
            Answer3.Clear();
            Answer4.Clear();
            AnswerWith.Clear();
        }

        private void Close_question_withanswer_click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        //все значения с текстбоксов
        public static string subject;
        public static string theme;
        public static string questions;
        public static string questions_without;
        public static string answer_with;
        public static string answer1_with;
        public static string answer2_with;
        public static string answer3_with;
        public static string answer4_with;
        public static string answer_without;
        private void Subject_TextChanged(object sender, TextChangedEventArgs e) => subject = Subject.Text;
        private void Theme_question_TextChanged(object sender, TextChangedEventArgs e) => theme = Theme_question.Text;
        private void Question_TextChanged(object sender, TextChangedEventArgs e)
        {
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = questions, FontSize = 12 });        
            toolTip.Content = toolTipPanel;
            question.ToolTip = toolTip;
        }
        private void Answer1_TextChanged(object sender, TextChangedEventArgs e)
        {
            answer1_with = Answer1.Text;
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = answer1_with, FontSize = 12 });
            toolTip.Content = toolTipPanel;
            answer1.ToolTip = toolTip;
        }
        private void Answer2_TextChanged(object sender, TextChangedEventArgs e)
        {
            answer2_with = Answer2.Text;
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = answer2_with, FontSize = 12 });
            toolTip.Content = toolTipPanel;
            answer2.ToolTip = toolTip;
        }
        private void Answer3_TextChanged(object sender, TextChangedEventArgs e)
        {
            answer3_with = Answer3.Text;
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = answer3_with, FontSize = 12 });
            toolTip.Content = toolTipPanel;
            answer3.ToolTip = toolTip;
        }
        private void Answer4_TextChanged(object sender, TextChangedEventArgs e)
        {
            answer4_with = Answer4.Text;
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = answer4_with, FontSize = 12 });
            toolTip.Content = toolTipPanel;
            answer4.ToolTip = toolTip;
        }
        private void AnswerWith_TextChanged(object sender, TextChangedEventArgs e) =>  answer_with = AnswerWith.Text;
        
        private void Question_Without_TextChanged(object sender, TextChangedEventArgs e)
        {
            questions_without = Question_Without.Text;
            questions = Question.Text;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            toolTipPanel.Children.Add(new TextBlock { Text = questions_without, FontSize = 12 });
            toolTip.Content = toolTipPanel;
            question_without.ToolTip = toolTip;
        }
        private void AnswerWithout_TextChanged(object sender, TextChangedEventArgs e)=>   answer_without = AnswerWithout.Text;
         
       
    }
}
