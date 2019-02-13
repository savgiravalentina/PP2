using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = File.ReadAllText(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task1\testing.txt"); // read from the file
            char[] arr = word.ToCharArray();
            string reverse = String.Empty;
            //reversing the string
            for (int i = arr.Length - 1; i > -1; i--)
            {
                reverse += arr[i];
            }
            if (reverse == word)
            {
                Console.WriteLine("Yes");
            }
            else Console.WriteLine("No");
            Console.ReadKey();
        }
    }
}
