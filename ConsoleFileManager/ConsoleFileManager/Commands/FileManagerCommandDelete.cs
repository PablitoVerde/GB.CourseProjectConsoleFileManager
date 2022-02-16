using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Commands;
using ConsoleFileManager.Models;
using ConsoleFileManager.Grafics;
using ConsoleFileManager.Options;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandDelete : FileManagerCommand
    {
        public FileManagerCommandDelete()
        {
            CommandName = "del";
            CommandDescription = "Удалить указанный файл или папку";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();

            Console.Write("Введите путь до объекта, который нужно удалить > ");
            string str = Console.ReadLine();

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            FileAttributes fattPath = File.GetAttributes(str);

            bool resultDelete = false;

            if ((fattPath & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryClass directoryClass = new DirectoryClass(str);
                resultDelete = directoryClass.DeleteFolder(str, userParameters);
            }
            else if ((fattPath & FileAttributes.Archive) == FileAttributes.Archive)
            {
                FileClass fileClass = new FileClass(str);
                resultDelete = fileClass.DeleteFile(str, userParameters);
            }

            if (resultDelete)
                Console.WriteLine("Операция проведена успешно.");
            else
                Console.WriteLine("Ошибка удаления");

            MenuDrawings.DrawHorizontalLine();
        }
    }
}

