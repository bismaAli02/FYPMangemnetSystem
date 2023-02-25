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
using System.Security.AccessControl;

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
            AddAdvScroll.Visibility = Visibility.Collapsed;
            DisplayAdvisors();
        }


        public void DisplayAdvisors()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select P.Id, (FirstName + ' ' + LastName) AS Name,LU1.Value AS Designation,A.Salary,LU.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy')) AS [DateOfBirth],Contact,Email FROM Person P JOIN Advisor A ON A.Id=P.Id JOIN Lookup LU ON LU.Id=P.Gender JOIN Lookup LU1 ON LU1.Id=A.Designation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            advDataGrid.ItemsSource = dt.DefaultView;
            if (AddAdvScroll.Visibility == Visibility.Collapsed)
            {
                AddAdvButton.Content = "Add Advisor";
            }
            else
            {
                AddAdvButton.Content = "Go Back";

            }
        }
        private void deleteTuple(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Advisor WHERE Id =@Id; DELETE FROM Person WHERE Id =@Id", con);
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
                AddAdvScroll.Visibility = Visibility.Collapsed;
                DisplayAdvisors();

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName, firstName, lastName, contact, email, salary, dob, gender, designation;
            DataRowView selectedRow = advDataGrid.SelectedItem as DataRowView;
            string[] name;
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                fullName = selectedRow["Name"].ToString();
                name = fullName.Split(' ');
                firstName = name[0];
                lastName = name[1];
                contact = selectedRow["Contact"].ToString();
                email = selectedRow["Email"].ToString();
                salary = selectedRow["Salary"].ToString();
                dob = selectedRow["DateOfBirth"].ToString();
                gender = selectedRow["Gender"].ToString();
                designation = selectedRow["Designation"].ToString();
                AddAdvUC.Content = new AddAdvUC(firstName, lastName, contact, email, gender, designation, salary, dob, id);
                AddAdvScroll.Visibility = Visibility.Visible;
                AddAdvUC.Visibility = Visibility.Visible;
                AddAdvButton.Content = "Go Back";
            }
        }

        private void AddAdvButton_Click(object sender, RoutedEventArgs e)
        {

            if (AddAdvButton.Content.ToString() == "Add Advisor")
            {
                AddAdvUC.Content = new AddAdvUC();
                AddAdvScroll.Visibility = Visibility.Visible;
                AddAdvUC.Visibility = Visibility.Visible;
                AddAdvButton.Content = "Go Back";
            }
            else
            {
                AddAdvUC.Visibility = Visibility.Collapsed;
                AddAdvScroll.Visibility = Visibility.Collapsed;
                AddAdvButton.Content = "Add Advisor";

            }
            DisplayAdvisors();
        }
    }
}
