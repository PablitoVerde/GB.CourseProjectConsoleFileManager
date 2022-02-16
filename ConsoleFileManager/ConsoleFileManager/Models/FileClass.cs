using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleFileManager.Options;

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

        public bool CreateFile(string path, UserParameters userParameters)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }

        public bool DeleteFile(string path, UserParameters userParameters)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }

        public bool MoveFile(string path, UserParameters userParameters)
        {
            try
            {
                File.Move(_File.FullName, path);
                return true;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }

        public bool CopyFile(string path, UserParameters userParameters)
        {
            try
            {
                File.Copy(_File.FullName, path);
                return true;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }

        public long GetSize()
        {
            return _File.Length;
        }
    }
}