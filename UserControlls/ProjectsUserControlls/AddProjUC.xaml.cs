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

namespace FYPManagementSystem.UserControlls.ProjectsUserControlls
{
    public partial class AddProjUC : UserControl
    {
        int id; // this attribute is used for Project id
        public AddProjUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
        }
        // this parametrized constructer made for update operation purpose
        public AddProjUC(string description, string title, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
            DescriptionTextBox.Text = description;
            TitleTextBox.Text = title;
            this.id = id;
        }

        //this code clears  all the controls
        private void EmptyForm()
        {
            DescriptionTextBox.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
        }


        // this code clears the controls 
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        //this code find the parent ProjUC control. It sets the text of a Button to "Add Project".function is used to call a method of Proj UC (display Projects)
        private void findParentUserControl()
        {
            //this code is used to find immediate parent of the current control
            var parent = VisualTreeHelper.GetParent(this);

            // this loop traverse on tree(user Controls)
            while (parent != null && !(parent is ProjUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is ProjUC)
            {
                ProjUC par = parent as ProjUC;
                par.DisplayProjects();
                Button btn = (Button)par.FindName("AddProjButton");
                btn.Content = "Add Project";

            }
        }
        // this function simply save data into data base
        private void SaveRecord()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Project(Description,Title) VALUES(@Description,@Title)", con);
                cmd.Parameters.AddWithValue("@Description", DescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@Title", TitleTextBox.Text);
                EmptyForm();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // this function simply Update data from data base
        private void UpdateRecord()
        {
            if (DescriptionTextBox.Text != string.Empty && TitleTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Project SET Description =@Description, Title=@Title WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@Description", DescriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Title", TitleTextBox.Text);
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
            if (TitleTextBox.Text == string.Empty)// validation for title
            {
                MessageBox.Show("Please Select Title of the Project", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DescriptionTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Select Description for the Project", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (ValidationForProjectTitle())
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
                // this function is used here for displayProjects method purpose 
                findParentUserControl();
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

        // validation
        private bool ValidationForProjectTitle()
        {
            string title = TitleTextBox.Text;
            bool isValid = false;
            for (int i = 0; i < title.Length; i++)
            {
                if ((title[i] > 64 && title[i] < 91) || (title[i] > 96 && title[i] < 123))
                {

                    isValid = true;
                    break;
                }
            }
            if (isValid)
            {
                if (ValidationInDatabase("SELECT Title FROM Project WHERE Title='" + title + "' AND Id<>" + id))
                {

                    MessageBox.Show("You canot add Projects with same name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                }
            }
            else
            {
                MessageBox.Show("Project Title cannot be only integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return isValid;
        }
    }
}
