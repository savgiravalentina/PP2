using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        { 
            int n = int.Parse(Console.ReadLine()); // convert given number from console to integer
            string[] numb = Console.ReadLine().Split(' '); //using split to separate numbers
            List<int> list = new List<int>(); //creating new list to find primes
            for (int i = 0; i < n; i++)
            {
                int x = int.Parse(numb[i]); //converting the numbers to integers
                int ok = 1;
                for (int j = 2; j <= x / 2; j++) // conditions for the primes
                {
                    if (x % j == 0) 
                    {
                        ok = 0;
                        break;
                    }
                }
                if (ok == 1 && x > 1)   // adding primes to the new list
                    list.Add(x);
            }
            Console.WriteLine(list.Count); //numbers of primes
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " "); //prime numbers
            Console.ReadKey();
        }
    }
}
