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
            string q = (@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task4\path.txt"); // path creation paths through string q
            string w = (@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task4\whowho.txt");  // path new to write smthing new
            string asd = "Who let the dogs out who who who"; //what we gonna write
            dd.Write(asd); //function dd.write(text from asd)
            dd.Close(); //close
            File.Copy(q, w); // Copy the new file from q to w
            File.Delete(q); // delete the first file q we dont need it anymore
        }
    }
}
