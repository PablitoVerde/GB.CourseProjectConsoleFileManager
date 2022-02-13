using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Базовый класс файла. Имя выбрано таким, чтобы избежать путаницы с базовой библиотекой

namespace ConsoleFileManager.Models
{
    public class FileClass : SystemObject
    {
        public string Type { get; set; }

        public bool IsReadOnly { get; set; }


        public FileClass()
        {

        }

        /// <summary>
        /// Конструктор для создания объекта класса Файл.
        /// </summary>
        /// <param Путь до файла="path"></param>
        public FileClass(string path)
        {
            Name = Path.GetFileName(path);
            PathToObject = Path.GetFullPath(path);
            Type = Path.GetExtension(path);

            FileInfo fileInfo = new FileInfo(path);

            Size = fileInfo.Length;
            IsReadOnly = fileInfo.IsReadOnly;
            IsHidden = fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly);
        }
    }
}
