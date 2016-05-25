using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IntersectionInspector.src;
using IntersectionInspector.src.locationcalc;
using IntersectionInspector.src.commonpoint;
using IntersectionInspector.src.locationcalc.util;

namespace IntersectionInspector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private List<TagDisplay> tags = new List<TagDisplay>();
        private List<IntersectionDisplay> intersectionPoints = new List<IntersectionDisplay>();
        private TagDisplay selectedTag;
        private const double changeRate = 2;
        private CommonPointStrategy cps;
        private CalculatorStrategy calculator;
        private LocationResult result;
        private SimPointDisplay simPoint;

        private void Init()
        {
            cps = new ClosestPointsStrategy();
            calculator = new AverageClosestDistanceLocationCalculator(cps);
            result = new LocationResult(new Point(-1, -1), Precision.NoTag);
            simPoint = new SimPointDisplay(new Point(-1, -1), backgr);
        }

        private TagDisplay ClickedTag(Point ClickPos)
        {
            foreach (TagDisplay tag in tags)
            {
                if (tag.Contains(ClickPos)) return tag;
            }
            return null;
        }

        private void Background_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int sign = e.Delta > 0 ? 1 : -1;
            int multiplier;
            if(Keyboard.IsKeyDown(Key.LeftCtrl)) multiplier = 5;
            else multiplier = 1;
            if (selectedTag != null)
            {
                selectedTag.AddDistance(multiplier * changeRate * sign);
            }
            CalculateIntersections();
        }

        private void Background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousepos = e.GetPosition(backgr);
            selectedTag = ClickedTag(mousepos);
            if (selectedTag == null)
            {
                selectedTag = new TagDisplay(mousepos, backgr);
                tags.Add(selectedTag);
            }
            CalculateIntersections();
        }

        private void Background_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousepos = e.GetPosition(backgr);
            selectedTag = ClickedTag(mousepos);
            if (selectedTag != null)
            {
                tags.Remove(selectedTag);
                selectedTag.RemoveItselfFromView();
            }
            CalculateIntersections();
        }

        private void Background_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousepos = e.GetPosition(backgr);
            if (e.LeftButton == MouseButtonState.Pressed && selectedTag != null)
            {
                selectedTag.Move(mousepos);
                CalculateIntersections();
            }
        }

        private void CalculateIntersections()
        {
            foreach (IntersectionDisplay id in intersectionPoints)
            {
                id.RemoveItselfFromView();
            }
            intersectionPoints.Clear();

            foreach (TagDisplay tag1 in tags)
            {
                foreach (TagDisplay tag2 in tags)
                {
                    if (!tag1.Equals(tag2))
                    {
                        Intersection i = new Intersection(tag1.Origo, tag1.Distance, tag2.Origo, tag2.Distance);
                        if (i.Points != null && i.Points.Count > 0)
                        {
                            foreach (Point p in i.Points)
                            {
                                IntersectionDisplay idisplay = new IntersectionDisplay(p, backgr);
                                intersectionPoints.Add(idisplay);
                            }
                        }
                    }
                }
            }
            CalculateCommonPoint();
        }

        public void CalculateCommonPoint()
        {
            result = calculator.CalculateLocation(tags, result);
            simPoint.Move(result.SimulatedLocation);
        }
    }
}
