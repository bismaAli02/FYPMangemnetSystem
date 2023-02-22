using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FYPManagementSystem.UserControlls.StudentsUserControlls;

namespace FYPManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentUC.xaml
    /// </summary>
    public partial class StudentUC : UserControl
    {
        public StudentUC()
        {
            InitializeComponent();
            DisplayStudent();
            AddStuUC.Content = new AddStudentUC();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show((AddStuUC.Visibility == Visibility.Collapsed).ToString());
            if (AddStuUC.Visibility == Visibility.Collapsed)
            {
                addStScroll.Visibility = Visibility.Collapsed;
            }
        }

        private void DisplayStudent()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Person AS P, Student AS S WHERE S.Id = P.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuDataGrid.ItemsSource = dt.DefaultView;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStuButton_Click(object sender, RoutedEventArgs e)
        {
            addStScroll.Visibility = Visibility.Visible;
            AddStuUC.Visibility = Visibility.Visible;
        }
    }
}
