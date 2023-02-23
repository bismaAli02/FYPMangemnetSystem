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
            DisplayProjects();
        }
        private void DisplayProjects()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id, Description, Title FROM Project", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ProjDataGrid.ItemsSource = dt.DefaultView;
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
                DisplayProjects();

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }

        }
    }
}
