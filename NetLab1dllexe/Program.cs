using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myList;

namespace NetLab1dllexe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // using empty constructor
            myList<int> mylist = new myList<int>();

            // using constructor that gets another collection(by using for each in it)
            List<int> list = new List<int>() { 1,2,3 };
            myList<int> mylist2 = new myList<int>(list);
            Console.WriteLine("list:");
            Printlist(list);
            Console.WriteLine("mylist: ");
            Printlist(mylist2);

            Console.WriteLine();

            // add delegate to myList class. For Notify event;
            mylist.Notify += PrintMessage;
            mylist2.Notify += PrintMessage;


            // add method
            mylist.Add(1);
            Console.WriteLine("mylist: ");
            Printlist(mylist);

            // clear method
            mylist2.Clear();
            Console.WriteLine("mylist2: ");
            Printlist(mylist2);

            // contains method
            Console.WriteLine(mylist.Contains(1));

            Console.WriteLine();

            // copyTo method
            int[] array = new int[5];
            mylist = new myList<int> { 1, 2, 3 };
            mylist.Notify += PrintMessage;
            mylist.CopyTo(array, 1);
            Console.WriteLine("array: ");
            Printlist(array);

            // indexOf method
            mylist.IndexOf(2);
            Console.WriteLine("mylist: ");
            Printlist(mylist);

            // insert method
            mylist.Insert(0, 99);
            Console.WriteLine("mylist: ");
            Printlist(mylist);

            // remove method
            mylist.Remove(99);
            Console.WriteLine("mylist: ");
            Printlist(mylist);

            // removeAt method
            mylist.RemoveAt(0);
            Console.WriteLine("mylist: ");
            Printlist(mylist);

            // count property
            Console.WriteLine(mylist.Count);

            // isreadOnly property
            Console.WriteLine(mylist.IsReadOnly);

            // index
            Console.WriteLine(mylist[mylist.Count - 1]);


            Console.ReadLine();

            void PrintMessage(string message)
            {
                Console.WriteLine(message);
            }

            void Printlist(ICollection<int> col)
            {
                foreach (int item in col)
                    Console.Write(item + "; ");

                Console.WriteLine("\n"); 
            }
        }
    }
}
