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

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    /// <summary>
    /// Interaction logic for EvaUC.xaml
    /// </summary>
    public partial class EvaUC : UserControl
    {
        public EvaUC()
        {
            InitializeComponent();
            DisplayEvaluation();
        }

        public void DisplayEvaluation()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id, Name, TotalMarks, TotalWeightage FROM Evaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            EvaDataGrid.ItemsSource = dt.DefaultView;
            if (AddEvaUC.Visibility == Visibility.Collapsed)
            {
                AddEvaButton.Content = "Add Evaluation";
            }
            else
            {
                AddEvaButton.Content = "Go Back";
            }
        }

        private void deleteTuple(int id)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Evaluation WHERE Id =@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void AddEvaButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddEvaButton.Content.ToString() == "Add Evaluation")
            {
                AddEvaUC.Content = new AddEvaUC();
                AddEvaUC.Visibility = Visibility.Visible;
                AddEvaButton.Content = "Go Back";
            }
            else
            {
                AddEvaUC.Visibility = Visibility.Collapsed;
                AddEvaButton.Content = "Add Evaluation";

            }
            DisplayEvaluation();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string name;
            int totalMarks, totalWeightage;
            DataRowView selectedRow = EvaDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = Int32.Parse(selectedRow["Id"].ToString());
                name = selectedRow["Name"].ToString();
                totalMarks = int.Parse(selectedRow["TotalMarks"].ToString());
                totalWeightage = int.Parse(selectedRow["TotalWeightage"].ToString());
                AddEvaUC.Content = new AddEvaUC(name, totalMarks, totalWeightage, id);
                AddEvaUC.Visibility = Visibility.Visible;
                AddEvaButton.Content = "Go Back";
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = EvaDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int id = int.Parse(selectedRow["Id"].ToString());
                deleteTuple(id);
                AddEvaUC.Visibility = Visibility.Collapsed;
                DisplayEvaluation();

            }
            else
            {
                MessageBox.Show("Please Select a specific row to Delete!!!");
            }

        }
    }
}
