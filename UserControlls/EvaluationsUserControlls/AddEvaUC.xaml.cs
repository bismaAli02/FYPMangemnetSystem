using CRUD_Operations;
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

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    /// <summary>
    /// Interaction logic for AddEvaUC.xaml
    /// </summary>
    public partial class AddEvaUC : UserControl
    {
        int id;
        public AddEvaUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
        }

        public AddEvaUC(string name, int totalMarks, int totalWeightage, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
            NameTextBox.Text = name;
            TMTextBox.Text = totalMarks.ToString();
            TWTextBox.Text = totalWeightage.ToString();
            this.id = id;
        }

        private void EmptyForm()
        {
            NameTextBox.Text = string.Empty;
            TMTextBox.Text = string.Empty;
            TWTextBox.Text = string.Empty;
        }

        private void LockForm()
        {
            NameTextBox.IsReadOnly = true;
            TMTextBox.IsReadOnly = true;
            TWTextBox.IsReadOnly = true;
        }


        private void SaveRecord()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Evaluation(Name,TotalMarks,TotalWeightage) VALUES(@Name,@TotalMarks,@TotalWeightage)", con);
                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", TMTextBox.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", TWTextBox.Text);
                EmptyForm();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateRecord()
        {
            if (NameTextBox.Text != string.Empty && TMTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name =@Name, TotalMarks=@TotalMarks, TotalWeightage=@TotalWeightage WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                    cmd.Parameters.AddWithValue("@TotalMarks", TMTextBox.Text);
                    cmd.Parameters.AddWithValue("@TotalWeightage", TWTextBox.Text);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                EmptyForm();
                LockForm();
            }
            else
            {
                MessageBox.Show("Please Select any record to Update");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveButtonTxt.Text == "Save")
            {
                SaveRecord();
            }
            else
            {
                UpdateRecord();
            }

        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }
    }
}
