using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace IntersectionInspector.src
{
    class SimPointDisplay
    {
        public SimPointDisplay(Point Origo, Canvas Background)
        {
            this.origo = Origo;
            this.background = Background;
            orange = new SolidColorBrush(Colors.Orange);
            Init();
        }

        private Point origo;
        private Ellipse display;
        private Label pointLabel;
        private Canvas background;

        private const double RADIUS = 3;
        private static SolidColorBrush orange;

        private void Init()
        {
            display = new Ellipse();
            display.Stroke = orange;
            display.Width = RADIUS*2;
            display.Height = RADIUS*2;
            display.Fill = orange;

            pointLabel = new Label();
            pointLabel.FontSize = 8;
            pointLabel.Foreground = orange;

            background.Children.Add(display);
            Canvas.SetLeft(display, origo.X - RADIUS);
            Canvas.SetTop(display, origo.Y - RADIUS);
            Canvas.SetZIndex(display, 10);

            background.Children.Add(pointLabel);
        }

        public void Move(Point NewOrigo)
        {
            origo = NewOrigo;
            pointLabel.Content = String.Format("{0};{1}", Math.Round(origo.X, 2), Math.Round(origo.Y, 2));
            Canvas.SetLeft(display, origo.X - RADIUS);
            Canvas.SetTop(display, origo.Y - RADIUS);
            Canvas.SetLeft(pointLabel, origo.X - RADIUS);
            Canvas.SetTop(pointLabel, origo.Y - RADIUS);
        }
    }
}
