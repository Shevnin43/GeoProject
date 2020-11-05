using System.Windows;
using GeoProject.openstreetmap;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using Microsoft.Win32;
using System.Text.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace GeoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string searchText = "Стрижи, Оричевский район, Кировская область";
        public IWorker Worker;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Worker = new OSMWorker()
            {
                Draw = DrawCanvas
            };
        }

        private async void SearchAddress(object sender, RoutedEventArgs e)
        {
            Worker.Objects.Clear();
            Worker.TempObjects.Clear();
            DrawCanvas.Children.Clear();
            if (await Worker.GetJson(SearchText))
            {
                Worker.ConvertToObjects();
            }
            if (!Worker.Objects.Any())
            {
                return;
            }
            DrawFigures(false);
            Worker.Objects.ForEach(x => Worker.TempObjects.Add(x));
            ScalePanel.Visibility = Visibility.Visible;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private void UpdateFigures(object sender, RoutedEventArgs e)
        {
            if (Worker?.Objects?.Count() > 0)
            {
                Worker.TempObjects.Clear();
                foreach (var obj in Worker.Objects)
                {
                    Worker.ScalePoints(obj, Convert.ToInt32(Delay.Value));
                }
                DrawFigures(true);
            }
        }

        private void DrawFigures(bool isTempObjects = false)
        {
            DrawCanvas.Children.Clear();
            foreach (var obj in isTempObjects ? Worker.TempObjects : Worker.Objects)
            {
                Worker.GetFigure(obj);
            }
        }

        private void SaveJson(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = "Файлы json|*.json",
            };
            if (saveDialog.ShowDialog() == true)
            {
                var fileName = saveDialog.FileName;
                var str = JsonSerializer.Serialize(Worker.TempObjects);
                str = Regex.Replace(str, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16));
                File.WriteAllText(fileName, str);
            }
        }
    }
}
