using CRUD_Operations;
using FYPManagementSystem.UserControlls.ProjectsUserControlls;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
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
    /// <summary>
    /// Interaction logic for GroupDetailUC.xaml
    /// </summary>
    public partial class GroupDetailUC : UserControl
    {
        int groupId;
        string projectTitle;
        public GroupDetailUC(int groupId, string projectTitle)
        {
            InitializeComponent();
            this.groupId = groupId;
            this.projectTitle = projectTitle;
            DisplayPieChart();
            GroupIdHeader.Text = "Group Id: " + groupId;
            ProjectTitleHeader.Text = "Project: " + projectTitle;
            DisplayStudent();
        }

        private void DisplayPieChart()
        {
            List<int> studentCount = new List<int>();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS Id FROM GroupStudent WHERE GroupId=@GroupId AND Status=3", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                studentCount.Add(int.Parse(reader["Id"].ToString()));
            }
            reader.Close();
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) AS Id FROM GroupStudent WHERE GroupId=@GroupId AND Status=4", con);
            cmd1.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {
                studentCount.Add(int.Parse(reader1["Id"].ToString()));
            }
            studentCount.Add(5 - studentCount[0]);
            reader1.Close();

            LiveCharts.SeriesCollection psc = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.PieSeries
                {
                    Values = new LiveCharts.ChartValues<decimal> {studentCount[0]},
                    Title="Active Students"
                },
                new LiveCharts.Wpf.PieSeries
                {
                    Values = new LiveCharts.ChartValues<decimal> {studentCount[1]},
                    Title= "In-Active Students"
                },
                new LiveCharts.Wpf.PieSeries
                {
                    Values = new LiveCharts.ChartValues<decimal> {studentCount[2]},
                    Title="Remaining Slots"
                }
            };

            foreach (LiveCharts.Wpf.PieSeries ps in psc)
            {
                myPieChart.Series.Add(ps);
            }
        }

        public void DisplayStudent()
        {
            var con = Configuration.getInstance().getConnection();

            // sql querey that retrieve data for students by joining lookup , student and person tabel

            //SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy') this format is used to only retrieve date no time

            SqlCommand cmd = new SqlCommand("SELECT CONCAT(P.FirstName ,' ',P.LastName) AS Name,S.Id ,S.RegistrationNo AS RegNo ,L.Value AS Status,(CASE WHEN GS.Status =@Status THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS ActiveDate,(CASE WHEN GS.Status<>@Status THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS InActiveDate FROM GroupStudent AS GS JOIN Lookup AS L ON GS.Status = L.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS P ON P.Id = S.Id WHERE GS.GroupId = @GroupId ORDER BY L.Value", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            cmd.Parameters.AddWithValue("@Status", 3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GroupDataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
