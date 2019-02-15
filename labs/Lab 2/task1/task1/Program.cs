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
            string word = File.ReadAllText(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task1\testing.txt"); // read from the file string word = File.ReadAllText(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task1\testing.txt")
            char[] arr = word.ToCharArray(); //prevrashaem array into char array  char[] arr = word.ToCharArray()
            string reverse = String.Empty; //create empty string  to reverse elements
            
            for (int i = arr.Length - 1; i > -1; i--) //reversing the string  int i = arr.Length - 1; i > -1; i--
            {
                reverse += arr[i]; //adding to reverse string chars from arrayv
            }
            if (reverse == word) //if it equals.....
            {
                Console.WriteLine("Yes");
            }
            else Console.WriteLine("No");
            Console.ReadKey();
        }
    }
}
