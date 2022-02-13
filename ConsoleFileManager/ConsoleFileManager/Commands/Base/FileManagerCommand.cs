using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileManager.Commands
{
    public abstract class FileManagerCommand
    {
        /// <summary>
        /// Команда, которую пользователь может ввести
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Описание команды
        /// </summary>
        public string CommandDescription { get; set; }

        //Абстрактный метод выполнения команды
        public abstract void CommandExecute();
    }
}
