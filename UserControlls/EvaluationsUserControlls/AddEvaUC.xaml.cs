using CRUD_Operations;
using FYPManagementSystem.UserControlls.ProjectsUserControlls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    public partial class AddEvaUC : UserControl
    {
        int id; // this attribute is used for Evaluation id 
        public AddEvaUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
        }

        // this parametrized constructer made for update operation purpose
        public AddEvaUC(string name, int totalMarks, int totalWeightage, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
            NameTextBox.Text = name;
            TMTextBox.Text = totalMarks.ToString();
            TWTextBox.Text = totalWeightage.ToString();
            this.id = id;
        }


        //this function validate a query in a database  and checking whether it returns any rows. If the query returns rows, the function returns true otherwise, it returns false. 
        private bool ValidationInDatabase(string query)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)// execute query and read the resulting rows one at a time.
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
        // validation title name cannot be repeated or it cannot be only integer
        private bool titleNameValidations()
        {
            string name = NameTextBox.Text;
            bool isValid = true;

            if (name == "")
            {
                MessageBox.Show("Evaluation Name cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
                return isValid;
            }
            else if (name == " ")
            {
                MessageBox.Show("Evaluation Name cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            isValid = false;
            for (int i = 0; i < name.Length; i++)
            {
                if (((name[i] > 64 && name[i] < 91) || (name[i] > 96 && name[i] < 123)))
                {
                    isValid = true;
                    break;
                }
            }

            if (isValid)
            {
                isValid = !ValidationInDatabase("SELECT Name FROM Evaluation WHERE Name='" + name + "' AND Id<>" + id);
                if (!ValidationInDatabase("SELECT Name FROM Evaluation WHERE Name='" + name + "' AND Id<>" + id))
                {
                    MessageBox.Show("There already exists one Evaluation With the same name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Evaluation Title must contain at least one Alphabet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return isValid;
        }


        //calculate obtained marks on the basis of weightage 
        private bool WeightageSumCalculate(int weightage)
        {
            int totalWeightage = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT SUM(TotalWeightage) AS total FROM Evaluation WHERE Id<>'" + id + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                totalWeightage = int.Parse(reader["total"].ToString());
            }
            reader.Close();
            if (totalWeightage + weightage > 100)
            {
                return false;
            }
            return true;
        }

        //this code clears  all the controls
        private void EmptyForm()
        {
            NameTextBox.Text = string.Empty;
            TMTextBox.Text = string.Empty;
            TWTextBox.Text = string.Empty;
        }



        // save the record into database
        private void SaveRecord()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Evaluation(Name,TotalMarks,TotalWeightage) VALUES(@Name,@TotalMarks,@TotalWeightage)", con);
                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", TMTextBox.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", TWTextBox.Text);
                EmptyForm();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // update the record into database
        private void UpdateRecord()
        {
            if (NameTextBox.Text != string.Empty && TMTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name =@Name, TotalMarks=@TotalMarks, TotalWeightage=@TotalWeightage WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                    cmd.Parameters.AddWithValue("@TotalMarks", TMTextBox.Text);
                    cmd.Parameters.AddWithValue("@TotalWeightage", TWTextBox.Text);
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
            if (NameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Name of the Evaluation", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (TMTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Total Marks for the Project", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (TWTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Total Weightage for the Project", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!WeightageSumCalculate(int.Parse(TWTextBox.Text.ToString())))
            {
                MessageBox.Show("Invalid Total Weightage as exceeded 100 for all evaluation", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (SaveButtonTxt.Text == "Save")
                {
                    SaveRecord();
                }
                else
                {
                    UpdateRecord();
                }
                this.Visibility = Visibility.Collapsed;
                findParentUserControl();

            }


        }
        //this code find the parent "EvaUC" control. It sets the text of a Button to "Add Evaluation".function is used to call a method of EvaUC (displayEvaluation)
        private void findParentUserControl()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is EvaUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is EvaUC)
            {
                EvaUC par = parent as EvaUC;
                par.DisplayEvaluation();
                Button btn = (Button)par.FindName("AddEvaButton");
                btn.Content = "Add Evaluation";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }
    }
}
