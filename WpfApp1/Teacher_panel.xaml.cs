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
using System.Data.SqlClient;
using System.Data.Linq;
//using System.Linq;
//using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Teacher_panel.xaml
    /// </summary>
    public partial class Teacher_panel : Window
    {
        entry ent = new entry();
        public static string teachername = entry.people;
        public Teacher_panel()
        {            
            
            InitializeComponent();            
        }
           
        private void Create_test_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(teachername);
        }

        private void Close_click_teacher(object sender, RoutedEventArgs e)
        {
            CheckClose check = new CheckClose();
            check.Show();
        }

        private void Create_questions_click(object sender, RoutedEventArgs e)
        {
            Questions_teacher quest = new Questions_teacher();
            quest.Show();
            
        }



        private void Label1_Click(object sender, RoutedEventArgs e)
        {
            label2.Content = teachername;
        }
    }
}
