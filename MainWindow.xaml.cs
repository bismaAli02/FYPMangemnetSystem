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
using System.Windows.Media;
using System.Windows.Controls;

namespace FYPManagementSystem
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private bool IsMaximized = false;
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
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

        private void NormalizeAllButtons()
        {
            HomeScreenButton.Background = Brushes.Transparent;
            HomeScreenButton.Foreground = Brushes.Black;

            StudentButton.Background = Brushes.Transparent;
            StudentButton.Foreground = Brushes.Black;

            GStudentButton.Background = Brushes.Transparent;
            GStudentButton.Foreground = Brushes.Black;

            ProjectButton.Background = Brushes.Transparent;
            ProjectButton.Foreground = Brushes.Black;

            AssignAdvButton.Background = Brushes.Transparent;
            AssignAdvButton.Foreground = Brushes.Black;

            EvaluationButton.Background = Brushes.Transparent;
            EvaluationButton.Foreground = Brushes.Black;

            GEvaluaionButton.Background = Brushes.Transparent;
            GEvaluaionButton.Foreground = Brushes.Black;

            AdvisorButton.Background = Brushes.Transparent;
            AdvisorButton.Foreground = Brushes.Black;

            GeneratePdfButton.Background = Brushes.Transparent;
            GeneratePdfButton.Foreground = Brushes.Black;
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
            HighlightButton(sender);
            CollapseAllGrids();
            StudentGrid.Visibility = Visibility.Visible;
            stUC.Visibility = Visibility.Visible;
            stUC.Children.Clear();
            stUC.Children.Add(new StudentUC());
        }

        private void GStudentButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            GroupUCGrid.Visibility = Visibility.Visible;
            GroupStuUC.Visibility = Visibility.Visible;
            GroupStuUC.Children.Clear();
            GroupStuUC.Children.Add(new GroupStuUC());
        }

        // colapsed all other grids and diplay only project UserControl
        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            ProjectGrid.Visibility = Visibility.Visible;
            ProjUC.Visibility = Visibility.Visible;
            ProjUC.Children.Clear();
            ProjUC.Children.Add(new ProjUC());
        }

        // colapsed all other grids and diplay only Evaluation UserControl
        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            EvaluationGrid.Visibility = Visibility.Visible;
            EvaUC.Visibility = Visibility.Visible;
            EvaUC.Children.Clear();
            EvaUC.Children.Add(new EvaUC());
        }

        // colapsed all other grids and diplay only Mark evaluation UserControl
        private void GEvaluaionButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            MarkEvaluationGrid.Visibility = Visibility.Visible;
            MarkEvaUC.Visibility = Visibility.Visible;
            MarkEvaUC.Children.Clear();
            MarkEvaUC.Children.Add(new EvaProjectUC());
        }

        // colapsed all other grids and diplay only advisor UserControl
        private void AdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            AdvisorGrid.Visibility = Visibility.Visible;
            advUC.Visibility = Visibility.Visible;
            advUC.Children.Clear();
            advUC.Children.Add(new AdvUC());
        }
        private void AssignAdvButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
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
            HighlightButton(sender);
            CollapseAllGrids();
        }

        private void GeneratePdfButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton(sender);
            CollapseAllGrids();
            reportUCGrid.Visibility = Visibility.Visible;
            reportUC.Visibility = Visibility.Visible;
        }

        private void HighlightButton(object sender)
        {
            NormalizeAllButtons();
            Button btn = sender as Button;
            btn.Background = Brushes.BlueViolet;
            btn.Foreground = Brushes.White;
        }
    }
}

