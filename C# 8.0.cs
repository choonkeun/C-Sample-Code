using System;
using System.Collections.Generic;
using System.IO;

namespace CSharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //reverse index
            var simArray = new string[]
            {
                "one",         // 0      ^4: reverse index
                "two",         // 1      ^3
                "three",       // 2      ^2
                "four",        // 3      ^1
            };
            Console.WriteLine("fetch using caret operator" + simArray[^1]);     //output as "four"
            Console.WriteLine("fetch using range operator" + simArray[..]);     //output as "one" to "four"
            Console.WriteLine("fetch using range operator" + simArray[..3]);    //output as "one" to "three"
            Console.WriteLine("fetch using range operator" + simArray[2..]);    //output as "two" to "four"

            //[..] indicates, fetch all items of list
            //[..3] indicates.fetch all items up to index 3
            //[1..] indicates, fetch all items starting from index 1(index starts from 1)
           

            //#1 switch 
            //1. no 'case', 'break'(or return) statement
            //2. The colon(:) is replaced with the 'Goes to'(=>) 
            //3. "default" is replaced with the "_"(underscore)
            var operation = 2;
            var result = operation switch
            {
                1 => "Case 1",
                2 => "Case 2",
                3 => "Case 3",
                4 => "Case 4",
                _ => "No case availabe"
            };
            Console.WriteLine(result);

            //example1    
            var (a, b, option) = (10, 5, "+");
            var example1 = option switch
            {
                "+" => a + b,
                "-" => a - b,
                _ => a * b
            };
            Console.WriteLine("Example 1 : " + example1);

            //example2    
            var value = 25;
            int example2 = value switch
            {
                _ when value > 10 => 0,
                _ when value <= 10 => 1
            };
            Console.WriteLine("Example 2 : " + example2);

            //example3    
            var (key, defaultValue) = ("CSharp8", "defaultValue");
            var dic = new Dictionary<string, string>();
            dic.Add("CSharp8", "C# 8.0");
            var example3 = dic.TryGetValue(key, out string val) switch
            {
                false => defaultValue,
                //true  => val,
                _ => val
            };
            Console.WriteLine($"Example 3 : { example3 }, val: { val }");


            //Using Declarations
            try
            {
                using var sr = new StreamReader("test.txt");
                Console.WriteLine(sr.ReadToEnd());
                // file is disposed here
            }
            catch (IOException e)
            {
                Console.WriteLine("Error while loading the file.");
            }

            string wait = Console.ReadLine();
        }

    }


}
