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
            DisplayAdvisors();
        }

        public void DisplayAdvisors()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS MainAdvisor, MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS CoAdvisor, MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS IndustryAdvisor FROM  ProjectAdvisor PA INNER JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id GROUP BY PA.ProjectId", con);
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
            DisplayAdvisors();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /*
                private void deleteTuple(int id)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE Id =@Id; DELETE FROM Person WHERE Id =@Id", con);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Deleted!!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }*/

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = AssignAdvDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = int.Parse(selectedRow["Id"].ToString());
                // deleteTuple(id);
                AssignAdvScroll.Visibility = Visibility.Collapsed;
                DisplayAdvisors();
                AssignProjectButton.Content = "Assign Advisor To Project";
            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }
    }
}
