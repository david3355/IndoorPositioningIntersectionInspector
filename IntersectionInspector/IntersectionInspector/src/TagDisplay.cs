using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace IntersectionInspector.src
{
    class TagDisplay
    {
        public TagDisplay(Point Origo, Canvas Background)
        {
            this.origo = Origo;
            this.background = Background;
            blue = new SolidColorBrush(Colors.Blue);
            green = new SolidColorBrush(Colors.Green);
            purple = new SolidColorBrush(Colors.Purple);
            Init();
        }

        private Point origo;
        private double radius;
        private Ellipse tagDisplay;
        private Ellipse distanceDisplay;
        private Line distanceLine;
        private Label distanceLabel;
        private Canvas background;

        private static SolidColorBrush blue, green, purple;
        private static double tagDiameter = 10;
        private static double defaultDistance = 50;

        public Point Origo
        {
            get { return origo; }
        }

        public double Distance
        {
            get { return radius; }
        }

        private void Init()
        {
            tagDisplay = new Ellipse();
            tagDisplay.Stroke = blue;
            tagDisplay.Width = tagDiameter;
            tagDisplay.Height = tagDiameter;
            tagDisplay.Fill = blue;

            distanceDisplay = new Ellipse();
            distanceDisplay.Stroke = green;

            distanceLine = new Line();
            distanceLine.Stroke = purple;

            distanceLabel = new Label();
            distanceLabel.FontSize = 8;

            background.Children.Add(distanceLine);

            background.Children.Add(tagDisplay);
            Canvas.SetLeft(tagDisplay, origo.X - tagDiameter / 2);
            Canvas.SetTop(tagDisplay, origo.Y - tagDiameter / 2);

            background.Children.Add(distanceDisplay);
            ChangeDistance(defaultDistance);

            background.Children.Add(distanceLabel);
        }

        public bool Contains(Point P)
        {
            return origo.X - tagDiameter / 2 < P.X && P.X < origo.X + tagDiameter / 2 && origo.Y - tagDiameter / 2 < P.Y && P.Y < origo.Y + tagDiameter / 2;
        }

        public void ChangeDistance(double Distance)
        {
            if (Distance > 0)
            {
                radius = Distance;
                Canvas.SetLeft(distanceDisplay, origo.X - Distance);
                Canvas.SetTop(distanceDisplay, origo.Y - Distance);
                distanceDisplay.Width = Distance * 2;
                distanceDisplay.Height = Distance * 2;
                ChangeLine();
            }
        }

        public bool Includes(TagDisplay OtherTag)
        {
            if (this.radius > LocationCalculator.Distance(this.origo, OtherTag.origo) + OtherTag.Distance) return true;
            return false;

        }

        public void AddDistance(double Change)
        {
            ChangeDistance(radius + Change);
        }

        public void RemoveItselfFromView()
        {
            background.Children.Remove(tagDisplay);
            background.Children.Remove(distanceDisplay);
            background.Children.Remove(distanceLine);
            background.Children.Remove(distanceLabel);
        }

        public void Move(Point NewOrigo)
        {
            origo = NewOrigo;
            Canvas.SetLeft(distanceDisplay, origo.X - radius);
            Canvas.SetTop(distanceDisplay, origo.Y - radius);
            Canvas.SetLeft(tagDisplay, origo.X - tagDiameter / 2);
            Canvas.SetTop(tagDisplay, origo.Y - tagDiameter / 2);
            ChangeLine();
        }

        public void ChangeLine()
        {
            Point p1 = origo;
            distanceLine.X1 = p1.X;
            distanceLine.Y1 = p1.Y;

            double xplus = radius * Math.Sin(AngleToRad(135));
            double yplus = radius * Math.Cos(AngleToRad(135));

            Point p2 = new Point(p1.X + xplus, p1.Y + yplus);
            distanceLine.X2 = p2.X;
            distanceLine.Y2 = p2.Y;
            distanceLabel.Content = Math.Round(radius, 2);
            Canvas.SetLeft(distanceLabel, (p1.X + p2.X) / 2);
            Canvas.SetTop(distanceLabel, (p1.Y + p2.Y) / 2);
        }

        private double AngleToRad(double Angle)
        {
            return (Angle * Math.PI) / 180;
        }

        public override bool Equals(object obj)
        {
            TagDisplay tag = obj as TagDisplay;
            return this.origo.Equals(tag.origo) && this.radius == tag.radius;
        }

    }
}
