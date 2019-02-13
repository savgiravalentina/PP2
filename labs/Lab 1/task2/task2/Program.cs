using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Student
    {
        private string name; //private to make sure than the other part of code will not disturb this conditions
        private string id;
        private int year;

        public Student(string name, string id, int year)
        {
            this.name = name; //next time when we will write name it will be приравнен to this name
            this.id = id; //the same as name
            this.year = year + 1; //adding one year because task told so
        }
        public string result
        {
            get
            {
                return name + id + year;  //getting access to the private data
            }
        }
        public static void Main()
        {
            Student s1 = new Student("Valentina", " 18BD987654 ", 2);
            Console.WriteLine(s1.result);
            Console.ReadKey();
        }
    }
}
