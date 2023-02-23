using CRUD_Operations;
using System;
using System.Collections.Generic;
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


namespace FYPManagementSystem.UserControlls.StudentsUserControlls
{
    /// <summary>
    /// Interaction logic for AddStudentUC.xaml
    /// </summary>
    public partial class AddStudentUC : UserControl
    {
        int id;
        public AddStudentUC()
        {
            InitializeComponent();
            savebuttontxt.Text = "Save";
        }
        public AddStudentUC(string firstName, string lastName, string contact, string email, int gender, string regNo, string dob, int id)
        {
            InitializeComponent();
            savebuttontxt.Text = "Update";
            FNTextBox.Text = firstName;
            LNTextBox.Text = lastName;
            ContactTextBox.Text = contact;
            EmailTextBox.Text = email;
            if (gender == 1)
            {
                genderComboBox.Text = "MALE";
            }
            else
            {
                genderComboBox.Text = "FEMALE";
            }
            Date.Text = dob;
            RNTextBox.Text = regNo;
            this.id = id;
        }

        private void BackButton_Click_1(object sender, RoutedEventArgs e)
        {
            EmptyForm();
            this.Visibility = Visibility.Collapsed;
        }

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
        private void LockForm()
        {
            FNTextBox.IsReadOnly = true;
            LNTextBox.IsReadOnly = true;
            genderComboBox.IsReadOnly = true;
            RNTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            ContactTextBox.IsReadOnly = true;
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int gender = 0;
            if (genderComboBox.SelectedIndex == 0)
            {
                gender = 1;
            }
            else
            {
                gender = 2;
            }

            if (savebuttontxt.Text == "Save")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) VALUES (@FirstName,@LastName, @Contact,@Email,@DateOfBirth, @Gender); INSERT INTO Student(Id,RegistrationNo) VALUES ((SELECT Id FROM Person WHERE FirstName = @FirstName AND LastName=@LastName AND Contact=@Contact AND Email=@Email AND DateOfBirth=@DateOfBirth AND Gender=@Gender) ,@RegNo);", con);
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
            else
            {
                if (FNTextBox.Text != string.Empty && RNTextBox.Text != string.Empty)
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
                    EmptyForm();
                    LockForm();
                }
                else
                {
                    MessageBox.Show("Please Select any record to Update");
                }
                //savebuttontxt.Text = "Save";
            }

        }


    }
}
