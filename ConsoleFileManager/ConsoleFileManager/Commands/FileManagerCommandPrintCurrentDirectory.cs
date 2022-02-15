using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Grafics;
using ConsoleFileManager.Options;
using ConsoleFileManager.Models;

namespace ConsoleFileManager.Commands
{
    /// <summary>
    /// Класс команды по выводу текущей директории. Происходит автоматически при обновлении экрана пользователя.
    /// </summary>
    public class FileManagerCommandPrintCurrentDirectory : FileManagerCommand
    {
        public FileManagerCommandPrintCurrentDirectory()
        {
            CommandName = "";
            CommandDescription = "Вывести на экран текущую директорию";
        }

        public override void CommandExecute()
        {
            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            FilesPage filesPage = new FilesPage();

            var str = new DirectoryClass(userParameters.LastPathToDirectory).GetFilesPage(userParameters.CurrentPage,userParameters.FilesAndDirScale);

            foreach (FilePage str2 in str)
            {
                Console.WriteLine(str2);
            }

            Console.ReadKey();
        }
    }
}
