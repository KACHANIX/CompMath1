using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompMath1.Menu
{
    public class MenuItem
    {
        public string Description;
        public Func<bool> Action;
        public ConsoleKey Key;
    }
}
