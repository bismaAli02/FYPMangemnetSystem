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

namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{
    public partial class AddAdvUC : UserControl
    {
        int id;    //this attribute is used for Advisor id
        public AddAdvUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
            GenderToComboBox();
            DesignationToComboBox();
        }
        // this parametrized constructer made for update operation purpose
        public AddAdvUC(string firstName, string lastName, string contact, string email, string gender, string designation, string salary, string dob, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
            FNTextBox.Text = firstName;
            LNTextBox.Text = lastName;
            ContactTextBox.Text = contact;
            EmailTextBox.Text = email;
            GenderToComboBox();
            genderComboBox.Text = gender;
            Date.Text = dob;
            DesignationToComboBox();
            DesComboBox.Text = designation;
            this.id = id;
            SalaryTextBox.Text = salary;
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
                findParentUserControl();//use to find Parent of Add advisor UC (Advisor UC) so i can use SAdvisor UC functions or controlls

            }
        }

        //this code find the parent AdvUC control. It sets the text of a Button to "Add Adviso".function is used to call a method of Advisor UC (displayAdvisors)
        private void findParentUserControl()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is AdvisorsUsercontrolls.AdvUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is AdvisorsUsercontrolls.AdvUC)
            {
                AdvisorsUsercontrolls.AdvUC par = parent as AdvisorsUsercontrolls.AdvUC;
                par.DisplayAdvisors();
                Button btn = (Button)par.FindName("AddAdvButton");
                btn.Content = "Add Advisor";

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
            DesComboBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            ContactTextBox.Text = string.Empty;
            SalaryTextBox.Text = string.Empty;
        }

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

        // take data from lookup tabel and display into designation ComboBox
        private void DesignationToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Value FROM Lookup WHERE Category=@Category", con);
                cmd.Parameters.AddWithValue("@Category", "DESIGNATION");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DesComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                DesComboBox.DisplayMemberPath = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        // save record into dataBase
        private void SaveRecord(int gender, int designation)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) VALUES (@FirstName,@LastName, @Contact,@Email,@DateOfBirth, @Gender); INSERT INTO Advisor(Id,Designation,Salary) VALUES ((SELECT Id FROM Person WHERE FirstName = @FirstName AND LastName=@LastName AND Contact=@Contact AND Email=@Email AND DateOfBirth=@DateOfBirth AND Gender=@Gender) ,@Designation, @Salary);", con);
                cmd.Parameters.AddWithValue("@FirstName", FNTextBox.Text);
                cmd.Parameters.AddWithValue("@LastName", LNTextBox.Text);
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text);
                cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", Date.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@Salary", int.Parse(SalaryTextBox.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                EmptyForm();
                findParent();// this function is used here for display student method purpose and scrollViewer visibility purpose
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // update record into database
        private void UpdateRecord(int gender, int designation)
        {
            if (FNTextBox.Text != string.Empty && EmailTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName=@LastName, Contact=@Contact, Email=@Email, DateOfBirth=@DateOfBirth, Gender=@Gender WHERE Id=@Id; UPDATE Advisor SET Designation=@Designation, Salary=@Salary WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@FirstName", FNTextBox.Text);
                    cmd.Parameters.AddWithValue("@LastName", LNTextBox.Text);
                    cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text);
                    cmd.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Date.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Designation", designation);
                    cmd.Parameters.AddWithValue("@Salary", int.Parse(SalaryTextBox.Text));
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated");
                    findParent();// this function is used here for display student method purpose and scrollViewer visibility purpose
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

        // give gender of current Advisor whose data will enter in form
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


        // give Designation of current Advisor whose data will enter in form
        private int ReturnDesignation()
        {
            int designation = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE Category='DESIGNATION' AND Value=@designation", con);
            cmd.Parameters.AddWithValue("@designation", DesComboBox.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                designation = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return designation;

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int gender = ReturnGender();
            int designation = ReturnDesignation();

            if (FirstNameValidations() && LastNameValidations() && ContactNumberValidation() && EmailValidation() && SalaryValidation())
            {
                if (gender > 0 && designation > 0)
                {
                    if (SaveButtonTxt.Text == "Save")
                    {
                        SaveRecord(gender, designation);
                    }
                    else
                    {
                        UpdateRecord(gender, designation);
                    }

                }
                else
                {
                    MessageBox.Show("First fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

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


        private bool SalaryValidation()
        {
            string numbers = "0123456789";
            bool isValid = true;
            if (SalaryTextBox.Text == "")
            {
                isValid = false;
                MessageBox.Show("Enter salary", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SalaryTextBox.Text.Length <= 7)
            {
                foreach (char n in SalaryTextBox.Text)
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
                MessageBox.Show("Salary is out of range", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


    }
}
