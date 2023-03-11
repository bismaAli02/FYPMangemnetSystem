using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using MaterialDesignColors.Recommended;

namespace FYPManagementSystem.UserControlls.GroupUserControlls
{
    public partial class EditGroupUC : UserControl
    {
        int groupId;// it is used to get groupID for a specific group
        int projId; // it is used to get Project ID for a specific group
        public EditGroupUC(int id)
        {
            InitializeComponent();
            this.groupId = id;
            LoadStudentsIntoGrids();
            CurrentProjectFromDataBase();
            ProjectToComboBox();
            StudentToComboBox();
            ProjectIdFromDataBase();

            // use a single button for two purpose if text is "Assign" it means no project assign yet if it is "Re-Assign" it means already project assigned now you can "Re-Assign" project
            if (APTextBox.Text == string.Empty)
            {
                AssignButtonTxt.Text = "Assign";
            }
            else
            {
                AssignButtonTxt.Text = "Re-Assign";
            }


        }

        //This method retrieves the current project title associated with the current group from the database and displays it in a text box.    
        private void CurrentProjectFromDataBase()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Title FROM Project AS P,GroupProject AS GP WHERE  P.Id=GP.ProjectId AND GP.GroupId=@GroupId", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                APTextBox.Text = reader["Title"].ToString();
            }
            reader.Close();
        }

        //This function retrieves the project ID of a selected project title from the database based
        private void ProjectIdFromDataBase()
        {
            string title = "";
            if (ProjectComboBox.Text != string.Empty)
            {
                title = ProjectComboBox.Text;
            }
            else if (APTextBox.Text != string.Empty)
            {
                title = APTextBox.Text;
            }
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Project AS P WHERE P.Title=@Title", con);
            cmd.Parameters.AddWithValue("@Title", title);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                projId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
        }

        //This method retrieves the ID of a student from the database based on their registration number.
        private int StudentIdFromDataBase()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Student AS S WHERE S.RegistrationNo=@RegNo", con);
            cmd.Parameters.AddWithValue("@RegNo", AddStuComboBox.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return id;
        }

        //The code checks whether the "Assign" button text is "Assign" or "Re-Assign". If the text is "Assign", it inserts a new record in the GroupProject table with the selected project and group ID. If the text is "Re-Assign", it updates the GroupProject table with the new project 
        private void AssignProjButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectIdFromDataBase();
            if (AssignButtonTxt.Text == "Assign")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO GroupProject VALUES (@ProjectId, @GroupId, @Date)", con);
                    cmd.Parameters.AddWithValue("@ProjectId", projId);
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    AssignButtonTxt.Text = "Re-Assign";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (AssignButtonTxt.Text == "Re-Assign")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE GroupProject SET ProjectId=@ProjectId,AssignmentDate=@Date WHERE GroupId=@GroupId", con);
                    cmd.Parameters.AddWithValue("@ProjectId", projId);
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            CurrentProjectFromDataBase();
            ProjectToComboBox();
            ProjectComboBox.Text = string.Empty;
        }

        private void ProjectToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM Project WHERE Title<>@Title", con);
                cmd.Parameters.AddWithValue("@Title", APTextBox.Text);

                //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                //this line of code creates a new DataSet object, which can be used to work with data in a tabular format.
                DataSet dataSet = new DataSet();
                //it retrieves data from a data source and fills a DataSet object with the retrieved data.
                dataAdapter.Fill(dataSet);
                // this code help to display data in comboBox
                ProjectComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                // ProjectComboBox control will display the data from the "Title" column of the data source in its dropdown list.
                ProjectComboBox.DisplayMemberPath = "Title";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StudentToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT S.RegistrationNo FROM Student S LEFT JOIN (SELECT * FROM GroupStudent GS WHERE GS.AssignmentDate = ( SELECT MAX(GS1.AssignmentDate) FROM GroupStudent GS1 WHERE GS1.StudentId = GS.StudentId)) AS recentStudent ON S.Id = recentStudent.StudentID WHERE recentStudent.Status = 4 OR recentStudent.GroupID IS NULL", con);

                //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                //this line of code creates a new DataSet object, which can be used to work with data in a tabular format.
                DataSet dataSet = new DataSet();

                //it retrieves data from a data source and fills a DataSet object with the retrieved data.
                dataAdapter.Fill(dataSet);

                // this code help to display data in comboBox
                AddStuComboBox.ItemsSource = dataSet.Tables[0].DefaultView;

                // AddStuComboBox control will display the data from the "RegistrationNo" column of the data source in its dropdown list.
                AddStuComboBox.DisplayMemberPath = "RegistrationNo";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // add student into a specific group
        private void AddStuButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupStudentDataGrid.Items.Count < 5)
            {
                int stuId;
                if (AddStuComboBox.Text != string.Empty)
                {
                    stuId = StudentIdFromDataBase();
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("INSERT INTO GroupStudent VALUES (@GroupId, @StudentId,@Status,@Date)", con);
                        cmd.Parameters.AddWithValue("@StudentId", stuId);
                        cmd.Parameters.AddWithValue("@GroupId", groupId);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Status", 3);
                        cmd.ExecuteNonQuery();
                        AddStuComboBox.Text = string.Empty;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                StudentToComboBox();
                LoadStudentsIntoGrids();
            }
            else
            {
                MessageBox.Show("Group reach its maximum Limit!!!");
            }
        }

        // display students of a specific group
        private void LoadStudentsIntoGrids()
        {
            var con = Configuration.getInstance().getConnection();

            // sql querey that retrieve data for students by joining lookup , student and person tabel
            SqlCommand cmd = new SqlCommand("SELECT S.Id ,S.RegistrationNo AS RegNo, CONCAT(P.FirstName ,' ',P.LastName) AS Name,L.Value AS Status  FROM GroupStudent AS GS JOIN Lookup AS L ON GS.Status = L.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS P ON P.Id = S.Id WHERE GS.GroupId = @GroupId AND GS.Status=@Status", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            cmd.Parameters.AddWithValue("@Status", 3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GroupStudentDataGrid.ItemsSource = dt.DefaultView;

        }

        //It first retrieves the currently selected row in a data grid view, then executes a SQL command to update a record in the database  status of "In Active" for the selected student in the selected group.
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

            DataRowView selectedRow = GroupStudentDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE GroupStudent SET AssignmentDate = @Date, Status=@Status WHERE GroupId=@GroupId AND StudentId = @StudentId", con);
                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(selectedRow["Id"].ToString()));
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", 4);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            LoadStudentsIntoGrids();
        }

    }
}
