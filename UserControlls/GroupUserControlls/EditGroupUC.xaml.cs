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
    /// <summary>
    /// Interaction logic for EditGroupUC.xaml
    /// </summary>
    public partial class EditGroupUC : UserControl
    {
        int groupId;
        int projId;
        public EditGroupUC(int id)
        {
            InitializeComponent();
            this.groupId = id;
            LoadStudentsIntoGrids();
            CurrentProjectFromDataBase();
            ProjectToComboBox();
            StudentToComboBox();
            ProjectIdFromDataBase();
            if (APTextBox.Text == string.Empty)
            {
                AssignButtonTxt.Text = "Assign";
            }
            else
            {
                AssignButtonTxt.Text = "Re-Assign";
            }

        }

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
        private void StatusComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void ProjectToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM Project WHERE Title<>@Title", con);
                cmd.Parameters.AddWithValue("@Title", APTextBox.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                ProjectComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
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
                SqlCommand cmd = new SqlCommand("SELECT RegistrationNo FROM Student AS S LEFT JOIN GroupStudent AS GS ON S.Id = GS.StudentId WHERE GS.StudentId IS NULL OR GS.Status = 4", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                AddStuComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                AddStuComboBox.DisplayMemberPath = "RegistrationNo";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddStuButton_Click(object sender, RoutedEventArgs e)
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

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

            DataRowView selectedRow = GroupStudentDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE GroupStudent SET Status=@Status WHERE GroupId=@GroupId AND StudentId = @StudentId", con);
                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(selectedRow["Id"].ToString()));
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
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

        /* private int ReturnGender()
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

         }*/

    }
}
