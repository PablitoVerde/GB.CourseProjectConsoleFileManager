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

            var str = filesPage.GetPage(new DirectoryClass(userParameters.LastPathToDirectory), userParameters);

            MenuDrawings.DrawHorizontalLine();

            foreach (string str2 in str)
            {
                Console.WriteLine(str2);
            }
            MenuDrawings.DrawHorizontalLine();

            Console.WriteLine($"Количество строк вывода {userParameters.FilesAndDirScale}. " +
                $"\nВсего элементов в директории {str.Count}. " +
                $"\nПоказана страница {userParameters.CurrentPage}. " +
                $"\nВсего страниц {Math.Floor((double)(str.Count + userParameters.FilesAndDirScale) / (double)userParameters.FilesAndDirScale)}.");

            MenuDrawings.DrawHorizontalLine();
        }
    }
}
