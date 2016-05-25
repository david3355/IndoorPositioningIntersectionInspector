using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IntersectionInspector.src.locationcalc.util
{
    public class LocationResult
    {
        public LocationResult(Point SimulatedLocation, Precision Precision, double Radius)
        {
            this.SimulatedLocation = SimulatedLocation;
            this.Precision = Precision;
            this.Radius = Radius;
        }

        public LocationResult(Point SimulatedLocation, Precision Precision)
            : this(SimulatedLocation, Precision, -1)
        { }

        public LocationResult() { }

        public Point SimulatedLocation { get; set; }
        public Precision Precision { get; set; }
        public double Radius { get; set; }
    }
}
