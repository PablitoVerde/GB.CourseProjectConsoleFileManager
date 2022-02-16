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
    public class FileManagerCommandChangePage : FileManagerCommand
    {
        public FileManagerCommandChangePage()
        {
            CommandName = "page";
            CommandDescription = "Перейти на другую страницу";
        }

        public override void CommandExecute()
        {
            UserParameters userParameters = new UserParameters();
            userParameters.LoadUserParameters();

            Console.Write("Введите номер страницы > ");

            try
            {
                int page = Convert.ToInt32(Console.ReadLine());
                if (page > 0 && page < userParameters.LastPathToDirectory.Count() / userParameters.FilesAndDirScale)
                {
                    userParameters.CurrentPage = page;
                    userParameters.SaveUserParameters();
                }

            }
            catch (Exception ex)
            {
                userParameters.SaveUserErrors(ex);
            }
        }
    }
}
