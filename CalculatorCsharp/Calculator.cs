using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorCsharp
{
    class Calculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Simple Calculator -----\n\n" 
                + "# You are allowed to use +, -, *, /, %, !, sqrt(), pow() operation variables.\n"
                + "# Please press the Enter key once you complete the expression.\n"
                + "# sqrt() has to be expressed as this example: sqrt(2) which means square root 2.\n"
                + "# pow() has to be expressed as this example: pow(2,4) which means 2 power of 4.\n"
                + "# Please enter 'quit' to close the program\n\n"
                + "Please type in your expression"
                );

            Console.Write("> ");
            string input = Console.ReadLine();
            while (input.ToLower() != "quit")
            {
                Mathematician.Calculate(input.ToLower().Replace(" ",""));
                Console.Write("> ");
                input = Console.ReadLine();
                continue;
            }
        }

        ///////// By Mickoon //////////////
    }
}
