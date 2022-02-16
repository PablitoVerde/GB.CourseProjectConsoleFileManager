using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Grafics;
using ConsoleFileManager.Models;
using ConsoleFileManager.Options;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandCreateFile:FileManagerCommand
    {
        public FileManagerCommandCreateFile()
        {
            CommandName = "crfile";
            CommandDescription = "Создать новый файл";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            Console.Write("Введите название нового файла > ");
            string str = Console.ReadLine();
            FileClass fileClass = new FileClass(str);

            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            bool result = fileClass.CreateFile(str, userParameters);

            if (result)
                Console.WriteLine("Файл успешно создан.");
            else
                Console.WriteLine("Ошибка создания файла.");
            MenuDrawings.DrawHorizontalLine();

        }
    }
}
