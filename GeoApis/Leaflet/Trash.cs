using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApis.Leaflet
{
    class Trash
    {

        private static decimal getBoundsArea(LatLngBounds bounds)
        {
            LatLng nw = bounds.NorthWest;
            LatLng se = bounds.SouthEast;
            // https://github.com/openstreetmap/cgimap/blob/master/src/bbox.cpp

            decimal maxLng = System.Math.Max(nw.lng, se.lng);
            decimal maxLat = System.Math.Max(nw.lat, se.lat);

            decimal minLng = System.Math.Min(nw.lng, se.lng);
            decimal minLat = System.Math.Min(nw.lat, se.lat);
            decimal area = (maxLng - minLng) * (maxLat - minLat);

            return area;
        } // End Function getBoundsArea 


        // radius = sizeInMeters/2
        public static LatLngBounds toBounds(LatLng point, decimal sizeInMeters)
        {
            decimal latAccuracy = 180.0m * sizeInMeters / 40075017m;
            decimal lngAccuracy = latAccuracy / (decimal)System.Math.Cos((System.Math.PI / 180.0d) * (double)point.lat);

            //           N
            //          180
            // (W) -180     +180 (E)
            //         -180
            //           S

            // https://github.com/Leaflet/Leaflet/blob/master/src/geo/LatLng.js
            // https://github.com/Leaflet/Leaflet/blob/master/src/geo/LatLngBounds.js
            // constructor(southWest: LatLngExpression, northEast: LatLngExpression);
            // a = [point.lat - latAccuracy, point.lng - lngAccuracy],
            // b = [point.lat + latAccuracy, point.lng + lngAccuracy]
            // new LatLngBounds(a, b); 

            decimal south = point.lat - latAccuracy;
            decimal west = point.lng - lngAccuracy;
            decimal north = point.lat + latAccuracy;
            decimal east = point.lng + lngAccuracy;


            // https://en.wikipedia.org/wiki/Ellipse
            // https://en.wikipedia.org/wiki/Latitude
            // https://en.wikipedia.org/wiki/Longitude
            // https://de.wikipedia.org/wiki/Wendekreis_(Breitenkreis)

            return new LatLngBounds(north, south, east, west);
        }

    }
}
