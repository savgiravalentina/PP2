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
            //function to identify prime numbers has crt=0  and if x%i =0 then crt ++ i from one to less or equal x
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
                if (crt == 2) return true; //like divides na 1 and samo sebya
                return false; //even more than 2
            }
            //setting the pah for in and output files
            StreamReader sr = new StreamReader(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task2\read.txt"); //stream reader to read numbers
            StreamWriter sw = new StreamWriter(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 2\task2\write.txt"); //stream writer to write results
            //creating an array of elemetns from input file
            string[] arr = sr.ReadLine().Split(); //split to separate numbers as individual
            for (int i = 0; i < arr.Length; i++) // uuuntil i is not equal arrays length parse to int x and use function isPrime
            {
                int x = int.Parse(arr[i]);
                if (isPrime(x) == true) //if given number is prime we gonna write it to sw 
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
