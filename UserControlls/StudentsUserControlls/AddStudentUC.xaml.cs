using CRUD_Operations;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf.qrcode;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FYPManagementSystem.UserControlls.StudentsUserControlls
{
    public partial class AddStudentUC : UserControl
    {
        int id; // this attribute is used for student id 
        public AddStudentUC()
        {
            InitializeComponent();
            savebuttontxt.Text = "Save";
            GenderToComboBox();
        }

        // this parametrized constructer made for update operation purpose
        public AddStudentUC(string firstName, string lastName, string contact, string email, string gender, string regNo, string dob, int id)
        {
            InitializeComponent();
            savebuttontxt.Text = "Update";
            FNTextBox.Text = firstName;
            LNTextBox.Text = lastName;
            ContactTextBox.Text = contact;
            EmailTextBox.Text = email;
            GenderToComboBox();
            genderComboBox.Text = gender;
            Date.Text = dob;
            RNTextBox.Text = regNo;
            this.id = id;
        }

        // this function gives value from Lookup table to gender combo box
        private void GenderToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Value FROM Lookup WHERE Category=@Category", con);
                cmd.Parameters.AddWithValue("@Category", "GENDER");

                //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                //this line of code creates a new DataSet object, which can be used to work with data in a tabular format.
                DataSet dataSet = new DataSet();

                //it retrieves data from a data source and fills a DataSet object with the retrieved data.
                dataAdapter.Fill(dataSet);
                // this code help to display data in comboBox
                genderComboBox.ItemsSource = dataSet.Tables[0].DefaultView;

                //GenderComboBox control will display the data from the "Value" column of the data source in its dropdown list.
                genderComboBox.DisplayMemberPath = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        // this code clears the controls and hides the current user control and  allow to return to the previous screen.
        private void BackButton_Click_1(object sender, RoutedEventArgs e)
        {
            EmptyForm();
            this.Visibility = Visibility.Collapsed;
        }

        //this code clears  all the controls
        private void EmptyForm()
        {
            FNTextBox.Text = string.Empty;
            LNTextBox.Text = string.Empty;
            genderComboBox.Text = string.Empty;
            Date.Text = string.Empty;
            RNTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            ContactTextBox.Text = string.Empty;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        //this method is used to find the parent ScrollViewer control of the current user control
        private void findParent()
        {
            //this code is used to find immediate parent of the current control
            var parent = VisualTreeHelper.GetParent(this);

            // this loop traverse on tree(user Controls)
            while (parent != null && !(parent is ScrollViewer))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is ScrollViewer)
            {
                ScrollViewer par = parent as ScrollViewer;
                par.Visibility = Visibility.Collapsed;
                findParentUserControl(); //use to find Parent of Add student UC (student UC) so i can use Student UC functions or controlls

            }
        }

        //this code find the parent StudentUC control. It sets the text of a Button to "Add Student".function is used to call a method of student UC (display students)
        private void findParentUserControl()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is StudentUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is StudentUC)
            {
                StudentUC par = parent as StudentUC;
                par.DisplayStudent();
                Button btn = (Button)par.FindName("AddStuButton");
                btn.Content = "Add Student";
            }
        }

        // give gender of current student whose data will enter in form
        private int ReturnGender()
        {
            int gender = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='GENDER' AND Value=@Gender", con);
            cmd.Parameters.AddWithValue("@Gender", genderComboBox.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                gender = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return gender;

        }

        // this function simply save data into data base

        private void SaveRecord(int gender)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) VALUES (@FirstName,@LastName, @Contact,@Email,@DateOfBirth, @Gender); insert into Student(Id,RegistrationNo) VALUES ((SELECT Id FROM Person WHERE FirstName = @FirstName AND LastName=@LastName AND Contact=@Contact AND Email=@Email AND DateOfBirth=@DateOfBirth AND Gender=@Gender) ,@RegNo);", con);
                cmd.Parameters.AddWithValue("@FirstName", FNTextBox.Text);
                cmd.Parameters.AddWithValue("@LastName", LNTextBox.Text);
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", Date.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@RegNo", RNTextBox.Text);
                EmptyForm();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // this function is used to update record in database
        private void UpdateRecord(int gender)
        {
            if (FNTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName=@LastName, Contact=@Contact, Email=@Email, DateOfBirth=@DateOfBirth, Gender=@Gender WHERE Id=@Id; UPDATE Student SET RegistrationNo=@RegNo WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@FirstName", FNTextBox.Text);
                    cmd.Parameters.AddWithValue("@LastName", LNTextBox.Text);
                    cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text);
                    cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Date.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@RegNo", RNTextBox.Text);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select any record to Update");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int gender = ReturnGender();// give gender of current student whose data will enter in form

            // in if condition checking all validation
            if (RegisterNoValidations() && FirstNameValidations() && LastNameValidations() && ContactNumberValidation() && EmailValidation())
            {
                if (savebuttontxt.Text == "Save")
                {
                    SaveRecord(gender);
                }
                else
                {
                    UpdateRecord(gender);
                }
                findParent(); // this function is used here for display student method purpose and scrollViewer visibility purpose
            }

        }


        //this function validate a query in a database  and checking whether it returns any rows. If the query returns rows, the function returns true otherwise, it returns false. 
        private bool ValidationInDatabase(string query)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();// execute query and read the resulting rows one at a time.
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        // All validation Code

        private bool FirstNameValidations()
        {
            string name = FNTextBox.Text;
            bool isValid = true;

            if (name == "")
            {
                MessageBox.Show("First Name cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            else if (name == " ")
            {
                MessageBox.Show("First Name cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            else if (name.Length >= 3)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (!((name[i] > 64 && name[i] < 91) || (name[i] > 96 && name[i] < 123)))
                    {
                        MessageBox.Show("Name cannot contain Characters other than A-Z or a-z", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        isValid = false;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Name cannot be less than 3 Characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            return isValid;
        }

        private bool LastNameValidations()
        {
            string name = LNTextBox.Text;
            bool isValid = true;

            if (name.Length >= 3)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (!((name[i] > 64 && name[i] < 91) || (name[i] > 96 && name[i] < 123)))
                    {
                        MessageBox.Show("Name cannot contain Characters other than A-Z or a-z", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        isValid = false;
                        break;
                    }
                }
            }
            else if (name != "")
            {
                MessageBox.Show("Name cannot be less than 3 Characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            return isValid;
        }

        private bool ContactNumberValidation()
        {
            string numbers = "0123456789";
            bool isValid = true;
            if (ContactTextBox.Text == "")
            {

            }
            else if (ContactTextBox.Text.Length == 11)
            {
                foreach (char n in ContactTextBox.Text)
                {
                    if (!numbers.Contains(n.ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("InValid Contact Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return isValid;
        }

        private bool EmailValidation()
        {
            string email = EmailTextBox.Text;
            bool isValid = true;
            if (email.Contains("@") && email.Contains("."))
            {

            }
            else
            {
                MessageBox.Show("InValid Email Address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            return isValid;
        }

        private bool RegisterNoValidations()
        {
            string regNo = RNTextBox.Text.ToString();
            bool isValid = true;
            if (regNo.Length < 9)
            {
                MessageBox.Show("Registration No does no fit up to the required Lenght", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            else
            {

                string year = regNo.Substring(0, 4);
                int year1;
                string department = regNo.Substring(4, 4);
                int regNumb;
                string regNumb1 = regNo.Substring(8, regNo.Length - 8);
                if (!int.TryParse(year, out year1) || (year1 < DateTime.Now.Year - 4) || year1 > DateTime.Now.Year)
                {
                    MessageBox.Show("Year should be between " + (DateTime.Now.Year - 4) + " and " + DateTime.Now.Year, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                }
                else if (department != "-CS-")
                {
                    MessageBox.Show("Registration Number is not up to the required Format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                }
                else if (!int.TryParse(regNumb1, out regNumb))
                {
                    MessageBox.Show("Registartion Number must be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                }
                else if (ValidationInDatabase("SELECT RegistrationNo FROM Student WHERE RegistrationNo='" + regNo + "' AND Id<>'" + id + "'"))
                {
                    MessageBox.Show("Registration Number is already taken up by another student", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                }
            }

            return isValid;
        }


    }
}
