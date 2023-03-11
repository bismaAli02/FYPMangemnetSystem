using CRUD_Operations;
using FYPManagementSystem.UserControlls.StudentsUserControlls;
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
using FYPManagementSystem.UserControlls.EvaluationsUserControlls;

namespace FYPManagementSystem.UserControlls.ProjectsUserControlls
{
    /// <summary>
    /// Interaction logic for ProjUC.xaml
    /// </summary>
    public partial class ProjUC : UserControl
    {
        public ProjUC()
        {
            InitializeComponent();
            DisplayProjects(); // whenever constructor call all Projects record display
        }
        public void DisplayProjects()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id, Description, Title FROM Project", con);
            //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
            // sql data adpter is use to retrieve data from tabel
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //this line of code creates a new Datatable object, which can be used to work with data in a tabular format.
            DataTable dt = new DataTable();
            da.Fill(dt);
            ProjDataGrid.ItemsSource = dt.DefaultView;

            // use a single button for two purpose if Add ProjUC visibility is visible that means we can open Add Project UserControl or if not it means Usercontrol is already opened now we use this button for go back purpose
            if (AddProjUC.Visibility == Visibility.Collapsed)
            {
                AddProjButton.Content = "Add Project";
            }
            else
            {
                AddProjButton.Content = "Go Back";
            }
        }

        private void deleteTuple(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Project WHERE Id =@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddProjButton_Click(object sender, RoutedEventArgs e)
        {
            // use a single button for two purpose if Add ProjUC visibility is visible that means we can open Add Project UserControl or if not it means Usercontrol is already opened now we use this button for go back purpose

            if (AddProjButton.Content.ToString() == "Add Project")
            {
                AddProjUC.Content = new AddProjUC();
                AddProjUC.Visibility = Visibility.Visible;
                AddProjButton.Content = "Go Back";
            }
            else
            {
                AddProjUC.Visibility = Visibility.Collapsed;
                AddProjButton.Content = "Add Project";

            }
            DisplayProjects();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string description, title;
            DataRowView selectedRow = ProjDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                description = selectedRow["Description"].ToString();
                title = selectedRow["Title"].ToString();
                // this constructer is call because if I clicked edit button of selected row so I can see previous data in Usercontrol
                AddProjUC.Content = new AddProjUC(description, title, id);
                AddProjUC.Visibility = Visibility.Visible;
                AddProjButton.Content = "Go Back";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = ProjDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = int.Parse(selectedRow["Id"].ToString());
                deleteTuple(id);
                AddProjUC.Visibility = Visibility.Collapsed;
                DisplayProjects();
            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }

        }
    }
}
