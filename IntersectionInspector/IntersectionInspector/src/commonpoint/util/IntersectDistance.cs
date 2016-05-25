using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IntersectionInspector.src.commonpoint.util
{
    class IntersectDistance
    {
        public IntersectDistance(Point P1, Point P2)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.Distance = LocationCalculator.Distance(P1, P2);
        }

        public double Distance { get; set; }
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public override string ToString()
        {
            return String.Format("D: {0} Point 1: [{1};{2}] Point 2: [{3};{4}]", Math.Round(Distance, 2), Math.Round(P1.X, 2), Math.Round(P1.Y, 2), Math.Round(P2.X, 2), Math.Round(P2.Y, 2));
        }

        public override bool Equals(object obj)
        {
            IntersectDistance idist = obj as IntersectDistance;
            return (this.P1.Equals(idist.P1) && this.P2.Equals(idist.P2)) || (this.P1.Equals(idist.P2) && this.P2.Equals(idist.P1));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
