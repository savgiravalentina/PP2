using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine()); //convert number to integer  int n = Convert.ToInt32(Console.ReadLine())

            
            int[][] array = new int[n][]; //creating 2d array int[][] array = new int[n][]
            for (int i = 1; i <= n; i++) //first condition int i = 1; i <= n; i++
            {
                for (int j = 1; j <= i; j++) //second condition (int j = 1; j <= i; j++)
                {
                    Console.Write("[*]"); //stars as many as number of i element
                }
                Console.WriteLine(""); //to dobit' number of 2d array
            }
            Console.ReadKey();
        }
    }
}
