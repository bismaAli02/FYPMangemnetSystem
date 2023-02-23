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

namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{
    /// <summary>
    /// Interaction logic for AddAdvUC.xaml
    /// </summary>
    public partial class AddAdvUC : UserControl
    {
        int id;
        public AddAdvUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
        }
        public AddAdvUC(string firstName, string lastName, string contact, string email, int gender, int designation, string salary, string dob, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
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
            if (designation == 3)
            {
                DesComboBox.Text = "Professor";
            }
            else if (designation == 4)
            {
                DesComboBox.Text = "Associate Professor";
            }
            else if (designation == 5)
            {
                DesComboBox.Text = "Assistant Professor";
            }
            else if (designation == 6)
            {
                DesComboBox.Text = "Lecturer";
            }
            else if (designation == 7)
            {
                DesComboBox.Text = "Industry Professional";
            }
            this.id = id;
            SalaryTextBox.Text = salary;
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
            DesComboBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            ContactTextBox.Text = string.Empty;
            SalaryTextBox.Text = string.Empty;
        }
        private void LockForm()
        {
            FNTextBox.IsReadOnly = true;
            LNTextBox.IsReadOnly = true;
            genderComboBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            ContactTextBox.IsReadOnly = true;
            SalaryTextBox.IsReadOnly = true;
            DesComboBox.IsReadOnly = true;
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int gender = 0;
            int designation = 0;
            if (genderComboBox.SelectedIndex == 0)
            {
                gender = 1;
            }
            else
            {
                gender = 2;
            }

            if (DesComboBox.SelectedIndex == 0)
            {
                designation = 3;
            }
            else if (DesComboBox.SelectedIndex == 1)
            {
                designation = 4;
            }
            else if (DesComboBox.SelectedIndex == 2)
            {
                designation = 5;
            }
            else if (DesComboBox.SelectedIndex == 3)
            {
                designation = 6;
            }
            else if (DesComboBox.SelectedIndex == 4)
            {
                designation = 7;
            }

            if (SaveButtonTxt.Text == "Save")
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
                    cmd.Parameters.AddWithValue("@Salary", SalaryTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                    EmptyForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
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
                        cmd.Parameters.AddWithValue("@Salary", SalaryTextBox.Text);
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
            }

        }


    }
}
