using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Базовый класс файла. Имя выбрано таким, чтобы избежать путаницы с базовой библиотекой

namespace ConsoleFileManager.Models
{
    public class FileClass : FileDirectorySystemObject
    {
        private readonly FileInfo _File;

        public string Name => _File.Name;

        public string Extension => _File.Extension;

        public bool Exist => _File.Exists;


        /// <summary>
        /// Конструктор для создания объекта класса Файл.
        /// </summary>
        /// <param Путь до файла="path"></param>
        public FileClass(string filePath)
        {
            _File = new FileInfo(filePath);
        }

        public FileClass(FileInfo fileInfo)
        {
            _File = fileInfo;
        }

        public IEnumerable<string> EnumerateLines()
        {
            if (!_File.Exists)
                throw new FileNotFoundException("Файл не найден", _File.FullName);

            using var reader = _File.OpenText();

            while (!reader.EndOfStream)
                yield return reader.ReadLine()!;
        }
    }
}