using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandHelp : FileManagerCommand
    {
        public FileManagerCommandHelp()
        {
            CommandName = "help";
            CommandDescription = "Вывести список команд и их описание";
        }

        public override void CommandExecute()
        {
           foreach (var (CommandName, CommandDescription) in Program.Commands)
            {
                Console.WriteLine("\t{0}\t-\t{1}", CommandName, CommandDescription);
            }
        }
    }
}
