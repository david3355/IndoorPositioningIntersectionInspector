using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IntersectionInspector.src
{
    /// <summary>
    /// Egy Intersection objektum két kör metszéspontját, vagy metszéspontjait tárolja egy listában.
    /// Értelemszerűen, ha a két körnek nincs metszéspontja, a lista 0 elemet tartalmaz
    /// Ha a két kör origójának távolsága egyenlő a két kör sugarainak összegével, 1 pontban érintik egymást, a lista 2 pontot tartalmaz, melyek egybeesnek
    /// Általános esetben pedig a két kör két pontban metszi egymást, ekkor ezt a két pontot tárolja a lista.
    /// </summary>
    class Intersection
    {
        public Intersection(Point P0, double r0, Point P1, double r1)
        {
            points = CalculateIntersection(P0, r0, P1, r1).Points;
        }

        public Intersection(List<Point> IntersectionPoints)
        {
            points = IntersectionPoints;
        }

        private List<Point> points;

        /// <summary>
        /// null: érvénytelen, végtelen sok metszéspont
        /// üres: nincs metszéspont
        /// két elemű: a két metszéspontot tartalmazza (érintés esetén a két metszéspont egyenlő)
        /// </summary>
        public List<Point> Points
        {
            get { return points; }
        }


        /// <summary>
        /// Két, a középpontjával és sugarával adott körnek számolja ki a metszéspontjait, ha lézetnek, vagy létezik
        /// </summary>
        /// <param name="P0">Az első kör középpontja</param>
        /// <param name="r0">Az első kör sugara</param>
        /// <param name="P1">A második kör középpontja</param>
        /// <param name="r1">A második kör sugara</param>
        /// <returns>Az Intersection objektum a metszéspontokat reprezentálja, melyben egy lista tárolja, hogy hány metszéspontja van a két körnek</returns>
        public static Intersection CalculateIntersection(Point P0, double r0, Point P1, double r1)
        {
            double d, a, h;
            Point P2, isPoint1, isPoint2;
            d = LocationCalculator.Distance(P0, P1);

            if (d > r0 + r1) return new Intersection(new List<Point>());    // Két külön körről van szó
            if (d < Math.Abs(r0 - r1)) return new Intersection(new List<Point>());   // Az egyik kör a másikon belül van
            if (d == 0 && r0 == r1) return new Intersection(null);      // A két kör éppen egybeesik
            //TODO: megvizsgálni, hogy két metszéspont lesz, egy, végtelen, vagy nem érintkeznek

            a = (Math.Pow(r0, 2) - Math.Pow(r1, 2) + Math.Pow(d, 2)) / (2 * d);
            h = Math.Sqrt(Math.Pow(r0, 2) - Math.Pow(a, 2));
            P2 = new Point(P0.X + a * (P1.X - P0.X) / d, P0.Y + a * (P1.Y - P0.Y) / d);

            isPoint1 = new Point(P2.X + h * (P1.Y - P0.Y) / d, P2.Y - h * (P1.X - P0.X) / d);
            isPoint2 = new Point(P2.X - h * (P1.Y - P0.Y) / d, P2.Y + h * (P1.X - P0.X) / d);

            // Ha a két kör éppen érinti egymást egy ponton (d = r1 + r2), akkor is két pont lesz a metszet eredménye, de ezek egybeesnek
            List<Point> solutions = new List<Point>();
            solutions.Add(isPoint1);
            solutions.Add(isPoint2);
            return new Intersection(solutions);
        }

        public override string ToString()
        {
            if (points == null) return "Infinite intersection points";
            if (points.Count == 0) return "No intersection point";
            StringBuilder stb = new StringBuilder();
            for (int i = 0; i < points.Count; i++)
            {
                stb.Append(String.Format("[{0};{1}]", Math.Round(points[i].X, 2), Math.Round(points[i].Y, 2)));
                if (i == 0) stb.Append(" - ");
            }
            return stb.ToString();
        }
    }
}
