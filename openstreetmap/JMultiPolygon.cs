using System.Text.Json.Serialization;

namespace GeoProject.openstreetmap
{
    public class JMultiPolygon : JAbstract
    {
        public GeoJMultyPolygon geojson { get; set; }
    }

    public class GeoJMultyPolygon
    {
        public string type { get; set; }
        public double[][][][] coordinates { get; set; }
    }
}
