﻿using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Terrorarium.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas SimulationCanvas { get; set; }

        private TextBlock SimulationStatsBox { get; set; }

        private Simulation Simulation { get; set; }

        private DateTime StartTime { get; }

        private static Stopwatch UpdateStopwatch = Stopwatch.StartNew();

        private readonly SolidColorBrush ButtonDefaultColor;
        private readonly SolidColorBrush ButtonOnColor = Brushes.Green;

        /*
        public MainWindow()
        {
            InitializeComponent();

            SimulationCanvas = new Canvas();
            SimulationCanvas.Background = Brushes.LightSteelBlue;
            ScaleTransform scaleTransform = new ScaleTransform(1, -1, .5, .5);
            SimulationCanvas.LayoutTransform = scaleTransform;

            SimulationStatsBox = new TextBlock();

            Grid mainGrid = new Grid();
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2.0, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8.0, GridUnitType.Star) });
           
            Grid.SetColumn(SimulationCanvas, 1);
            Grid.SetColumn(SimulationStatsBox, 0);

            mainGrid.Children.Add(SimulationCanvas);
            mainGrid.Children.Add(SimulationStatsBox);

            this.Content = mainGrid;
            this.Simulation = Simulator.New(ConfigPresets.UpgradeConfig);
            CompositionTarget.Rendering += OnRender;
            StartTime = DateTime.Now;
        }
        */

        public MainWindow()
        {
            InitializeComponent();

            SetupButtons();
            ButtonDefaultColor = LoadWeightsButton.Background as SolidColorBrush;
        }

        public void SetupButtons()
        {
            this.LoadWeightsButton.Click += LoadWeightsOnClick;
            this.RandomWeightsButton.Click += RandomWeightsOnClick;
            this.StartSimulationButton.Click += StartButtonOnClick;

            
        }

        public void LoadWeightsOnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Json files (*.json)|*.json";
            bool? result = dialog.ShowDialog();
            
            if (result == true)
            {
                LoadWeightsButton.Background = ButtonOnColor;
                StartSimulationButton.Background = ButtonOnColor;
            }
            else
            {
                ResetButtons();
            }
        }

        public void RandomWeightsOnClick(object sender, RoutedEventArgs e)
        {
            LoadWeightsButton.Background = ButtonDefaultColor;
            RandomWeightsButton.Background = ButtonOnColor;
            StartSimulationButton.Background = ButtonOnColor;
        }

        public void StartButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (ButtonDefaultColor != LoadWeightsButton.Background)
            {

            }
        }

        public void ResetButtons()
        {
            LoadWeightsButton.Background = ButtonDefaultColor;
            RandomWeightsButton.Background = ButtonDefaultColor;
            StartSimulationButton.Background = ButtonDefaultColor;
        }

        /*
        public void OnRender(object sender, EventArgs e)
        {
            if (UpdateStopwatch.Elapsed.Milliseconds > 5)
            {
                this.Simulation = Simulator.Step(this.Simulation);
                DrawSimulation(this.Simulation);
                WriteSimulationResults(this.Simulation);
                UpdateStopwatch = Stopwatch.StartNew();
            }
        }
        */

        public void DrawSimulation(Simulation sim)
        {
            SimulationCanvas.Children.Clear();

            foreach (var animal in sim.World.Animals)
            {
                var canvasPoint = new Point(
                    animal.Position.X * SimulationCanvas.ActualWidth,
                    animal.Position.Y * SimulationCanvas.ActualHeight);
                DrawTriangle(canvasPoint, .01 * SimulationCanvas.ActualWidth, animal.Rotation);
            }
            foreach (var food in sim.World.Foods)
            {
                var canvasPoint = new Point(
                    food.Position.X * SimulationCanvas.ActualWidth,
                    food.Position.Y * SimulationCanvas.ActualHeight);
                DrawCircle(canvasPoint, (0.01 / 2.0) * SimulationCanvas.ActualWidth);
            }

            this.InvalidateVisual();
        }

        public void DrawCircle(Point center, double radius)
        {
            Ellipse ellipse = new Ellipse()
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = Brushes.Black
            };

            Canvas.SetLeft(ellipse, center.X - radius);
            Canvas.SetTop(ellipse, center.Y - radius);

            SimulationCanvas.Children.Add(ellipse);
            ellipse.InvalidateVisual();
        }

        public void DrawTriangle(Point center, double size, Vector2D rotation)
        {
            Polygon triangle = new Polygon();
            var x = center.X;
            var y = center.Y;

            var rotationRads = 
                (Angle.FromDegrees(-90).Radians) +
                Vector2D.FromPolar(1.0, Angle.FromDegrees(0)).SignedAngleTo(rotation, false, true).Radians;

            List<Point> pointlist = new List<Point>
            {
                new Point(
                    x - Math.Sin(rotationRads) * size * 1.5,
                    y + Math.Cos(rotationRads) * size * 1.5),
                new Point(
                    x - Math.Sin(rotationRads + 2.0 / 3.0 * Math.PI) * size,
                    y + Math.Cos(rotationRads + 2.0 / 3.0 * Math.PI) * size),
                new Point(
                    x - Math.Sin(rotationRads + 4.0 / 3.0 * Math.PI) * size,
                    y + Math.Cos(rotationRads + 4.0 / 3.0 * Math.PI) * size),
                new Point(
                    x - Math.Sin(rotationRads) * size * 1.5,
                    y + Math.Cos(rotationRads) * size * 1.5)
            };
            triangle.Points = new PointCollection(pointlist);
            triangle.Fill = Brushes.Black;

            SimulationCanvas.Children.Add(triangle);
            triangle.InvalidateVisual();
        }

        public void WriteSimulationResults(Simulation sim)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Started: {StartTime.ToString("h:mm:ss tt")}");
            builder.AppendLine();
            builder.AppendLine("Current Simulation Age");
            builder.AppendLine($"{sim.Age} steps out of {sim.Config.SimGenerationLength}");
            builder.AppendLine();
            foreach (var stats in sim.Statistics)
            {
                var gaStats = stats.GAStatistics;
                builder.AppendLine($"Generation: {stats.Generation}");
                builder.AppendLine($"{nameof(gaStats.AvgFitness)}: {gaStats.AvgFitness}");
                builder.AppendLine($"{nameof(gaStats.MaxFitness)}: {gaStats.MaxFitness}");
                builder.AppendLine($"{nameof(gaStats.MinFitness)}: {gaStats.MinFitness}");
                builder.AppendLine($"{nameof(gaStats.MedianFitness)}: {gaStats.MedianFitness}");
                builder.AppendLine();
            }

            SimulationStatsBox.Text = builder.ToString();
        }
    }
}
