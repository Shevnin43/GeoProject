
using System.Text.Json.Serialization;

namespace GeoProject.openstreetmap
{
    public class JPolygon :JAbstract
    {
        public GeoJPolygon geojson { get; set; }
    }

    public class GeoJPolygon
    {
        public string type { get; set; }
        public double[][][] coordinates { get; set; }
    }
}
