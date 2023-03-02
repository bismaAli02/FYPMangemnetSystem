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
            MainAdvToComboBox("", "");
            CoAdvToComboBox("", "");
            IndustryAdvToComboBox("", "");
        }

        public AddProjectAdvUC(int projId, string title, string mainAdv, string coAdv, string industryAdv)
        {
            InitializeComponent();
            AllProjectToComboBox();
            ProjTitleComboBox.Text = title;
            MainAdvToComboBox(coAdv, industryAdv);
            CoAdvToComboBox(mainAdv, industryAdv);
            IndustryAdvToComboBox(coAdv, mainAdv);
            IAComboBox.Text = industryAdv;
            Co_AdvisorComboBox.Text = coAdv;
            MainAdvComboBox.Text = mainAdv;
            ProjTitleComboBox.IsEnabled = false;
            CancelButton.Visibility = Visibility.Collapsed;
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
            while (parent != null && !(parent is AssignAdvUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is AssignAdvUC)
            {
                AssignAdvUC par = parent as AssignAdvUC;
                par.DisplayAdvisors();
                Button btn = (Button)par.FindName("AssignProjectButton");
                btn.Content = "Assign Advisor To Project";
            }
        }
        private void AllProjectToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM Project", con);
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
        private void MainAdvToComboBox(string coAdv, string industryAdv)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @CoAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @IndustryAdvisor", con);
                cmd.Parameters.AddWithValue("@CoAdvisor", coAdv);
                cmd.Parameters.AddWithValue("@IndustryAdvisor", industryAdv);
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
        private void RemoveAdvisor(int projId)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM ProjectAdvisor WHERE ProjectId=@projId", con);
            cmd.Parameters.AddWithValue("@projId", projId);
            cmd.ExecuteNonQuery();

        }

        private void CoAdvToComboBox(string mainAdv, string industryAdv)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @MainAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @IndustryAdvisor", con);
                cmd.Parameters.AddWithValue("@MainAdvisor", mainAdv);
                cmd.Parameters.AddWithValue("@IndustryAdvisor", industryAdv);
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
                MainAdvToComboBox("", "");
                CoAdvToComboBox("", "");
                IndustryAdvToComboBox("", "");
                AssignAdvButtonTxt.Text = "Assign";
            }


        }

        private void IndustryAdvToComboBox(string coAdv, string mainAdv)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name FROM Advisor AS A JOIN Person AS P ON A.Id = P.Id WHERE CONCAT(P.FirstName,' ',P.LastName) <> @CoAdvisor AND CONCAT(P.FirstName,' ',P.LastName) <> @MainAdvisor", con);
                cmd.Parameters.AddWithValue("@CoAdvisor", coAdv);
                cmd.Parameters.AddWithValue("@MainAdvisor", mainAdv);
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

        private void AssignProject(string role, int projId, int advId)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProjectAdvisor VALUES (@AdvisorId, @ProjectId, (SELECT Id FROM Lookup  WHERE Category='ADVISOR_ROLE' AND Value='" + role + "'), @AssignmentDate)", con);
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
        private void MainAdvComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string coAdv, indusAdv;
            coAdv = Co_AdvisorComboBox.Text;
            indusAdv = IAComboBox.Text;
            CoAdvToComboBox(MainAdvComboBox.Text, indusAdv);
            Co_AdvisorComboBox.Text = coAdv;
            IAComboBox.Text = indusAdv;
            IndustryAdvToComboBox(coAdv, MainAdvComboBox.Text);
            Co_AdvisorComboBox.Text = coAdv;
            IAComboBox.Text = indusAdv;
        }

        private void Co_AdvisorComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string mainAdv, indusAdv;
            mainAdv = MainAdvComboBox.Text;
            indusAdv = IAComboBox.Text;
            MainAdvToComboBox(Co_AdvisorComboBox.Text, indusAdv);
            MainAdvComboBox.Text = mainAdv;
            IAComboBox.Text = indusAdv;
            IndustryAdvToComboBox(Co_AdvisorComboBox.Text, mainAdv);
            MainAdvComboBox.Text = mainAdv;
            IAComboBox.Text = indusAdv;
        }

        private void IAComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string mainAdv, coAdv;
            mainAdv = MainAdvComboBox.Text;
            coAdv = Co_AdvisorComboBox.Text;
            MainAdvToComboBox(coAdv, IAComboBox.Text);
            MainAdvComboBox.Text = mainAdv;
            Co_AdvisorComboBox.Text = coAdv;
            CoAdvToComboBox(mainAdv, IAComboBox.Text);
            MainAdvComboBox.Text = mainAdv;
            Co_AdvisorComboBox.Text = coAdv;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        private void AssignAdvButton_Click(object sender, RoutedEventArgs e)
        {
            int projId = ProjectIdFromDataBase();
            int mainAdvId = AdvisorIdFromDataBase(MainAdvComboBox.Text);
            int coAdvId = AdvisorIdFromDataBase(Co_AdvisorComboBox.Text);
            int industryAdvId = AdvisorIdFromDataBase(IAComboBox.Text);
            RemoveAdvisor(projId);
            AssignProject("Main Advisor", projId, mainAdvId);
            AssignProject("Co-Advisor", projId, coAdvId);
            AssignProject("Industry Advisor", projId, industryAdvId);
            MessageBox.Show("Assigned Successfullly");
            findParent();

        }
    }
}
