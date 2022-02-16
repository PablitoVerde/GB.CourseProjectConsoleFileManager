using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Models;
using ConsoleFileManager.Grafics;
using ConsoleFileManager.Options;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandCopy : FileManagerCommand
    {
        public FileManagerCommandCopy()
        {
            CommandName = "copy";
            CommandDescription = "Копировать папку или файл";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            Console.Write("Введите путь до объекта, который нужно скопировать > ");
            string fromPath = Console.ReadLine();

            Console.Write(Environment.NewLine);

            Console.Write("Введите новый путь > ");
            string toPath = Console.ReadLine();

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            FileAttributes fattFromPath = File.GetAttributes(fromPath);

            bool resultCopy = false;

            if ((fattFromPath & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryClass directoryClass = new DirectoryClass(fromPath);
                resultCopy = directoryClass.MoveFolder(toPath, userParameters);
            }
            else if ((fattFromPath & FileAttributes.Archive) == FileAttributes.Archive)
            {
                FileClass fileClass = new FileClass(fromPath);
                resultCopy = fileClass.CopyFile(toPath, userParameters);
            }

            if (resultCopy)
                Console.WriteLine("Операция проведена успешно.");
            else
                Console.WriteLine("Ошибка копирования");

            MenuDrawings.DrawHorizontalLine();
        }
    }
}
