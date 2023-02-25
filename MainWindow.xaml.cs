using System.Windows;
using System.Windows.Input;
using System.Threading;
using FYPManagementSystem.UserControlls.EvaluationsUserControlls;

namespace FYPManagementSystem
{
    public partial class MainWindow : Window
    {
        private void CollapseAllGrids()
        {
            StudentGrid.Visibility = Visibility.Collapsed;
            AdvisorGrid.Visibility = Visibility.Collapsed;
            ProjectGrid.Visibility = Visibility.Collapsed;
            EvaluationGrid.Visibility = Visibility.Collapsed;
            MarkEvaluationGrid.Visibility = Visibility.Collapsed;
            stUC.Visibility = Visibility.Collapsed;
            advUC.Visibility = Visibility.Collapsed;
            ProjUC.Visibility = Visibility.Collapsed;
            EvaUC.Visibility = Visibility.Collapsed;
            MarkEvaUC.Visibility = Visibility.Collapsed;

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
            stUC.Children.Clear();
            stUC.Children.Add(new StudentUC());
        }

        private void GStudentButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            ProjectGrid.Visibility = Visibility.Visible;
            ProjUC.Visibility = Visibility.Visible;
        }

        private void GProjectsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            EvaluationGrid.Visibility = Visibility.Visible;
            EvaUC.Visibility = Visibility.Visible;
        }

        private void GEvaluaionButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            MarkEvaluationGrid.Visibility = Visibility.Visible;
            MarkEvaUC.Visibility = Visibility.Visible;
        }

        private void AdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            AdvisorGrid.Visibility = Visibility.Visible;
            advUC.Visibility = Visibility.Visible;
        }

        private void GAdvisorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PdfRepButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

