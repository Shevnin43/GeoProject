using System;
using System.Collections.Generic;
using System.Text;

namespace GeoProject.Drawing
{
    public class GeoPoint
    {
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public string Description { get; set; }

        public GeoPoint(double x, double y)
        {
            CoordX = x;
            CoordY = y;
        }
        public GeoPoint(double x, double y, string desc)
        {
            CoordX = x;
            CoordY = y;
            Description = desc;
        }
    }
}
