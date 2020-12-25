using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_for_CSharp7
{
    class Program
    {
        static void Main(string[] args)
        {
            ////#1: bool = TryParse, Inline Variable
            //while (true)
            //{
            //    Console.Write("What is your age: ");
            //    string ageText = Console.ReadLine();
            //    bool isValidAge = int.TryParse(ageText, out int age);   //inline variable
            //    Console.WriteLine($"your age is { age }. ");
            //    if (age == 99) break;
            //}


            ////#2: Pattern Matching
            //string agreFromConsole = "21";
            //int ageFromDatabase = 84;
            //object ageVal = agreFromConsole;
            ////age is inline variable
            //if (ageVal is int age || (ageVal is string ageText && int.TryParse(ageText, out age))) 
            //{
            //    Console.WriteLine($"your age is { age }. ");
            //}
            //else 
            //{
            //    Console.WriteLine($"We do not know your age");
            //}


            ////#3: Switch
            //Employee emp1 = new Employee { FirstName = "Joe", LastName = "Smith", IsManager = true , YearWorked = 2 };
            //Customer cus1 = new Customer { FirstName = "Sandra", LastName = "Jones", TotalDollarsSpent = 1000.12M };
            //List<Object> people = new List<Object>() { emp1, cus1 };
            //Console.WriteLine("Start Switch: ");
            //foreach (var item in people)
            //{
            //    switch (item)
            //    {
            //        case Employee e when (e.IsManager == false) :
            //            Console.WriteLine($"Employee: { e.FirstName } is a good employee.");
            //            break;
            //        case Employee e when (e.IsManager) :
            //            Console.WriteLine($"Employee: { e.FirstName } runs this company.");
            //            break;
            //        case Customer c when (c.TotalDollarsSpent > 1000) :
            //            Console.WriteLine($"Customer: { c.FirstName } is a loyal customer.");
            //            break;
            //        case Employee c :
            //            Console.WriteLine($"Customer: { c.FirstName } needs to spend more money.");
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //string wait = Console.ReadLine();


            ////#4: Throw in expression
            //Employee emp1 = new Employee { FirstName = "Joe", LastName = "Smith", IsManager = false, YearWorked = 2 };
            //Employee emp2 = new Employee { FirstName = "Sandra", LastName = "Jones", IsManager = false, YearWorked = 28 };
            //List<Employee> people = new List<Employee>() { emp1, emp2 };
            //Console.WriteLine("Throw in expression: ");
            //Employee ceo = people.Where(x => x.IsManager).FirstOrDefault() ?? throw new Exception("There is an error to find manager.");
            //Console.WriteLine($"The ceo is { ceo.FirstName } ");
            //string wait = Console.ReadLine();


            ////#5: Tuples
            //var name = SplitName("Sandra Jones");
            //Console.WriteLine($"The firstname is { name.FirstName } and lastName is { name.LastName } ");
            //string wait = Console.ReadLine();

            ////#5-1: Tuples
            //var myPoint = new Point(3, 4);
            //var (x, y) = myPoint;
            //var (a, _) = myPoint;       // '_' do not care
            //Console.WriteLine($"x is { x }, y is { y }, a is { a } ");
            //string wait = Console.ReadLine();

            //#6: Reference Variable for array
            //int[] numbers = { 1, 2, 3 };
            //ref int refPosition2 = ref numbers[1];
            //var valueSecond = refPosition2;
            //Console.WriteLine($"valueSecond is { valueSecond } ");
            //refPosition2 = 123;
            //Console.WriteLine($"refPosition2 is { refPosition2 } ");
            ////refPosition2 = ref numbers[2];  //failed: override is not allowed
            //Array.Resize(ref numbers, 1);   // element count is only 1
            //Console.WriteLine($"second position value is still { refPosition2 }, even if array size redused to { numbers.Length} ");
            //var numberList = new List<int> { 1, 2, 3 };
            ////ref int second = ref numerList[1];        // not working on List<>
            //int[] moreNumbers = { 10, 20, 30, 40, 30 };
            //ref int refThirty = ref Find(moreNumbers, 30);
            //Console.WriteLine($"refThirty is { refThirty } ");
            //refThirty = 123;
            //Console.WriteLine($"new refThirty value is { refThirty } ");
            //Find(moreNumbers, 123) = 555;       //change index 3 position value to 555
            //Console.WriteLine($"new refThirty value is now { refThirty } ");
            //for (int i = 0; i < moreNumbers.Length; i++)
            //    Console.WriteLine($"moreNumbers is { moreNumbers[i] } ");
            //string wait = Console.ReadLine();

            //#7: underscore
            int a = 123_456_789;        //123456789
            int b = 111______1;         //1111
            var bin = 0b11_10_0100_11;  //915
            Console.WriteLine($"a={ a }, b= { b }, bin = { bin }");
            string wait = Console.ReadLine();


        }

        //#6: Reference Variable for array
        static ref int Find(int[] numbers, int value) 
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == value)
                    return ref numbers[i];
            }
            throw new ArgumentException("not found");
        }


        //#5-1: Tuples
        public class Point
        {
            public int X = 1, Y = 2;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Deconstruct(out int x, out int y)
            {
                x = X;
                y = Y;
            }
        }

        //#5: Tuples
        private static (string FirstName, string LastName) SplitName(string fullName)
        {
            string[] vals = fullName.Split(' ');
            return (vals[0], vals[1]);
        }

        private class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool IsManager { get; set; } = false;
            public int YearWorked { get; set; } = 0;
        }

        private class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal TotalDollarsSpent { get; set; }
        }

    }


}

