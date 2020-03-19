using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class ConsoleLog : ILog
    {
        public void info(string str)
        {
            Console.WriteLine(str);
        }
    }
}
