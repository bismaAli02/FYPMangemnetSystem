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
            GenderToComboBox();
            DesignationToComboBox();
        }
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
        private void findParent()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is ScrollViewer))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is ScrollViewer)
            {
                ScrollViewer par = parent as ScrollViewer;
                par.Visibility = Visibility.Collapsed;
                findParentUserControl();

            }
        }

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

        private void GenderToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Value FROM Lookup WHERE Category=@Category", con);
                cmd.Parameters.AddWithValue("@Category", "GENDER");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                genderComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                genderComboBox.DisplayMemberPath = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

            if (FNTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter  First Name ", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DesComboBox.Text == string.Empty)
            {
                MessageBox.Show("Please Select Designation", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EmailTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Email", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SalaryTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Salary", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (SaveButtonTxt.Text == "Save")
                {
                    SaveRecord(gender, designation);
                }
                else
                {
                    UpdateRecord(gender, designation);
                }
                findParent();

            }
        }


    }
}
