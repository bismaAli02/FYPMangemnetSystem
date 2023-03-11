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

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    public partial class EvaProjectUC : UserControl
    {
        public EvaProjectUC()
        {
            InitializeComponent();
            DisplayEvaluateGroup();
        }
        // display all evaluated groups
        public void DisplayEvaluateGroup()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM GroupEvaluation AS GE,Evaluation AS E WHERE GE.EvaluationId=E.Id ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            EvaDataGrid.ItemsSource = dt.DefaultView;
            if (MarkEvaUC.Visibility == Visibility.Collapsed)
            {
                MarkEvaButton.Content = "Evaluate Group";
            }
            else
            {
                MarkEvaButton.Content = "Go Back";
            }
        }

        // use a single button for two purpose if Add "MarkEvaUC" visibility is visible that means we can open "add evaluation" UserControl or if not it means Usercontrol is already opened now we use this button for go back purpose
        private void MarkEvaButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarkEvaButton.Content.ToString() == "Evaluate Group")
            {
                MarkEvaUC.Content = new MarkEvaUC();
                MarkEvaUC.Visibility = Visibility.Visible;
                MarkEvaButton.Content = "Go Back";
            }
            else
            {
                MarkEvaUC.Visibility = Visibility.Collapsed;
                MarkEvaButton.Content = "Add Evaluation";

            }
            DisplayEvaluateGroup();

        }

        // edit evaluated group
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            string groupId, evaName;
            int evaId, obtainedMarks;
            DataRowView selectedRow = EvaDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                groupId = selectedRow["GroupId"].ToString();
                evaName = selectedRow["Name"].ToString();
                evaId = Int32.Parse(selectedRow["EvaluationId"].ToString());
                obtainedMarks = Int32.Parse(selectedRow["ObtainedMarks"].ToString());
                MarkEvaUC.Content = new MarkEvaUC(groupId, evaName, obtainedMarks, evaId);
                MarkEvaUC.Visibility = Visibility.Visible;
                MarkEvaButton.Content = "Go Back";
            }
        }
    }
}
