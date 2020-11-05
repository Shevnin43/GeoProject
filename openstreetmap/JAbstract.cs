using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GeoProject.openstreetmap
{
    public class JAbstract
    {
        public long place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public long osm_id { get; set; }
        public string[] boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        [JsonPropertyName("class")]
        public string class_ { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string icon { get; set; }
    }
}
