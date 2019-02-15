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
            int n = int.Parse(Console.ReadLine()); // convert given number from console to integer int n = int.Parse(Console.ReadLine())
            string[] numb = Console.ReadLine().Split(' '); //using split to separate numbers string[] numb = Console.ReadLine().Split(' ')
            List<int> list = new List<int>(); //creating new list to find primes  List<int> list = new List<int>()
            for (int i = 0; i < n; i++) // until i is not equal to n do next: (int i = 0; i < n; i++)
            {
                int x = int.Parse(numb[i]); //converting the numbers to integers int x = int.Parse(numb[i])
                int ok = 1; //ok if number is odd
                for (int j = 2; j <= x / 2; j++) // conditions for the primes int j = 2; j <= x / 2; j++
                {
                    if (x % j == 0) 
                    {
                        ok = 0; //even number
                        break;
                    }
                }
                if (ok == 1 && x > 1)   // adding primes to the new list ok == 1 && x > 1
                    list.Add(x);
            }
            Console.WriteLine(list.Count); //numbers of primes writing through list.count
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " "); //prime numbers
            Console.ReadKey();
        }
    }
}
