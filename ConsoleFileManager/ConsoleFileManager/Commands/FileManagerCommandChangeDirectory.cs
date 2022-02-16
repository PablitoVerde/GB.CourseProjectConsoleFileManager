using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Grafics;
using ConsoleFileManager.Options;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandChangeDirectory : FileManagerCommand
    {
        public FileManagerCommandChangeDirectory()
        {
            CommandName = "cd";
            CommandDescription = "Поменять директорию";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            Console.WriteLine("Введите новую директорию > ");
            string dir = Console.ReadLine();

            if (Directory.Exists(dir))
            {
                userParameters.LastPathToDirectory = dir;
                userParameters.SaveUserParameters();
            }
            else
                Console.WriteLine("Путь введен неправильно");
        }       
    }
}
