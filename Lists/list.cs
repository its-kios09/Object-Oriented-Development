using System;
using System.Collections.Generic;

namespace SimpleListProgram
{
    public class Program
    {
        private static List<double> _values = new List<double>();

        public enum UserOption
        {
            NewValue,
            Sum,
            Print,
            Quit
        }

        public static int ReadInteger(string prompt)
        {
            Console.Write(prompt);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
                Console.Write(prompt);
            }
            return value;
        }

        public static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            double value;
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write(prompt);
            }
            return value;
        }

        public static void AddValueToList()
        {
            double value = ReadDouble("Enter a value: ");
            _values.Add(value);
            Console.WriteLine("Value added to the list.");
        }

        public static void Print()
        {
            Console.WriteLine("Values in the list:");
            foreach (double value in _values)
            {
                Console.WriteLine(value);
            }
        }

        public static void SumValues()
        {
            double sum = 0;
            foreach (double value in _values)
            {
                sum += value;
            }
            Console.WriteLine("Sum of all values: " + sum);
        }

        public static UserOption ReadUserOption()
        {
            Console.WriteLine("Enter 0 to add a value");
            Console.WriteLine("Enter 1 to sum all values");
            Console.WriteLine("Enter 2 to print all values");
            Console.WriteLine("Enter 3 to quit");

            int option = ReadInteger("Option: ");
            return (UserOption)option;
        }

        public static void Main(string[] args)
        {
            UserOption option = UserOption.Quit;

            while (option != UserOption.Quit)
            {
                option = ReadUserOption();

                switch (option)
                {
                    case UserOption.NewValue:
                        AddValueToList();
                        break;
                    case UserOption.Sum:
                        SumValues();
                        break;
                    case UserOption.Print:
                        Print();
                        break;
                    case UserOption.Quit:
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
