using System;

public class Program
{
// <<<<<<<<<<<<<<< prompts in integer >>>>>>>>>>>>>>>>   
    public static int ReadInteger(string prompt)
    {
        Console.Write(prompt);
        while(true)
        {
            try
            {
                return Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please enter a valid integer: ");
            }
        }

    }

 // <<<<<<<<<<<<<<< prompts in double >>>>>>>>>>>>>>>>   
    public static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        while (true)
        {
            try
            {
                return double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please enter a valid double");
            }
        }
    }
    public static void Main()
    {
        int numberOfValues = ReadInteger("Enter the number of values: ");
        double[] values = new double[numberOfValues];

        for (int i = 0; i < numberOfValues; i++)
        {
            values[i] = ReadDouble($"Enter the {i + 1}st value: ");
        }

        Console.WriteLine("Values entered:");
        for (int i = 0; i < numberOfValues; i++)
        {
            Console.WriteLine(values[i]);
        }

        double sum = 0;
        for (int i = 0; i < numberOfValues; i++)
        {
            sum += values[i];
        }

        Console.WriteLine($"Sum of values: {sum}");
    }
}