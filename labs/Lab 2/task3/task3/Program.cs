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
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\vsavg\Desktop\pp2-labs"); //directoryinfo = new directory info
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files) //for each file in files we searching fileinfo  and write files name
            {
                Console.WriteLine(file.Name);
            }

            Console.WriteLine();
            DirectoryInfo[] directories = directory.GetDirectories(); //doing the same with directories  dinfo in directories and their name directoryinfo works as array
            //getting names of the files from the choosen folder
            foreach (DirectoryInfo dInfo in directories)
            {
                Console.WriteLine(dInfo.Name);
            }
        }

        static void PrintSpaces(int level) //spaces depends on int level
        {
            //setting the printspaces depending on the position of the files in the folder
            for (int i = 0; i < level; i++)
                Console.Write(" ");
        }

        static void dir(DirectoryInfo directory, int level) //combine functions to make it like pyramide
        {
            FileInfo[] files = directory.GetFiles(); //get files in directories file info
            DirectoryInfo[] directories = directory.GetDirectories(); //getting directories in directories through dir info

            foreach (FileInfo file in files) //for each file in files print space level and then their names
            {
                PrintSpaces(level);
                Console.WriteLine(file.Name);
            }

            foreach (DirectoryInfo d in directories) //for each directory d in directories print space level and add +1 to dir(d, level +1)
            {
                PrintSpaces(level);
                Console.WriteLine(d.Name);
                dir(d, level + 1);
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\vsavg\Desktop\pp2-labs"); //getting path
            dir(d, 0); //0 cause at the end we'll start with one
            Console.ReadKey();
        }
    }
}
