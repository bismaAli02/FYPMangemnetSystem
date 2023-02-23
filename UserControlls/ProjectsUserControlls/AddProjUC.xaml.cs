﻿using CRUD_Operations;
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

namespace FYPManagementSystem.UserControlls.ProjectsUserControlls
{
    /// <summary>
    /// Interaction logic for AddProjUC.xaml
    /// </summary>
    public partial class AddProjUC : UserControl
    {
        int id;
        public AddProjUC()
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Save";
        }
        public AddProjUC(string description, string title, int id)
        {
            InitializeComponent();
            SaveButtonTxt.Text = "Update";
            DescriptionTextBox.Text = description;
            TitleTextBox.Text = title;
            this.id = id;
        }

        private void EmptyForm()
        {
            DescriptionTextBox.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
        }

        private void LockForm()
        {
            DescriptionTextBox.IsReadOnly = true;
            TitleTextBox.IsReadOnly = true;
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            EmptyForm();
        }

        private void SaveRecord()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Project(Description,Title) VALUES(@Description,@Title)", con);
                cmd.Parameters.AddWithValue("@Description", DescriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@Title", TitleTextBox.Text);
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
            if (DescriptionTextBox.Text != string.Empty && TitleTextBox.Text != string.Empty)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Project SET Description =@Description, Title=@Title WHERE Id=@Id;", con);
                    cmd.Parameters.AddWithValue("@Description", DescriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Title", TitleTextBox.Text);
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
    }
}
