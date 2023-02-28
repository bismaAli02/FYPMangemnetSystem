using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{
    /// <summary>
    /// Interaction logic for AssignAdvUC.xaml
    /// </summary>
    public partial class AssignAdvUC : UserControl
    {
        public AssignAdvUC()
        {
            InitializeComponent();
        }

        public void DisplayAdvisors()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT FROM ProjectAdvisor AS PA JOIN Project AS P ON PA.ProjectId = P.Id JOIN Person AS P1 ON PA.", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            AssignAdvDataGrid.ItemsSource = dt.DefaultView;

        }

        private void AssignProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (AssignProjectButton.Content.ToString() == "Assign Advisor To Project")
            {
                addProjectUC.Content = new AddProjectAdvUC();
                addProjectUC.Visibility = Visibility.Visible;
                AddStuScroll.Visibility = Visibility.Visible;
                AssignProjectButton.Content = "Go Back";
            }
            else
            {
                addProjectUC.Visibility = Visibility.Collapsed;
                AddStuScroll.Visibility = Visibility.Collapsed;
                AssignProjectButton.Content = "Assign Advisor To Project";

            }
            /*DisplayStudent*/
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
