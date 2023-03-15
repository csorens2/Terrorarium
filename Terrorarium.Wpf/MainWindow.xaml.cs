using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Canvas WindowCanvas { get; set; }

        private Simulation Simulation { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Canvas myCanvas = new Canvas();
            myCanvas.Background = Brushes.LightSteelBlue;

            ScaleTransform scaleTransform = new ScaleTransform(1, -1, .5, .5);
            myCanvas.LayoutTransform = scaleTransform;

            WindowCanvas = myCanvas;
            this.Content = myCanvas;

            Simulation = new Simulation();


            CompositionTarget.Rendering += OnRender;
        }

        private static Stopwatch UpdateStopwatch = Stopwatch.StartNew();
        private static bool FirstDraw = false;

        public void OnRender(object sender, EventArgs e)
        {
            if (UpdateStopwatch.Elapsed.Milliseconds > 5)
            {
                var nextSim = this.Simulation.Step();
                this.Simulation = nextSim.Item1;
                DrawSimulation(this.Simulation);
                UpdateStopwatch = Stopwatch.StartNew();
            }
        }

        public void DrawSimulation(Simulation sim)
        {
            WindowCanvas.Children.Clear();

            foreach (var animal in sim.World.Animals)
            {
                var canvasPoint = new Point(
                    animal.Position.X * WindowCanvas.ActualWidth,
                    animal.Position.Y * WindowCanvas.ActualHeight);
                DrawTriangle(canvasPoint, .01 * WindowCanvas.ActualWidth, animal.Rotation);
            }
            foreach (var food in sim.World.Foods)
            {
                var canvasPoint = new Point(
                    food.Position.X * WindowCanvas.ActualWidth,
                    food.Position.Y * WindowCanvas.ActualHeight);
                DrawCircle(canvasPoint, (0.01 / 2.0) * WindowCanvas.ActualWidth);
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

            WindowCanvas.Children.Add(ellipse);
            ellipse.InvalidateVisual();
        }

        public void DrawTriangle(Point center, double size, Vector2D rotation)
        {
            Polygon triangle = new Polygon();
            var x = center.X;
            var y = center.Y;

            var rotationRads = Vector2D.FromPolar(1.0, Angle.FromDegrees(0)).SignedAngleTo(rotation, false, true).Radians;

            var baseRotation = Angle.FromDegrees(-90).Radians;
            List<Point> pointlist = new List<Point>
            {
                new Point(
                    x - Math.Sin(baseRotation + rotationRads) * size * 1.5,
                    y + Math.Cos(baseRotation + rotationRads) * size * 1.5),
                new Point(
                    x - Math.Sin(baseRotation + rotationRads + 2.0 / 3.0 * Math.PI) * size,
                    y + Math.Cos(baseRotation + rotationRads + 2.0 / 3.0 * Math.PI) * size),
                new Point(
                    x - Math.Sin(baseRotation + rotationRads + 4.0 / 3.0 * Math.PI) * size,
                    y + Math.Cos(baseRotation + rotationRads + 4.0 / 3.0 * Math.PI) * size),
                new Point(
                    x - Math.Sin(baseRotation + rotationRads) * size * 1.5,
                    y + Math.Cos(baseRotation + rotationRads) * size * 1.5)
            };
            triangle.Points = new PointCollection(pointlist);
            triangle.Fill = Brushes.Black;

            WindowCanvas.Children.Add(triangle);
            triangle.InvalidateVisual();
        }
    }
}
