using System;
using System.Collections.Generic;

namespace CSharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

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
            string wait = Console.ReadLine();
        }
    }
}
