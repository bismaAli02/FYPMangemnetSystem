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

namespace FYPManagementSystem.UserControlls.GroupUserControlls
{
    public partial class GroupStuUC : UserControl
    {
        public GroupStuUC()
        {
            InitializeComponent();
            DisplayGroups();

        }

        int totalMembers = 5; // limit you cannot add more than 5 members in a group

        // when button click it create a new group in database
        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateGroupButton.Content.ToString() == "Create Group")
            {
                int id = 0;
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Group](Created_On) VALUES(@Created_On);SELECT TOP 1 Id FROM [Group] ORDER BY Id DESC", con);
                    cmd.Parameters.AddWithValue("@Created_On", DateTime.Today);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id = int.Parse(reader["ID"].ToString());
                    }
                    reader.Close();
                    GroupScroll.Visibility = Visibility.Visible;
                    GroupUC.Content = new EditGroupUC(id);
                    GroupUC.Visibility = Visibility.Visible;
                    CreateGroupButton.Content = "Go Back";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                GroupScroll.Visibility = Visibility.Collapsed;
                CreateGroupButton.Content = "Create Group";
            }
            DisplayGroups();
        }

        // simply display all groups
        public void DisplayGroups()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT('Group',G.Id) AS GroupId,P.Title,COUNT(S.RegistrationNo) AS RegNo,('" + totalMembers + "' - COUNT(S.RegistrationNo)) AS RemainingStudents,(SELECT FORMAT(G.Created_On, 'dd/MM/yyyy')) AS Created_On FROM [Group] AS G LEFT JOIN GroupProject AS GP ON G.Id=GP.GroupId LEFT JOIN GroupStudent AS GS ON GS.GroupId=G.Id LEFT JOIN Project AS P ON GP.ProjectId=P.Id LEFT JOIN Student AS S ON S.Id = GS.StudentId WHERE GS.Status=3 GROUP BY G.Id,P.Id, P.Title, G.Created_On", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupDataGrid.ItemsSource = dt.DefaultView;




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //it open the group detail UC also it give the id of selectedgroup
        private void GDButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = GroupDataGrid.SelectedItem as DataRowView; // as is used for typecasting through as we convert selected item in dataRowView 
            int id2;
            string id1 = "";
            string projectTitle;
            if (selectedRow != null)
            {
                string id = selectedRow["GroupId"].ToString();
                projectTitle = selectedRow["Title"].ToString();

                for (int i = 5; i < id.Length; i++)
                {
                    id1 += id[i];
                }
                id2 = int.Parse(id1);
                GroupScroll.Visibility = Visibility.Visible;
                GroupUC.Content = new GroupDetailUC(id2, projectTitle);
                GroupUC.Visibility = Visibility.Visible;
                CreateGroupButton.Content = "Go Back";

            }

        }
        private void deleteTuple(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                // delete a Group from Group tabel
                SqlCommand cmd = new SqlCommand("DELETE FROM GroupEvaluation WHERE GroupId =@Id;DELETE FROM GroupProject WHERE GroupId =@Id;DELETE FROM GroupStudent WHERE GroupId =@Id ;DELETE FROM [Group] WHERE Id =@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = GroupDataGrid.SelectedItem as DataRowView; // as is used for typecasting through as we convert selected item in dataRowView 
            int id2;
            string id1 = "";
            if (selectedRow != null)
            {
                string id = selectedRow["GroupId"].ToString();

                for (int i = 5; i < id.Length; i++)
                {
                    id1 += id[i];
                }
                id2 = int.Parse(id1);
                deleteTuple(id2);
                GroupScroll.Visibility = Visibility.Collapsed;
                DisplayGroups();
                CreateGroupButton.Content = "Create Group";

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }


        //It gets the selected row as a DataRowView object, extracts the ID of the group to be edited from the "GroupId" column, passes it to a new instance of the EditGroupUC user control. 
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = GroupDataGrid.SelectedItem as DataRowView; // as is used for typecasting through as we convert selected item in dataRowView 
            int id2;
            string id1 = "";
            if (selectedRow != null)
            {
                string id = selectedRow["GroupId"].ToString();

                for (int i = 5; i < id.Length; i++)
                {
                    id1 += id[i];
                }
                id2 = int.Parse(id1);
                GroupScroll.Visibility = Visibility.Visible;
                GroupUC.Content = new EditGroupUC(id2);
                GroupUC.Visibility = Visibility.Visible;
                CreateGroupButton.Content = "Go Back";


            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }
        }
    }
}
