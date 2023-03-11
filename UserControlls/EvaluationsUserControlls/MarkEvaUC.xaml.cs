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

namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls
{
    /// <summary>
    /// Interaction logic for MarkEvaUC.xaml
    /// </summary>
    public partial class MarkEvaUC : UserControl
    {
        int evaID;

        public MarkEvaUC()
        {
            InitializeComponent();
            GroupIdToComboBox();
            EvaNameToComboBox();
        }

        // this parametrized constructer made for update operation purpose
        public MarkEvaUC(string groupID, string evaName, int obtainMarks, int evaID)
        {
            InitializeComponent();
            this.evaID = evaID;
            GroupIdToComboBox();
            GIdComboBox.Text = groupID;
            EvaNameToComboBox();
            SaveButtonTxt.Text = "Update";
            EvaComboBox.Text = evaName.ToString();
            OMTextBox.Text = obtainMarks.ToString();
            GIdComboBox.IsReadOnly = true;
            EvaComboBox.IsReadOnly = true;
            TMTextBox.IsReadOnly = true;
            GIdComboBox.IsEnabled = false;
            EvaComboBox.IsEnabled = false;
            GiveEvaluationId();
        }


        //this code clears  all the controls
        private void EmptyForm()
        {
            if (GIdComboBox.IsEnabled == true)
            {
                GIdComboBox.Text = string.Empty;
                EvaComboBox.Text = string.Empty;
                TMTextBox.Text = string.Empty;
            }
            OMTextBox.Text = string.Empty;
        }

        private void GroupIdToComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT G.Id FROM [Group] AS G LEFT JOIN GroupProject AS GP ON G.Id=GP.GroupId LEFT JOIN GroupStudent AS GS ON GS.GroupId=G.Id WHERE GS.Status=3", con);
                //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                //this line of code creates a new DataSet object, which can be used to work with data in a tabular format.
                DataSet dataSet = new DataSet();
                //it retrieves data from a data source and fills a DataSet object with the retrieved data.
                dataAdapter.Fill(dataSet);
                // this code help to display data in comboBox
                GIdComboBox.ItemsSource = dataSet.Tables[0].DefaultView;

                //GIdComboBox control will display the data from the "Id" column of the data source in its dropdown list.
                GIdComboBox.DisplayMemberPath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EvaNameToComboBox()
        {
            if (GIdComboBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Evaluation E EXCEPT SELECT E.Name FROM Evaluation E JOIN GroupEvaluation GE ON GE.EvaluationId = E.Id JOIN [Group] G ON G.Id = GE.GroupId WHERE G.Id = @ID AND GE.EvaluationId <>" + evaID, con);
                    cmd.Parameters.AddWithValue("@ID", int.Parse(GIdComboBox.Text));
                    //creates a new instance of the SqlDataAdapter class and assigns it to the variable dataAdapter
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    //this line of code creates a new DataSet object, which can be used to work with data in a tabular format.
                    DataSet dataSet = new DataSet();
                    //it retrieves data from a data source and fills a DataSet object with the retrieved data.
                    dataAdapter.Fill(dataSet);
                    // this code help to display data in comboBox
                    EvaComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
                    //EvaComboBox control will display the data from the "Name" column of the data source in its dropdown list.
                    EvaComboBox.DisplayMemberPath = "Name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // give evaluatio id from database
        private void GiveEvaluationId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id,TotalMarks FROM Evaluation WHERE Name=@Name", con);
            cmd.Parameters.AddWithValue("@Name", EvaComboBox.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                evaID = int.Parse(reader["Id"].ToString());
                TMTextBox.Text = reader["TotalMarks"].ToString();
                TMTextBox.IsReadOnly = true;
            }
            reader.Close();
        }

        // save the record in dataBase
        private void SaveRecord()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO GroupEvaluation VALUES(@GroupId,@EvaluationId,@ObtainedMarks,@EvaluationDate)", con);
                cmd.Parameters.AddWithValue("@ObtainedMarks", int.Parse(OMTextBox.Text));
                cmd.Parameters.AddWithValue("@GroupId", GIdComboBox.Text);
                cmd.Parameters.AddWithValue("@EvaluationId", evaID);
                cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                EmptyForm();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // update the record in database
        private void UpdateRecord()
        {
            if (GIdComboBox.Text != string.Empty && OMTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE GroupEvaluation SET ObtainedMarks=@ObtainedMarks,EvaluationDate=@EvaluationDate WHERE GroupId=@GroupId AND EvaluationId=@EvaluationId;", con);
                    cmd.Parameters.AddWithValue("@ObtainedMarks", int.Parse(OMTextBox.Text));
                    cmd.Parameters.AddWithValue("@GroupId", GIdComboBox.Text);
                    cmd.Parameters.AddWithValue("@EvaluationId", evaID);
                    cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select any record to Update");
            }
        }



        // save and update function used in this method
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // validations
            if (GIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("Please Select a Group", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EvaComboBox.Text == string.Empty)
            {
                MessageBox.Show("Please Select Evaluation Name", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (OMTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Obtained Marks", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (int.Parse(OMTextBox.Text) > int.Parse(TMTextBox.Text))
            {
                MessageBox.Show("Obtained Markes cannot be more than Total Marks", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (SaveButtonTxt.Text == "Save")
                {
                    SaveRecord();
                }
                else
                {
                    UpdateRecord();
                }
                this.Visibility = Visibility.Collapsed;
                findParentUserControl();

            }
        }


        //this code find the parent EvaProjUC control. It sets the text of a Button to "Evaluate Group".function is used to call a method of EvaProjUC (displayEvaluateGroup)
        private void findParentUserControl()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is EvaProjectUC))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is EvaProjectUC)
            {
                EvaProjectUC par = parent as EvaProjectUC;
                par.DisplayEvaluateGroup();
                Button btn = (Button)par.FindName("MarkEvaButton");
                btn.Content = "Evaluate Group";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        private void EvaComboBox_DropDownClosed(object sender, EventArgs e)
        {
            GiveEvaluationId();
        }

        //Updates the Combobox that shows the evaluation names according to the selected group only those evaluations are shown which that particular group has not been evaluated
        private void GIdComboBox_DropDownClosed(object sender, EventArgs e)
        {
            EvaNameToComboBox();
        }
    }
}
