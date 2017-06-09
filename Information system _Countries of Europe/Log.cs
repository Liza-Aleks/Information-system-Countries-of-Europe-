using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_system__Countries_of_Europe
{
   public class Log
    {
        
        public static void L(string log)
        {
            using (var s = File.AppendText("../../logi.txt"))
            {
                s.WriteLine(log);
            }
        }

    }
}
