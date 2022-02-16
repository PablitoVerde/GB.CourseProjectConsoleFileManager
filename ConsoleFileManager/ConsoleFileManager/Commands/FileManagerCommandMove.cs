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
    public class FileManagerCommandMove : FileManagerCommand
    {
        public FileManagerCommandMove()
        {
            CommandName = "move";
            CommandDescription = "Перенести объект";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            Console.Write("Введите путь до объекта, который нужно перенести > ");
            string fromPath = Console.ReadLine();

            Console.Write(Environment.NewLine);

            Console.Write("Введите новый путь > ");
            string toPath = Console.ReadLine();

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            FileAttributes fattFromPath = File.GetAttributes(fromPath);

            bool resultMove = false;

            if ((fattFromPath & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryClass directoryClass = new DirectoryClass(fromPath);
                resultMove = directoryClass.MoveFolder(toPath, userParameters);
            }
            else if ((fattFromPath & FileAttributes.Archive) == FileAttributes.Archive)
            {
                FileClass fileClass = new FileClass(fromPath);
                resultMove = fileClass.MoveFile(toPath, userParameters);
            }

            if (resultMove)
                Console.WriteLine("Операция проведена успешно.");
            else
                Console.WriteLine("Ошибка перемещения");

            MenuDrawings.DrawHorizontalLine();
        }
    }
}
