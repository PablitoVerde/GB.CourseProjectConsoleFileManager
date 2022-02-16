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
    public class FileManagerCommandGetSize : FileManagerCommand
    {
        public FileManagerCommandGetSize()
        {
            CommandName = "size";
            CommandDescription = "Вычислить размер файла или папки";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();

            Console.Write("Введите путь до объекта > ");
            string str = Console.ReadLine();

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            FileAttributes fattPath = File.GetAttributes(str);

            if ((fattPath & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryClass directoryClass = new DirectoryClass(str);
                Console.WriteLine($"{directoryClass.GetTotalLength()}");
            }
            else if ((fattPath & FileAttributes.Archive) == FileAttributes.Archive)
            {
                FileClass fileClass = new FileClass(str);             
                Console.WriteLine($"{fileClass.GetSize()}");
            }
            MenuDrawings.DrawHorizontalLine();
        }
    }
}
