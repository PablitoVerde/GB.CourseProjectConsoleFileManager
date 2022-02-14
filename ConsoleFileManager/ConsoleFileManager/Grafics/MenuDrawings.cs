using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileManager.Grafics
{
    public class MenuDrawings
    {
        static public void DrawHorizontalLine()
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
