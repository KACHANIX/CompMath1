using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompMath1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var menu = new Menu.Menu();
            bool myProgramIsRunning = true;

            menu.AddItem("Use data from file", Functions.FileInput);
            menu.AddItem("Use the keyboard input", Functions.KeyboardInput);
            menu.AddItem("Randomized input", Functions.RandomInput);
            menu.AddItem("Quit", Functions.Quit);
            while (myProgramIsRunning)
            {
                Console.Clear();
                myProgramIsRunning = menu.Display();
                Console.ReadKey();
            }
        }
    }
}
