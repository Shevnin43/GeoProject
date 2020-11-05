using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeoProject.Drawing
{
    public static class GeoDraw
    {
        /// <summary>
        /// Масштаб
        /// </summary>
        private static int Scale {get; set;}
        private static double MinX { get; set; }
        private static double MinY { get; set; }
        /// <summary>
        /// Рисовательное место (канвас)
        /// </summary>
        private static Canvas Draw { get; set; }
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="draw"></param>
        public static void DrawFigure(GeoFigure figure, Canvas draw)
        {
            Draw = draw;
            if (figure.Points.Length > 1)
            {
                for (var i = 1; i < figure.Points.Length; i++)
                {
                    var line = new Line
                    {
                        Y1 = CCX(figure.Points[i - 1].CoordX),
                        Y2 = CCX(figure.Points[i].CoordX),
                        X1 = CCY(figure.Points[i - 1].CoordY),
                        X2 = CCY(figure.Points[i].CoordY),
                        Stroke = Brushes.Blue,
                        StrokeThickness = 1
                    };
                    Draw.Children.Add(line);
                    DrawPoint(figure.Points[i]);
                }
            }
            else if (figure.Points.Length == 1)
            {
                DrawPoint(figure.Points[0]);
            }
        }
        /// <summary>
        /// Нарисовать ограничивающий фигуру прямоугольник
        /// </summary>
        /// <param name="box"></param>
        public static void DrawBounding(string[] box, Canvas draw)
        {
            Draw = draw;
            double height=1, width = 1, xStart = 1, xEnd = 1, yStart = 1, yEnd = 1;
            if (double.TryParse(box[1].Replace(".", ","), out xEnd) && double.TryParse(box[0].Replace(".", ","), out xStart))
            {
                width = xEnd - xStart;
            }
            if (double.TryParse(box[3].Replace(".", ","), out yEnd) && double.TryParse(box[2].Replace(".", ","), out yStart))
            {
                height = yEnd - yStart;
            }
            Scale = new List<int>
            {
                Convert.ToInt32((Draw.ActualWidth ) / height),
                Convert.ToInt32((Draw.ActualHeight ) / width)
            }.Min();
            MinX = xStart;
            MinY = yStart;
            var rectangle = new Rectangle
            {
                Height = CCX(xEnd) - CCX(xStart),
                Width = CCY(yEnd) - CCY(yStart),
                Fill = Brushes.LightBlue,
                Stroke = Brushes.LightPink,
                StrokeThickness = 1,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Draw.Children.Add(rectangle);
        }
        /// <summary>
        /// Нарисовать точку
        /// </summary>
        /// <param name="figure"></param>
        public static void DrawPoint(GeoPoint point)
        {
            var point1 = new Line
            {
                Y1 = CCX(point.CoordX) - 2,
                Y2 = CCX(point.CoordX) + 2,
                X1 = CCY(point.CoordY) - 2,
                X2 = CCY(point.CoordY) + 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Draw.Children.Add(point1);
            var point2 = new Line
            {
                Y1 = CCX(point.CoordX) + 2,
                Y2 = CCX(point.CoordX) - 2,
                X1 = CCY(point.CoordY) - 2,
                X2 = CCY(point.CoordY) + 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Draw.Children.Add(point2);
        }

        /// <summary>
        /// Преобразование координат объектов к координатам отображения X
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private static int CCX(double coord) => Convert.ToInt32((coord - MinX)  * Scale);
        /// <summary>
        /// Преобразование координат объектов к координатам отображения Y
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private static int CCY(double coord) => Convert.ToInt32((coord - MinY) * Scale);
    }
}
