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
            if (APTextBox.Text != string.Empty)
            {
                title = APTextBox.Text;
            }
            else if (ProjectComboBox.Text != string.Empty)
            {
                title = ProjectComboBox.Text;
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
                SqlCommand cmd = new SqlCommand("SELECT RegistrationNo FROM Student", con);
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
