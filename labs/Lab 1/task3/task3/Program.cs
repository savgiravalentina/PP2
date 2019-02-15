using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = Convert.ToInt32(Console.ReadLine()); //define the range of array Convert.ToInt32(Console.ReadLine())
            int[] array = new int[length]; //creating an array  int[] array = new int[length]

            string s = Console.ReadLine(); //writing the elements of the array string s = Console.ReadLine()
            string[] arr = s.Split(); //using split to separate elements string[] arr = s.Split()

            //converting that elements into integers to enter them into our array
            //also we can use int.Parse command
            for (int i = 0; i < length; i++)
            {
                array[i] = Convert.ToInt32(arr[i]); // convert to integers elements in array  array[i] = Convert.ToInt32(arr[i])
            }

            //output
            for (int i = 0; i < length; i++) //length euals to array elements number int i = 0; i < length; i++
            {
                Console.Write(array[i] + " " + arr[i] + " "); //array we had first time and arr we prisvoili 
            }
            Console.ReadKey();
        }
    }
}
