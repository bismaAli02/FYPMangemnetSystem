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
        }


        private void DisplayStudent()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Id,P.FirstName,P.LastName,P.Contact,P.Gender,P.Email,P.DateOfBirth,S.RegistrationNo FROM Person AS P, Student AS S WHERE S.Id = P.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuDataGrid.ItemsSource = dt.DefaultView;
        }
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
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = stuDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = int.Parse(selectedRow["Id"].ToString());
                deleteTuple(id);
                DisplayStudent();

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName, lastName, contact, email, regNo, dob;
            int gender;
            DataRowView selectedRow = stuDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                firstName = selectedRow["FirstName"].ToString();
                lastName = selectedRow["LastName"].ToString();
                contact = selectedRow["Contact"].ToString();
                email = selectedRow["Email"].ToString();
                regNo = selectedRow["RegistrationNo"].ToString();
                dob = selectedRow["DateOfBirth"].ToString();
                gender = int.Parse(selectedRow["Gender"].ToString());
                AddStuUC.Content = new AddStudentUC(firstName, lastName, contact, email, gender, regNo, dob, id);
                AddStuUC.Visibility = Visibility.Visible;
                AddStuButton.Content = "Go Back";
            }
        }

        private void AddStuButton_Click(object sender, RoutedEventArgs e)
        {

            if (AddStuButton.Content.ToString() == "Add Student")
            {
                AddStuUC.Content = new AddStudentUC();
                AddStuUC.Visibility = Visibility.Visible;
                AddStuButton.Content = "Go Back";
            }
            else
            {
                AddStuUC.Visibility = Visibility.Collapsed;
                AddStuButton.Content = "Add Student";

            }
            DisplayStudent();
        }
    }
}
