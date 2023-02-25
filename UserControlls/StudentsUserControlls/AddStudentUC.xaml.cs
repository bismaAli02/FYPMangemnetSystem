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
            GenderToComboBox();
        }
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
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

        private void UpdateRecord(int gender)
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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int gender = ReturnGender();
            if (FNTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter  First Name ", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (RNTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Registration Number", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EmailTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Email", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (savebuttontxt.Text == "Save")
                {
                    SaveRecord(gender);
                }
                else
                {
                    UpdateRecord(gender);
                }
                findParent();
            }
        }


    }
}
