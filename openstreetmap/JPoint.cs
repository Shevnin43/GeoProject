
namespace GeoProject.openstreetmap
{
    public class JPoint: JAbstract
    {
        public GeoJPoint geojson { get; set; }
    }

    public class GeoJPoint
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }
}
