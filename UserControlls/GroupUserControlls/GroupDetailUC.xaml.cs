using CRUD_Operations;
using FYPManagementSystem.UserControlls.ProjectsUserControlls;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
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
    }
}
