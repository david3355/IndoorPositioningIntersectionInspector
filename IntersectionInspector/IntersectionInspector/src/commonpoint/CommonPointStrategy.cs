using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IntersectionInspector.src.commonpoint
{
    abstract class CommonPointStrategy
    {
        public Point CommonPointOfIntersections(List<Intersection> intersections)
        {
            intersections.RemoveAll(intersection => intersection == null || intersection.Points == null);
            if (intersections.Count < 2) throw new ArgumentException("Argument list must contain 2 or more intersections!");
            foreach (Intersection isection in intersections)
            {
                if (isection.Points.Count != 2) throw new ArgumentException("Every valid intersection must contain 2 points!");
            }

            return CalculateCommonPoint(intersections);
        }

        public abstract Point CalculateCommonPoint(List<Intersection> intersections);
    }
}
