using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string q = (@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task4\path.txt"); // File creation paths
            string w = (@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task4\whowho.txt"); 
            StreamWriter dd = new StreamWriter(q);
            string asd = "Who let the dogs out who who who";
            dd.Write(asd);
            dd.Close();
            File.Copy(q, w); // Copy the new file
            File.Delete(q); // delete the first file
        }
    }
}
