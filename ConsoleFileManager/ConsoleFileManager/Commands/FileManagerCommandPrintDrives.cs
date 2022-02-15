using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFileManager.Grafics;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandPrintDrives :FileManagerCommand
    {
        public FileManagerCommandPrintDrives()
        {
            CommandName = "drives";
            CommandDescription = "Вывести на экран список дисков и носителей";
        }

        public override void CommandExecute()
        {
            MenuDrawings.DrawHorizontalLine();
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
                Console.WriteLine("{0} : {1}", drive.Name, drive.RootDirectory.Name);
        }
    }
}
