using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorCsharp
{
    class Mathematician
    {
        public static string input = "";
        public static int pos = -1;

        /// <summary>
        /// The program uses loops and recursions for calculation.
        /// These makes it possible to calculate an expression with multiple operations and numbers.
        /// 
        /// By Mickoon
        /// </summary>
        /// <param name="userInput"></param>

        public static void Calculate(string userInput)
        {
            input = userInput;

            try
            {
                double result = LastOrder();
                Console.WriteLine("= " + result + "\n");
                pos = -1;   // Reset pos value
            }  
            catch (FormatException)
            {
                Console.WriteLine("Wrong Expression!\n");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divide by Zero occurs!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: " + ex + "\n");
            }
        }

        static double LastOrder()
        {
            double left = SecondOrder();
            pos++;

            while (pos < input.Length)
            {
                switch (input[pos])
                {
                    case '+':
                        left += SecondOrder();
                        pos++;
                        break;
                    case '-':
                        left -= SecondOrder();
                        pos++;
                        break;
                    default:
                        pos--;
                        return left;
                }
            }
            return left;
        }

        static double SecondOrder()
        {
            double left = MainOrder();
            pos++;

            while (pos < input.Length)
            {
                switch (input[pos])
                {
                    case '*':
                        left *= MainOrder();
                        pos++;
                        break;
                    case '/':
                        double d = MainOrder();
                        if (d == 0) throw new DivideByZeroException();
                        left /= d;
                        pos++;
                        break;
                    case '%':
                        d = MainOrder();
                        if (d == 0) throw new DivideByZeroException();
                        left %= d;
                        pos++;
                        break;
                    default:
                        pos--;
                        return left;
                }
            }
            return left;
        }

        static double MainOrder()
        {
            pos++;

            while (pos < input.Length)
            {
				switch(input[pos].ToString())
                {
				    case "(":
						double num = LastOrder();
						pos++;
						if(input[pos] != ')') throw new InvalidCastException("')' expected", new InvalidCastException());
                        return num;
				    
                    case "s": 
                        if(input[pos+1] == 'q' && input[pos+2] == 'r' && input[pos+3] == 't')
                        {
                            pos += 4;
                            num = LastOrder();
                            if (num < 0) throw new InvalidCastException("Can't be negative", new InvalidCastException());
							else 
                            {
                                pos++;
                                if (input[pos] != ')') throw new InvalidCastException("')' expected", new InvalidCastException());
								return Math.Sqrt(num);
							}
                        }
                        else
                            throw new FormatException();
				    
				    case "p":
                        if (input[pos+1] == 'o' && input[pos+2] == 'w')
                        {
                            pos += 3;
                            if (input[pos] != '(') throw new InvalidCastException("'(' expected", new InvalidCastException());
							num = LastOrder();
							pos++;
                            if (input[pos] != ',') throw new InvalidCastException("',' expected", new InvalidCastException());
							double i = LastOrder();
							pos++;
                            if (input[pos] != ')') throw new InvalidCastException("')' expected", new InvalidCastException());
							return Math.Pow(num, i);
                        }
                        else
                            throw new FormatException();
				    
				    case "0":case "1":case "2":case "3":case "4":case "5":case "6":case "7":case "8":case "9":
                        double dbl;
                        string number = input[pos].ToString();

                        if (pos + 1 < input.Length)
                        {
                            while (double.TryParse(input[pos + 1].ToString(), out dbl))
                            {
                                pos++;
                                number += input[pos].ToString();
                                if (pos+1 >= input.Length) break;
                            }
                        }

						num = Convert.ToDouble(number);
                        if (pos + 1 < input.Length)
                        {
                            if (input[pos + 1] == '!') // check '!' for factorial
                            {
                                pos++;
                                return factorial(Convert.ToInt32(number));
                            }
                        }
						return num;
				    
                    case "+":
                        return MainOrder();
                    case "-":
                        return -MainOrder();
				    default:
                        throw new InvalidCastException("Wrong Expression!", new InvalidCastException());
				}
		    }
            return 0;
        }

        static double factorial(int num)
        {
            if (num < 0) throw new InvalidCastException("Only non-negative integers are allowed", new InvalidCastException());
            if (num < 2)
                return 1;
            else
                return (double)num * factorial(num - 1);
        }
    }
}
