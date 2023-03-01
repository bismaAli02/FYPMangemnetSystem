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
using FYPManagementSystem.UserControlls.ProjectsUserControlls;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;

namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{
    /// <summary>
    /// Interaction logic for AddProjectAdvUC.xaml
    /// </summary>
    public partial class AddProjectAdvUC : UserControl
    {
        public AddProjectAdvUC()
        {
            InitializeComponent();
            ProjectToComboBox();
            MainAdvToComboBox();
            CoAdvToComboBox();
            IndustryAdvToComboBox();
        }

        private void ProjectToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM Project AS P LEFT JOIN ProjectAdvisor AS PA ON P.Id = PA.ProjectId WHERE PA.AdvisorId IS NULL", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                ProjTitleComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                ProjTitleComboBox.DisplayMemberPath = "Title";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainAdvToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @CoAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @IndustryAdvisor", con);
                cmd.Parameters.AddWithValue("@CoAdvisor", Co_AdvisorComboBox.Text);
                cmd.Parameters.AddWithValue("@IndustryAdvisor", IAComboBox.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                MainAdvComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                MainAdvComboBox.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int ProjectIdFromDataBase()
        {
            int projId = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Project AS P WHERE P.Title=@Title", con);
            cmd.Parameters.AddWithValue("@Title", ProjTitleComboBox.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                projId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return projId;
        }

        private int AdvisorIdFromDataBase(string name)
        {
            int advId = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT A.Id FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE  CONCAT(P.FirstName,' ',P.LastName) = @AdvisorName", con);
            cmd.Parameters.AddWithValue("@AdvisorName", name);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                advId = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            return advId;
        }

        private void CoAdvToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @MainAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @IndustryAdvisor", con);
                cmd.Parameters.AddWithValue("@MainAdvisor", MainAdvComboBox.Text);
                cmd.Parameters.AddWithValue("@IndustryAdvisor", IAComboBox.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                Co_AdvisorComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                Co_AdvisorComboBox.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void EmptyForm()
        {
            if (ProjTitleComboBox.IsEnabled == true)
            {
                ProjectToComboBox();
            }
            MainAdvToComboBox();
            CoAdvToComboBox();
            IndustryAdvToComboBox();
            Co_AdvButtonTxt.Text = "Assign";
            MainAdvButtonTxt.Text = "Assign";
            IAButtonTxt.Text = "Assign";


        }

        private void IndustryAdvToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @CoAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @MainAdvisor", con);
                cmd.Parameters.AddWithValue("@MainAdvisor", MainAdvComboBox.Text);
                cmd.Parameters.AddWithValue("@CoAdvisor", Co_AdvisorComboBox.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                IAComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                IAComboBox.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainAdvButton_Click(object sender, RoutedEventArgs e)
        {
            int projId = ProjectIdFromDataBase();
            int advId = AdvisorIdFromDataBase(MainAdvComboBox.Text);
            if (MainAdvButtonTxt.Text == "Assign")
            {
                AssignProject("Main Advisor", projId, advId, MainAdvButtonTxt);
            }
            else if (MainAdvButtonTxt.Text == "Re-Assign")
            {
                ReAssignProject("Main Advisor", projId, advId);
            }
        }

        private void Co_AdvButton_Click(object sender, RoutedEventArgs e)
        {
            int projId = ProjectIdFromDataBase();
            int advId = AdvisorIdFromDataBase(Co_AdvisorComboBox.Text);
            if (Co_AdvButtonTxt.Text == "Assign")
            {
                AssignProject("Co-Advisor", projId, advId, Co_AdvButtonTxt);
            }
            else if (Co_AdvButtonTxt.Text == "Re-Assign")
            {
                ReAssignProject("Co-Advisor", projId, advId);
            }

        }

        private void AssignProject(string role, int projId, int advId, TextBlock btnTxt)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProjectAdvisor VALUES (@AdvisorId, @ProjectId, (SELECT Id FROM Lookup  WHERE Category='ADVISOR_ROLE' AND Value='" + role + "'), @AssignmentDate)", con);
                cmd.Parameters.AddWithValue("@ProjectId", projId);
                cmd.Parameters.AddWithValue("@AdvisorId", advId);
                cmd.Parameters.AddWithValue("@AssignmentDate", DateTime.Now);

                cmd.ExecuteNonQuery();
                btnTxt.Text = "Re-Assign";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReAssignProject(string role, int projId, int advId)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE ProjectAdvisor SET AssignmentDate=@Date, AdvisorId=@AdvisorId WHERE ProjectId=@ProjectId AND AdvisorRole =(SELECT Id FROM Lookup  WHERE Category='ADVISOR_ROLE' AND Value=" + role + "Industry Advisor') ", con);
                cmd.Parameters.AddWithValue("@ProjectId", projId);
                cmd.Parameters.AddWithValue("@AdvisorId", advId);
                cmd.Parameters.AddWithValue("@AssignmentDate", DateTime.Now);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void IAButton_Click(object sender, RoutedEventArgs e)
        {
            int projId = ProjectIdFromDataBase();
            int advId = AdvisorIdFromDataBase(IAComboBox.Text);
            if (IAButtonTxt.Text == "Assign")
            {
                AssignProject("Industry Advisor", projId, advId, IAButtonTxt);
            }
            else if (IAButtonTxt.Text == "Re-Assign")
            {
                ReAssignProject("Industry Advisor", projId, advId);
            }
        }

        private void ProjTitleComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void MainAdvComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string coAdv, indusAdv;
            coAdv = Co_AdvisorComboBox.Text;
            indusAdv = IAComboBox.Text;
            CoAdvToComboBox();
            Co_AdvisorComboBox.Text = coAdv;
            IAComboBox.Text = indusAdv;
            IndustryAdvToComboBox();
            Co_AdvisorComboBox.Text = coAdv;
            IAComboBox.Text = indusAdv;
        }

        private void Co_AdvisorComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string mainAdv, indusAdv;
            mainAdv = MainAdvComboBox.Text;
            indusAdv = IAComboBox.Text;
            MainAdvToComboBox();
            MainAdvComboBox.Text = mainAdv;
            IAComboBox.Text = indusAdv;
            IndustryAdvToComboBox();
            MainAdvComboBox.Text = mainAdv;
            IAComboBox.Text = indusAdv;
        }

        private void IAComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string mainAdv, coAdv;
            mainAdv = MainAdvComboBox.Text;
            coAdv = Co_AdvisorComboBox.Text;
            MainAdvToComboBox();
            MainAdvComboBox.Text = mainAdv;
            Co_AdvisorComboBox.Text = coAdv;
            CoAdvToComboBox();
            MainAdvComboBox.Text = mainAdv;
            Co_AdvisorComboBox.Text = coAdv;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }
    }
}
