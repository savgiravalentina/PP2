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
            int n = Convert.ToInt32(Console.ReadLine()); //convert number to integer 

            //creating 2d array
            int[][] array = new int[n][];
            for (int i = 1; i <= n; i++) //first condition
            {
                for (int j = 1; j <= i; j++) //second condition 
                {
                    Console.Write("[*]"); //stars as many as number of i element
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
