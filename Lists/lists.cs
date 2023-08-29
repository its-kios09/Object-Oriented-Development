using System;
using System.Collections.Generic;

namespace SimpleProgram
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

        public static void Main()
        {
            UserOption option;

            do
            {
                option = ReadUserOption();

                switch (option)
                {
                    case UserOption.NewValue:
                        AddValueToList();
                        break;
                    case UserOption.Sum:
                        CalculateSum();
                        break;
                    case UserOption.Print:
                        PrintValues();
                        break;
                    case UserOption.Quit:
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (option != UserOption.Quit);
        }

        public static UserOption ReadUserOption()
        {
            Console.WriteLine("Enter 0 to add a value");
            Console.WriteLine("Enter 1 to sum all values");
            Console.WriteLine("Enter 2 to print all values");
            Console.WriteLine("Enter 3 to quit");

            int option = 3;
            Int32.TryParse(Console.ReadLine(), out option);

            return (UserOption)option;
        }

        public static void AddValueToList()
        {
            double value = ReadDouble("Enter a value to add: ");
            _values.Add(value);
            Console.WriteLine("Value added successfully!");
        }

        public static void CalculateSum()
        {
            double sum = 0;

            foreach (double value in _values)
            {
                sum += value;
            }

            Console.WriteLine("Sum of all values: " + sum);
        }

        public static void PrintValues()
        {
            Console.WriteLine("Values in the list:");

            foreach (double value in _values)
            {
                Console.WriteLine(value);
            }
        }

        public static double ReadDouble(string prompt)
        {
            double value;
            bool isValid;

            do
            {
                Console.Write(prompt);
                isValid = double.TryParse(Console.ReadLine(), out value);

                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

            } while (!isValid);

            return value;
        }
    }
}
