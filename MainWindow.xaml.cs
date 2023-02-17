using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace FYPManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void StudentButton_Click(object sender, RoutedEventArgs e)
        {
            //stUC.Visibility = Visibility.Collapsed;
            //AddStuUC.Visibility = Visibility.Visible;
            StudentGrid.Visibility = Visibility.Visible;
        }

        private void GStudentButton_Click(object sender, RoutedEventArgs e)
        {
            //stUC.Visibility = Visibility.Visible;
            //AddStuUC.Visibility = Visibility.Collapsed;
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GProjectsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GEvaluaionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdvisorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GAdvisorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PdfRepButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


