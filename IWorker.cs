using GeoProject.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoProject
{
    public interface IWorker
    {
        /// <summary>
        /// Строка (неотпарсенная), полученная с геоистточника
        /// </summary>
        public string Json { get; set; }
        /// <summary>
        /// Приведенные к нужному типу объекты с источника
        /// </summary>
        public List<object> Objects { get; set; }
        /// <summary>
        /// Временные объекты источника для масштабирования
        /// </summary>
        public List<object> TempObjects { get; set; }
        /// <summary>
        /// Метод получения информации из источника
        /// </summary>
        /// <param name="requestText"> Строка запроса от пользователя</param>
        /// <returns></returns>
        public Task<bool> GetJson(string requestText);
        /// <summary>
        /// Метод преобразования полученной от источника информации в объекты источника
        /// </summary>
        public void ConvertToObjects();
        /// <summary>
        /// Получить фигуру для дальнейшей работы из объекта источника
        /// </summary>
        /// <param name="obj">Объект источника</param>
        /// <returns></returns>
        public void GetFigure(object obj);
        public void ScalePoints(object obj, int delay);
    }
}
