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
            string rev;
            string text = System.IO.File.ReadAllText(@"C:\Users\vsavg\Desktop\pp2-labs\Lab 1\task1\palindrome.txt");
            char[] ch = text.ToCharArray();

            Array.Reverse(ch);
            rev = new string(ch);

            bool b = text.Equals(rev, StringComparison.OrdinalIgnoreCase);
            if (b == true)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }
            Console.Read();
        }

    }

}
