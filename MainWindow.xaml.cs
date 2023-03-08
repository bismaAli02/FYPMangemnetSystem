using System.Windows;
using System.Windows.Input;
using System.Threading;
using FYPManagementSystem.UserControlls.EvaluationsUserControlls;
using FYPManagementSystem.UserControlls.GroupUserControlls;
using FYPManagementSystem.UserControlls.ProjectsUserControlls;
using FYPManagementSystem.UserControlls.AdvisorsUsercontrolls;
using System.Timers;
using System.Windows.Threading;
using System;

namespace FYPManagementSystem
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            CollapseAllGrids(); // initially when constructor call All grids hide
                                // Create a new DispatcherTimer
            timer = new DispatcherTimer();

            // Set the interval to 2 seconds
            timer.Interval = TimeSpan.FromSeconds(2);

            // Handle the Tick event
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();
            slider.Visibility = Visibility.Collapsed;
        }

        // this function is used for all grids or user controls to hide except one grid whose button is clicked
        private void CollapseAllGrids()
        {
            StudentGrid.Visibility = Visibility.Collapsed;
            AdvisorGrid.Visibility = Visibility.Collapsed;
            ProjectGrid.Visibility = Visibility.Collapsed;
            EvaluationGrid.Visibility = Visibility.Collapsed;
            AssignAdvUCGrid.Visibility = Visibility.Collapsed;
            GroupUCGrid.Visibility = Visibility.Collapsed;
            MarkEvaluationGrid.Visibility = Visibility.Collapsed;
            reportUCGrid.Visibility = Visibility.Collapsed;
            stUC.Visibility = Visibility.Collapsed;
            advUC.Visibility = Visibility.Collapsed;
            ProjUC.Visibility = Visibility.Collapsed;
            EvaUC.Visibility = Visibility.Collapsed;
            MarkEvaUC.Visibility = Visibility.Collapsed;
            GroupStuUC.Visibility = Visibility.Collapsed;
            AssignAdvUC.Visibility = Visibility.Collapsed;
            reportUC.Visibility = Visibility.Collapsed;
        }

        //this function is used for design Purpose when mouse drag to any button that button will highlight
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
            CollapseAllGrids();
            GroupUCGrid.Visibility = Visibility.Visible;
            GroupStuUC.Visibility = Visibility.Visible;
            GroupStuUC.Children.Clear();
            GroupStuUC.Children.Add(new GroupStuUC());
        }

        // colapsed all other grids and diplay only project UserControl
        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            ProjectGrid.Visibility = Visibility.Visible;
            ProjUC.Visibility = Visibility.Visible;
            ProjUC.Children.Clear();
            ProjUC.Children.Add(new ProjUC());
        }

        // colapsed all other grids and diplay only Evaluation UserControl
        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            EvaluationGrid.Visibility = Visibility.Visible;
            EvaUC.Visibility = Visibility.Visible;
            EvaUC.Children.Clear();
            EvaUC.Children.Add(new EvaUC());
        }

        // colapsed all other grids and diplay only Mark evaluation UserControl
        private void GEvaluaionButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            MarkEvaluationGrid.Visibility = Visibility.Visible;
            MarkEvaUC.Visibility = Visibility.Visible;
            MarkEvaUC.Children.Clear();
            MarkEvaUC.Children.Add(new EvaProjectUC());
        }

        // colapsed all other grids and diplay only advisor UserControl
        private void AdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            AdvisorGrid.Visibility = Visibility.Visible;
            advUC.Visibility = Visibility.Visible;
            advUC.Children.Clear();
            advUC.Children.Add(new AdvUC());
        }
        private void AssignAdvButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            AssignAdvUCGrid.Visibility = Visibility.Visible;
            AssignAdvUC.Visibility = Visibility.Visible;
            AssignAdvUC.Children.Clear();
            AssignAdvUC.Children.Add(new AssignAdvUC());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider.Value == 0)
            {
                image1.Visibility = Visibility.Visible;
                image2.Visibility = Visibility.Collapsed;
                image3.Visibility = Visibility.Collapsed;
            }
            else if (slider.Value == 1)
            {
                image1.Visibility = Visibility.Collapsed;
                image2.Visibility = Visibility.Visible;
                image3.Visibility = Visibility.Collapsed;
            }
            else if (slider.Value == 2)
            {
                image1.Visibility = Visibility.Collapsed;
                image2.Visibility = Visibility.Collapsed;
                image3.Visibility = Visibility.Visible;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment the Slider value by 1
            slider.Value += 1;
            if (slider.Value == 3)
            {
                slider.Value = 0;
            }

        }


        private void HomeScreenButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
        }

        private void GeneratePdfButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();
            reportUCGrid.Visibility = Visibility.Visible;
            reportUC.Visibility = Visibility.Visible;
        }
    }
}

