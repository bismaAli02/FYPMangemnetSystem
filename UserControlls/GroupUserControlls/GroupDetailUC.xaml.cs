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
        }

        private void DisplayPieChart()
        {
            List<int> studentCount = new List<int>();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GroupStudent WHERE GroupId=@GroupId AND Status=3 ; SELECT COUNT(*) FROM GroupStudent WHERE GroupId=@GroupId AND Status=4", con);
            cmd.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                studentCount.Add(int.Parse(reader["Id"].ToString()));
            }
            studentCount.Add(studentCount[0]);
            reader.Close();
            // Create a new chart series
            var series = new PieSeries();
            // Set the data source for the series
            var data = new List<KeyValuePair<string, int>>();
            data.Add(new KeyValuePair<string, int>("Slice 1", 10));
            data.Add(new KeyValuePair<string, int>("Slice 2", 20));
            data.Add(new KeyValuePair<string, int>("Slice 3", 30));
            series.DataContext = data;

            // Set the value and category bindings for the series
            series.ValueBinding = new PropertyNameDataPointBinding("Value");
            series.CategoryBinding = new PropertyNameDataPointBinding("Label");

            // Add the series to the pie chart
            pieChart.Series.Add(series);
        }
    }
}
