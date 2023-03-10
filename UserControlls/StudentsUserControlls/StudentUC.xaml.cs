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
using FYPManagementSystem.UserControlls.ProjectsUserControlls;

namespace FYPManagementSystem
{
    public partial class StudentUC : UserControl
    {
        public StudentUC()
        {
            InitializeComponent();
            DisplayStudent(); // whenever constructor call all students record display
            AddStuScroll.Visibility = Visibility.Collapsed;
        }


        public void DisplayStudent()
        {
            var con = Configuration.getInstance().getConnection();

            // sql querey that retrieve data for students by joining lookup , student and person tabel

            //SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy') this format is used to only retrieve date no time

            SqlCommand cmd = new SqlCommand("SELECT P.Id, S.RegistrationNo , (FirstName +' '+ LastName) AS Name,LU.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy')) AS DateOfBirth,Contact,Email FROM Person AS P JOIN Student AS S ON S.Id=P.Id JOIN Lookup LU ON LU.Id=P.Gender", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuDataGrid.ItemsSource = dt.DefaultView;

            // use a single button for two purpose if Add studentUC visibility is visible that means we can open add student UserControl or if not it means Usercontrol is already opened now we use this button for go back purpose
            if (AddStuUC.Visibility == Visibility.Collapsed)
            {
                AddStuButton.Content = "Add Student";
            }
            else
            {
                AddStuButton.Content = "Go Back";
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName, firstName, lastName, contact, email, regNo, dob, gender;
            DataRowView selectedRow = stuDataGrid.SelectedItem as DataRowView;
            string[] name;// array is used because I concatenated first name and last name 
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                fullName = selectedRow["Name"].ToString();
                name = fullName.Split(' ');
                firstName = name[0];
                lastName = name[1];
                contact = selectedRow["Contact"].ToString();
                email = selectedRow["Email"].ToString();
                regNo = selectedRow["RegistrationNo"].ToString();
                dob = selectedRow["DateOfBirth"].ToString();
                gender = selectedRow["Gender"].ToString();

                // this constructer is call because if I clicked edit button of selected row so I can see previous data in Usercontrol

                AddStuUC.Content = new AddStudentUC(firstName, lastName, contact, email, gender, regNo, dob, id);
                AddStuUC.Visibility = Visibility.Visible;
                AddStuScroll.Visibility = Visibility.Visible;
                AddStuButton.Content = "Go Back";
            }
        }

        private void AddStuButton_Click(object sender, RoutedEventArgs e)
        {

            // use a single button for two purpose if Add student bitton content = Add student that means we can open add student UserControl or if not it means Usercontrol is already opened now we use this button for go back purpose

            if (AddStuButton.Content.ToString() == "Add Student")
            {
                AddStuUC.Content = new AddStudentUC();
                AddStuUC.Visibility = Visibility.Visible;
                AddStuScroll.Visibility = Visibility.Visible;
                AddStuButton.Content = "Go Back";
            }
            else
            {
                AddStuUC.Visibility = Visibility.Collapsed;
                AddStuScroll.Visibility = Visibility.Collapsed;
                AddStuButton.Content = "Add Student";

            }
            DisplayStudent();
        }
    }
}
