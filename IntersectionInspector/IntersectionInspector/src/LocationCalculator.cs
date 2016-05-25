using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IntersectionInspector.src
{
    abstract class LocationCalculator
    {
        public static double Distance(Point P0, Point P1)
        {
            return Math.Sqrt(Math.Pow(P0.X - P1.X, 2) + Math.Pow(P0.Y - P1.Y, 2));
        }

        /// <summary>
        /// A két pont közti felezőpontot számítja ki
        /// </summary>
        /// <returns>A felezőpont</returns>
        public static Point Midpoint(Point P0, Point P1)
        {
            return new Point((P0.X + P1.X) / 2, (P0.Y + P1.Y) / 2);
        }

        /// <summary>
        /// A paraméterként megadott pontok átlagát adja, vagyis az átlag középpontot (két pont esetében ez a felezőpont)
        /// </summary>
        public static Point PointAverage(List<Point> Points)
        {
            double sx = 0;
            double sy = 0;
            int n = Points.Count;
            foreach (Point p in Points)
            {
                sx += p.X;
                sy += p.Y;
            }
            return new Point(sx / n, sy / n);
        }


    }
}
