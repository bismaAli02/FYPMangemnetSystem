using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace FYPManagementSystem
{
    public partial class MainWindow : Window
    {
        private void CollapseAllGrids()
        {
            StudentGrid.Visibility = Visibility.Collapsed;
            AdvisorGrid.Visibility = Visibility.Collapsed;
            stUC.Visibility = Visibility.Collapsed;
            //  AddStuUC.Visibility = Visibility.Collapsed;
            AddAdvUC.Visibility = Visibility.Collapsed;
            advUC.Visibility = Visibility.Collapsed;
        }
        public MainWindow()
        {
            InitializeComponent();
            CollapseAllGrids();
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
            CollapseAllGrids();
            StudentGrid.Visibility = Visibility.Visible;
            stUC.Visibility = Visibility.Visible;
        }

        private void GStudentButton_Click(object sender, RoutedEventArgs e)
        {
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
            CollapseAllGrids();
            AdvisorGrid.Visibility = Visibility.Visible;
            AddAdvUC.Visibility = Visibility.Visible;
        }

        private void GAdvisorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PdfRepButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

