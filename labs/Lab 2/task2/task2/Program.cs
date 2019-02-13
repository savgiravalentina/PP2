using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            //function to identify prime numbers
            bool isPrime(int x)
            {
                int crt = 0;
                for (int i = 1; i <= x; i++)
                {
                    if (x % i == 0)
                    {
                        crt++;
                    }
                }
                if (crt == 2) return true;
                return false;
            }
            //setting the pah for in and output files
            StreamReader sr = new StreamReader(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task2\read.txt");
            StreamWriter sw = new StreamWriter(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task2\write.txt");
            //creating an array of elemetns from input file
            string[] arr = sr.ReadLine().Split();
            for (int i = 0; i < arr.Length; i++)
            {
                int x = int.Parse(arr[i]);
                if (isPrime(x) == true)
                {
                    //writing numbers, that satisfies given condition
                    sw.Write(x + " ");
                }
            }
            sr.Close();
            sw.Close();
        }
    }
}
