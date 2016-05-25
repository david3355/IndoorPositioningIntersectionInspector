using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using IntersectionInspector.src.commonpoint.util;

namespace IntersectionInspector.src.commonpoint
{
    class ClosestPointsStrategy : CommonPointStrategy
    {
        /// <summary>
        /// Ez működik általános esetre, amikor nincs pontos metszéspont
        /// 
        /// A legközelebbi 3 metszéspont átlagpontját adjuk vissza
        /// Válasszuk ki a metszéspontok távolságai közül a legrövidebb hármat
        /// Elméletileg a 3 távolság 6 metszéspontja lényegében 3 pont, amit keresünk, ezek átlaga kell
        /// </summary>
        public override Point CalculateCommonPoint(List<Intersection> intersections)
        {
            List<IntersectDistance> idistances = new List<IntersectDistance>();     // A metszéspontok távolságai
            IntersectDistance idist;
            for (int i = 0; i < intersections.Count; i++)
            {
                for (int j = 0; j < intersections.Count; j++)
                {
                    if (i != j)
                    {
                        // Egy metszet két metszéspontjának távolságát nem is vesszük figyelembe
                        idist = new IntersectDistance(intersections[i].Points[0], intersections[j].Points[1]);
                        if (!idistances.Contains(idist)) idistances.Add(idist);
                        idist = new IntersectDistance(intersections[i].Points[0], intersections[j].Points[0]);
                        if (!idistances.Contains(idist)) idistances.Add(idist);
                        idist = new IntersectDistance(intersections[i].Points[1], intersections[j].Points[1]);
                        if (!idistances.Contains(idist)) idistances.Add(idist);
                    }
                }
            }

            //---------------------új megoldás:--------------------

            // Az összes metszéspontot ebben a listában tároljuk
            List<Point> intersectPoints = new List<Point>();

            //Ebben a listában tároljuk el a k legközelebbi metszéspontokat
            List<Point> closestPoints = new List<Point>();

            IntersectDistance firstDistance = getMinimumDistance(idistances);
            closestPoints.Add(firstDistance.P1);
            closestPoints.Add(firstDistance.P2);

            foreach (Intersection i in intersections)
            {
                foreach (Point p in i.Points)
                {
                    if (!p.Equals(firstDistance.P1) && !p.Equals(firstDistance.P2))
                    {
                        intersectPoints.Add(p);
                    }
                }
            }

            Point candidate1 = getClosestPoint(firstDistance.P1, intersectPoints);
            Point candidate2 = getClosestPoint(firstDistance.P2, intersectPoints);

            closestPoints.Add(getThirdPoint(candidate1, candidate2, firstDistance));

            return LocationCalculator.PointAverage(closestPoints);

        }

        /// <summary>
        /// Kikeressük a legkisebb metszet távolságot
        /// </summary>
        private IntersectDistance getMinimumDistance(List<IntersectDistance> idistances)
        {
            int mini = 0;
            for (int i = 1; i < idistances.Count; i++)
            {
                if (idistances[i].Distance < idistances[mini].Distance) mini = i;
            }
            return idistances[mini];
        }

        private Point getClosestPoint(Point P, List<Point> IntersectionPoints)
        {
            int mini = 0;
            Point intersectionPoint;
            double mindist = LocationCalculator.Distance(P, IntersectionPoints[0]);
            double dist;
            for (int i = 1; i < IntersectionPoints.Count; i++)
            {
                intersectionPoint = IntersectionPoints[i];
                dist = LocationCalculator.Distance(P, intersectionPoint);
                if (dist < mindist)
                {
                    mini = i;
                    mindist = dist;
                }
            }
            return IntersectionPoints[mini];
        }


        private Point getThirdPoint(Point candidate1, Point candidate2, IntersectDistance minimumDistance)
        {
            double dist1 = getDistance(candidate1, minimumDistance.P1, minimumDistance.P2);
            double dist2 = getDistance(candidate2, minimumDistance.P1, minimumDistance.P2);
            return dist1 < dist2 ? candidate1 : candidate2;
        }

        private double getDistance(Point p1, Point p2, Point p3)
        {
            return LocationCalculator.Distance(p1, p2) + LocationCalculator.Distance(p2, p3) + LocationCalculator.Distance(p1, p3);
        }

    }
}
