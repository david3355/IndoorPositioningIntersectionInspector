using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using IntersectionInspector.src.commonpoint;
using IntersectionInspector.src.locationcalc.util;

namespace IntersectionInspector.src.locationcalc
{
    class ClosestDistanceLocationCalculator : CalculatorStrategy
    {
        public ClosestDistanceLocationCalculator(CommonPointStrategy CommonPointStrategy)
        {
            commonPointStrategy = CommonPointStrategy;
        }

        protected CommonPointStrategy commonPointStrategy;

        /// <summary>
        /// A megadott bluetooth tag-ek távolságaiból visszaadja a k legkisebb távolságú tag-et, vagyis azok tag-eket, amelyek legközelebb vannak az eszközhöz
        /// </summary>
        /// <param name="Distances">A közeli bluetooth tag-ek, melyek tárolják az eszköztől számított közelítő távoságot</param>
        /// <param name="kLeastDistances">Az a k érték amennyi bluetooth tag-et vissza akarunk kapni, melyek a legközelebb vannak az eszköztől</param>
        /// <returns></returns>
        protected List<TagDisplay> CalculateClosestDistances(List<TagDisplay> Distances, int kLeastDistances)
        {
            // Válasszuk ki a k legkisebb távolságot:
            List<TagDisplay> distancesCopy = Distances.ToList<TagDisplay>();
            List<TagDisplay> leastDistances = new List<TagDisplay>();
            int mini;
            for (int j = 0; j < kLeastDistances; j++)
            {
                mini = 0;
                for (int i = 1; i < distancesCopy.Count; i++)
                {
                    if (distancesCopy[i].Distance < distancesCopy[mini].Distance) mini = i;
                }
                leastDistances.Add(distancesCopy[mini]);
                distancesCopy.RemoveAt(mini);
            }
            return leastDistances;
        }

        protected override LocationResult CalculateCommonPoint(List<TagDisplay> Distances, LocationResult LastLocation)
        {
            List<TagDisplay> leastDistances = CalculateClosestDistances(Distances, 3);

            List<Intersection> intersectionPoints = GetIntersections(leastDistances); // ebben lesznek két-két kör metszéspontjai

            return new LocationResult(commonPointStrategy.CommonPointOfIntersections(intersectionPoints), Precision.ThreeOrMoreTag);
        }
    }
}
