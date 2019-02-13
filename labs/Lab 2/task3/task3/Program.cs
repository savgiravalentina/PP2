using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task3
{
    class Program
    {
        static void dirs()
        {
            //choosing a folder to get the fileinfo from
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\vsavg\Desktop\pp2-labs");
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                Console.WriteLine(file.Name);
            }

            Console.WriteLine();
            DirectoryInfo[] directories = directory.GetDirectories();
            //getting names of the files from the choosen folder
            foreach (DirectoryInfo dInfo in directories)
            {
                Console.WriteLine(dInfo.Name);
            }
        }

        static void PrintSpaces(int level)
        {
            //setting the printspaces depending on the position of the files in the folder
            for (int i = 0; i < level; i++)
                Console.Write(" ");
        }

        static void dir(DirectoryInfo directory, int level)
        {
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] directories = directory.GetDirectories();

            foreach (FileInfo file in files)
            {
                PrintSpaces(level);
                Console.WriteLine(file.Name);
            }

            foreach (DirectoryInfo d in directories)
            {
                PrintSpaces(level);
                Console.WriteLine(d.Name);
                dir(d, level + 1);
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\vsavg\Desktop\pp2-labs");
            dir(d, 0);
            Console.ReadKey();
        }
    }
}
