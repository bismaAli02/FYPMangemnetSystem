using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace FYPManagementSystem
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker student = new BackgroundWorker();
            student.WorkerReportsProgress = true;
            student.DoWork += Student_DoWork;
            student.ProgressChanged += student_ProgressChanged;
            student.RunWorkerAsync();

        }

        private void student_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            loadinNumber.Text = "Loading ... " + e.ProgressPercentage;
            if (progressBar.Value == 100)
            {
                MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.Show();

            }
        }

        private void Student_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(80);
            }
        }
    }
}
