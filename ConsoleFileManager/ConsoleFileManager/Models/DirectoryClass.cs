using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//Базовый класс работы с директориями. Имя выбрано намеренно, чтобы не путать с базовой библиотекой
namespace ConsoleFileManager.Models
{
    public class DirectoryClass : SystemObject
    {
        /// <summary>
        /// Свойство получения размера директории
        /// </summary>
        public long Size
        {
            get
            {
                return GetDirectorySize(PathToObject);
            }
        }

        public DirectoryClass()
        {

        }

        public DirectoryClass(string path)
        {
            Name = Path.GetDirectoryName(path);
            PathToObject = path;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);           
            IsHidden = directoryInfo.Attributes.HasFlag(FileAttributes.Hidden);
        }

        /// <summary>
        /// Приватный метод получения размера файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private long GetDirectorySize(string path)
        {
            long result = 0;

            //Пройти по подпапкам и файлам
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            //Собрать размер файлов в папке
            foreach (string f in files)
            {
                FileClass file = new FileClass(f);
                result = result + file.Size;
            }

            //Пройти с рекурсией по подпапкам и далее
            foreach (string d in dirs)
            {
                result = result + GetDirectorySize(d);
            }

            return result;
        }
    }
}