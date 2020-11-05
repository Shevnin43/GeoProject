using GeoProject.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeoProject.openstreetmap
{
    public class OSMWorker : IWorker
    {
        public Canvas Draw { get; set; }
        public string Json { get; set; }
        public List<object> Objects { get; set; } = new List<object>();
        public List<object> TempObjects { get; set; } = new List<object>();

        /// <summary>
        /// Преобразуем в объекты (Polygon, Point и др.)
        /// </summary>
        public void ConvertToObjects()
        {
            try
            {
                var array = JArray.Parse(Json);
                foreach (var doc in array)
                {
                    switch ((doc as dynamic).geojson.type.Value)
                    {
                        case "Polygon":
                            Objects.Add(doc.ToObject<JPolygon>());
                            break;
                        case "Point":
                            Objects.Add(doc.ToObject<JPoint>());
                            break;
                        case "MultiPolygon":
                            Objects.Add(doc.ToObject<JMultiPolygon>());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке преобразовать объекты произошло исключение: {ex.Message}");
                Objects.Clear();
            }
        }

        /// <summary>
        /// Получаем информацию с сайта
        /// </summary>
        /// <param name="requestText"></param>
        /// <returns></returns>
        public async Task<bool> GetJson(string requestText)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://nominatim.openstreetmap.org/search?q={requestText}&format=json&polygon_geojson=1"),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.Headers.Add("accept-encoding", "gzip, deflate, br");
            request.Headers.Add("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            try
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    Json = await responseContent.ReadAsStringAsync();
                    Json = Json.Replace("class", "class_");
                    MessageBox.Show($"Сервер вернул.");
                    return true;
                }
                MessageBox.Show($"Сервер отказал.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке получить данные произошло исключение: {ex.Message}");
                return false;
            }
        }


        public void GetFigure(object obj)
        {
            if (!(obj is JPoint))
            {
                GeoDraw.DrawBounding((obj as JAbstract).boundingbox, Draw);
            }
            if (obj is JPolygon PolyObject) 
            {
                foreach (var contour in PolyObject.geojson.coordinates)
                {
                    var figure = new GeoFigure()
                    {
                        Points = new GeoPoint[contour.Length]
                    };
                    for (var i=0; i < contour.Length; i++)
                    {
                       figure.Points[i] = new GeoPoint(contour[i][1], contour[i][0]);
                    }
                    GeoDraw.DrawFigure(figure, Draw);
                }
            }
            else if (obj is JPoint PointObject)
            {
                var figure = new GeoFigure()
                {
                    Points = new GeoPoint[1]
                };
                figure.Points[0] = new GeoPoint(PointObject.geojson.coordinates[1], PointObject.geojson.coordinates[0]);
                GeoDraw.DrawFigure(figure, Draw);
            }
            else if (obj is JMultiPolygon MPolyObject)
            {
                foreach (var polygons in MPolyObject.geojson.coordinates)
                {
                    foreach (var contour in polygons)
                    {
                        var figure = new GeoFigure()
                        {
                            Points = new GeoPoint[contour.Length]
                        };
                        for (var i = 0; i < contour.Length; i++)
                        {
                            figure.Points[i] = new GeoPoint(contour[i][1], contour[i][0]);
                        }
                        GeoDraw.DrawFigure(figure, Draw);
                    }
                }
            }

        }

        public void ScalePoints(object obj, int delay)
        {
            if (obj is JPoint PointObject)
            {
                TempObjects.Add(PointObject);
                return;
            }
            if (obj is JPolygon PolyObject)
            {
                var result = new JPolygon()
                {
                    place_id = PolyObject.place_id,
                    licence = PolyObject.licence,
                    osm_type = PolyObject.osm_type,
                    osm_id = PolyObject.osm_id,
                    boundingbox = PolyObject.boundingbox,
                    lat = PolyObject.lat,
                    lon = PolyObject.lon,
                    display_name = PolyObject.display_name,
                    class_ = PolyObject.class_,
                    type = PolyObject.type,
                    importance = PolyObject.importance,
                    icon = PolyObject.icon,
                    geojson = new GeoJPolygon()
                    {
                        type = PolyObject.geojson.type,
                        coordinates = new double[PolyObject.geojson.coordinates.Length][][]
                    }
                };
                for (var i = 0; i < PolyObject.geojson.coordinates.Length; i++)
                { 
                    result.geojson.coordinates[i] = RemovePoints(PolyObject.geojson.coordinates[i], delay);
                }
                TempObjects.Add(result);
            }
            else if (obj is JMultiPolygon MPolyObject)
            {
                var result = new JMultiPolygon()
                {
                    place_id = MPolyObject.place_id,
                    licence = MPolyObject.licence,
                    osm_type = MPolyObject.osm_type,
                    osm_id = MPolyObject.osm_id,
                    boundingbox = MPolyObject.boundingbox,
                    lat = MPolyObject.lat,
                    lon = MPolyObject.lon,
                    display_name = MPolyObject.display_name,
                    class_ = MPolyObject.class_,
                    type = MPolyObject.type,
                    importance = MPolyObject.importance,
                    icon = MPolyObject.icon,
                    geojson = new GeoJMultyPolygon()
                    {
                        type = MPolyObject.geojson.type,
                        coordinates = new double[MPolyObject.geojson.coordinates.Length][][][]
                    }
                };
                for (var k=0; k < MPolyObject.geojson.coordinates.Length; k++)
                {
                    var polygon = MPolyObject.geojson.coordinates[k];
                    result.geojson.coordinates[k] = new double[polygon.Length][][];
                    for (var i = 0; i < polygon.Length; i++)
                    {
                        result.geojson.coordinates[k][i] = RemovePoints(MPolyObject.geojson.coordinates[k][i], delay);
                    }
                }
                TempObjects.Add(result);
            }
        }

        private double[][] RemovePoints(double[][] contour, int delay)
        {
            var points = new List<double[]>();
            for (var i = 0; i < contour.Length; i++)
            {
                if (delay == 1 || (i + 1) % delay != 0 || i == contour.Length - 1)
                {
                    points.Add(contour[i]);
                }
            }
            return points.ToArray();

        }
    }
}
