using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileManager.Models
{
    /// <summary>
    /// Абстрактный класс объекта системы - файла или папки
    /// </summary>
    public abstract class SystemObject
    {
        public string Name { get; set; } //Название директории
        public string PathToObject { get; set; } // Путь до директории
        public long Size { get; set; } //Размер объекта
        public bool IsHidden { get; set; } //Атрибут "скрытый"
    }
}
