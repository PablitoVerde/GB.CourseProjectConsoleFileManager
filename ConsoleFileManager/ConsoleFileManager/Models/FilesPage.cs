using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Options;

namespace ConsoleFileManager.Models
{
    public class FilesPage
    {
        public int Index { get; }

        public int Count { get; }

        public int Size { get; }

        public IEnumerable<FileClass> Files { get; }

        public int TotalCount { get; }

        public int PagesCount => (int)Math.Floor((double)TotalCount / Size);

        public FilesPage(int index, int count, int size, IEnumerable<FileClass> files, int totalCount)
        {
            Index = index;
            Count = count;
            Size = size;
            Files = files;
            TotalCount = totalCount;
        }

        public FilesPage()
        {

        }


        public void GetPage(DirectoryClass directoryClass, UserParameters userParameters, string? mask = null)
        {
            DirectoryInfo[] directoryInfos;

            if (mask is null)
                directoryInfos = directoryClass.GetDirectories();
            else
                directoryInfos = directoryClass.GetDirectories(mask);

            FileInfo[] fileInfos;

            if (mask is null)
                fileInfos = directoryClass.GetFiles();
            else
                fileInfos = directoryClass.GetFiles(mask);

            int count = fileInfos.Length + directoryInfos.Length;
        }
    }
}