using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleFileManager.Options;


//Базовый класс работы с директориями. Имя выбрано намеренно, чтобы не путать с базовой библиотекой
namespace ConsoleFileManager.Models
{
    public class DirectoryClass : FileDirectorySystemObject
    {
        private readonly DirectoryInfo _Directory;

        public string Name => _Directory.Name;

        public string Extension => _Directory.Extension;

        public bool Exist => _Directory.Exists;

        public long TotalSize => _Directory
           .EnumerateFiles("*.*", SearchOption.AllDirectories)
           .Sum(f => f.Length);

        public long GetTotalLength() => _Directory
           .EnumerateFiles("*.*", SearchOption.AllDirectories)
           .Sum(f => f.Length);

        public DirectoryClass(string directoryPath)
        {
            _Directory = new DirectoryInfo(directoryPath);
        }

        public DirectoryClass(DirectoryInfo directoryInfo)
        {
            _Directory = directoryInfo;
        }

        public DirectoryInfo[] GetDirectories(string? Mask = null)
        {
            if (Mask is null)
                return _Directory.GetDirectories();

            return _Directory.GetDirectories(Mask);
        }

        public FileInfo[] GetFiles(string? Mask = null)
        {
            if (Mask is null)
                return _Directory.GetFiles();

            return _Directory.GetFiles(Mask);
        }
        public IEnumerable<DirectoryClass> EnumerateDirectories(string? Mask = null)
        {
            var files = Mask is null
                ? _Directory.EnumerateDirectories()
                : _Directory.EnumerateDirectories(Mask);

            foreach (var directory in files)
                yield return (DirectoryClass)directory;
        }

        public IEnumerable<FileClass> EnumerateFiles(string? Mask = null)
        {
            if (Mask is null)
                return _Directory.EnumerateFiles().Select(file => new FileClass(file));

            return _Directory.EnumerateFiles(Mask).Select(file => new FileClass(file));
        }
        public IEnumerable<FileDirectorySystemObject> EnumerateContent(string? Mask = null)
        {
            var items = Mask is null
                ? _Directory.EnumerateFileSystemInfos()
                : _Directory.EnumerateFileSystemInfos(Mask);



            return items.Select<FileSystemInfo, FileDirectorySystemObject>(item => item switch
            {
                FileInfo file => new FileClass(file),
                DirectoryInfo dir => new DirectoryClass(dir),
                _ => throw new InvalidOperationException("Неподдерживаемый тип данных " + item.GetType())
            });
        }

        public FileClass[] GetFiles(int Skip, int Count)
        {
            return EnumerateFiles()
               .Skip(Skip)
               .Take(Count)
               .ToArray();
        }

        public FilesPage GetFilesPage(int index, int size)
        {
            var all_files = EnumerateFiles();
            var page_files = all_files.Skip(index * size).Take(size).ToArray();
            var total_count = all_files.Count();

            return new FilesPage(index, page_files.Length, size, page_files, total_count);
        }

        public static implicit operator DirectoryInfo(DirectoryClass model) => model._Directory;

        public static explicit operator DirectoryClass(DirectoryInfo dir) => new DirectoryClass(dir);

        public bool CreateFolder(string name, UserParameters userParameters)
        {
            try
            {
                if (!Directory.Exists(name))
                {
                    Directory.CreateDirectory(name);
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

        public bool DeleteFolder(string name, UserParameters userParameters)
        {
            try
            {
                if (Directory.Exists(name))
                {
                    Directory.Delete(name);
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

        public bool MoveFolder(string toPath, UserParameters userParameters)
        {
            try
            {
                Directory.Move(_Directory.FullName, toPath);
                return true;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }

        public bool CopyFolder(string toPath, UserParameters userParameters)
        {
            try
            {
                Directory.CreateDirectory(toPath);
                foreach (string s1 in Directory.GetFiles(_Directory.FullName))
                {
                    string s2 = toPath + "\\" + Path.GetFileName(s1);
                    File.Copy(s1, s2);
                }
                foreach (string s in Directory.GetDirectories(_Directory.FullName))
                {
                    CopyFolder(toPath + "\\" + Path.GetFileName(s), userParameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
                return false;
            }
        }
    }
}