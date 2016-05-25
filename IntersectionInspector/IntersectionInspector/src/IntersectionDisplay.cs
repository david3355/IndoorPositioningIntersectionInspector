using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IntersectionInspector.src
{
    class IntersectionDisplay
    {
        public IntersectionDisplay(Point Origo, Canvas Background)
        {
            this.origo = Origo;
            this.background = Background;
            red = new SolidColorBrush(Colors.Red);
            Init();
        }

        private Point origo;
        private Canvas background;
        private static SolidColorBrush red;
        private Ellipse pointDisplay;
        private Label pointLabel;
        private static double diameter = 5;


        private void Init()
        {
            pointDisplay = new Ellipse();
            pointDisplay.Stroke = red;
            pointDisplay.Width = diameter;
            pointDisplay.Height = diameter;
            pointDisplay.Fill = red;

            pointLabel = new Label();
            pointLabel.FontSize = 8;
            pointLabel.Content = String.Format("({0};{1})", Math.Round(origo.X, 1), Math.Round(origo.Y, 1));

            background.Children.Add(pointDisplay);
            Canvas.SetLeft(pointDisplay, origo.X - diameter / 2);
            Canvas.SetTop(pointDisplay, origo.Y - diameter / 2);

            background.Children.Add(pointLabel);
            Canvas.SetLeft(pointLabel, origo.X - diameter / 2);
            Canvas.SetTop(pointLabel, origo.Y - diameter / 2);
        }

        public void RemoveItselfFromView()
        {
            background.Children.Remove(pointDisplay);
            background.Children.Remove(pointLabel);
        }

    }
}
