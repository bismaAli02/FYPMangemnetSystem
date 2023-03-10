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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    public partial class EvaUC : UserControl
    {
        public EvaUC()
        {
            InitializeComponent();
            DisplayEvaluation();
        }

        //Calculte the total weightage of evaluations 
        private bool WeightageSumCalculate()
        {
            int totalWeightage = 0;
            foreach (System.Data.DataRowView dataRow in EvaDataGrid.ItemsSource)
            {
                totalWeightage += int.Parse(dataRow[3].ToString());
            }
            if (totalWeightage >= 100)
            {
                return false;
            }
            return true;
        }

        // display evaluations
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

        // Add evaluation
        private void AddEvaButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddEvaButton.Content.ToString() == "Add Evaluation")
            {
                if (EvaDataGrid.Items.Count != 4 && WeightageSumCalculate()) //check validation you cannot add more than 4 eva
                {
                    AddEvaUC.Content = new AddEvaUC();
                    AddEvaUC.Visibility = Visibility.Visible;
                    AddEvaButton.Content = "Go Back";
                }
                else
                {
                    MessageBox.Show("You can't add more evaluation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                AddEvaUC.Visibility = Visibility.Collapsed;
                DisplayEvaluation();
                AddEvaButton.Content = "Add Evaluation";
            }

        }
        //update record
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
    }
}
