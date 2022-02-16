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
    public class FileManagerCommandCreateFolder:FileManagerCommand
    {
        public FileManagerCommandCreateFolder()
        {
            CommandName = "crdir";
            CommandDescription = "Создать папку";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            Console.Write("Введите название новой папки > ");
            string str = Console.ReadLine();

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            DirectoryClass directoryClass = new DirectoryClass(str);
            bool result = directoryClass.CreateFolder(str, userParameters);

            if (result)
                Console.WriteLine("Новая директория создана!");
            else
                Console.WriteLine("Ошибка при создании директории.");
            MenuDrawings.DrawHorizontalLine();
        }
    }
}
