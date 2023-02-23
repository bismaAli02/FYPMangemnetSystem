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

namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{
    /// <summary>
    /// Interaction logic for AdvUC.xaml
    /// </summary>
    public partial class AdvUC : UserControl
    {
        public AdvUC()
        {
            InitializeComponent();
            DisplayAdvisors();
        }


        private void DisplayAdvisors()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Id,P.FirstName,P.LastName,P.Contact,P.Gender,P.Email,P.DateOfBirth,A.Designation,A.Salary FROM Person AS P, Advisor AS A WHERE A.Id = P.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            advDataGrid.ItemsSource = dt.DefaultView;
        }
        private void deleteTuple(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Advisor" +
                    " WHERE Id =@Id; DELETE FROM Person WHERE Id =@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = advDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = int.Parse(selectedRow["Id"].ToString());
                deleteTuple(id);
                DisplayAdvisors();

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName, lastName, contact, email, salary, dob;
            int gender, designation;
            DataRowView selectedRow = advDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                firstName = selectedRow["FirstName"].ToString();
                lastName = selectedRow["LastName"].ToString();
                contact = selectedRow["Contact"].ToString();
                email = selectedRow["Email"].ToString();
                salary = selectedRow["Salary"].ToString();
                dob = selectedRow["DateOfBirth"].ToString();
                gender = int.Parse(selectedRow["Gender"].ToString());
                designation = int.Parse(selectedRow["Designation"].ToString());
                AddAdvUC.Content = new AddAdvUC(firstName, lastName, contact, email, gender, designation, salary, dob, id);
                AddAdvUC.Visibility = Visibility.Visible;
                AddAdvButton.Content = "Go Back";
            }
        }

        private void AddAdvButton_Click(object sender, RoutedEventArgs e)
        {

            if (AddAdvButton.Content.ToString() == "Add Advisor")
            {
                AddAdvUC.Content = new AddAdvUC();
                AddAdvUC.Visibility = Visibility.Visible;
                AddAdvButton.Content = "Go Back";
            }
            else
            {
                AddAdvUC.Visibility = Visibility.Collapsed;
                AddAdvButton.Content = "Add Advisor";

            }
            DisplayAdvisors();
        }
    }
}
