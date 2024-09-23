﻿using System;
using System.Collections;

namespace HomeWork_Basic_03_rev1;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Home work 3 ---\n");
        Console.WriteLine("a*x^2 + b*x + c = 0\n");
        while (true)
        {
            try
            {
                int a = InputIntOdd("a");
                if (a == 0)
                {
                    var ex = new WrongFormatException("The value of coefficient a should not be equal to zero");
                    throw ex;
                }
                int b = InputIntOdd("b");
                int c = InputIntOdd("c");
                double[] roots = CalculateSquareRoots(a, b, c);
                if (roots is not null)
                {
                    for (int i = 0; i < roots.Length; i++)
                    {
                        Console.WriteLine($"root {i + 1} = {roots[i]:n2}");
                    }
                }
                break;
            }
            catch (WrongFormatException ex)
            {
                FormatData(ex.Message, Severity.Error, ex.Data);
            }
            catch (Exception ex)
            {
                FormatData(ex.Message, Severity.Warning, ex.Data);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Implementation of data entry
    private enum TypeCode { stringType, longType, intType }
    static TypeCode ParseCode(string? stringParse)
    {
        TypeCode typeCode = TypeCode.stringType;
        if (long.TryParse(stringParse, out long l))
        {
            typeCode = TypeCode.longType;
            if (l >= int.MinValue && l <= int.MaxValue)
            {
                typeCode = TypeCode.intType;
            }
        }
        return typeCode;
    }

    static int InputIntOdd(string oddName)
    {
        string? oddString;
        int odd;
        do
        {
            Console.Write($"Input {oddName}: ");
            oddString = Console.ReadLine();
            try
            {
                if (ParseCode(oddString) == TypeCode.stringType)
                {
                    var wf = new WrongFormatException($"!!!Wrong format odd");
                    wf.Data.Add(oddName, oddString);
                    throw wf;
                }
                else if (ParseCode(oddString) == TypeCode.longType)
                {
                    var ar = new ArgumentOutOfRangeIntTypeException($"!!!Argument out of range: {oddString}" +
                        $"\nRange for int type from {int.MinValue} to {int.MaxValue}");
                    throw ar;
                }
            }
            catch (WrongFormatException ex)
            {
                FormatData(ex.Message, Severity.Error, ex.Data);
            }
            catch (ArgumentOutOfRangeIntTypeException ex)
            {
                FormatData(ex.Message, Severity.Attention, ex.Data);
            }
        } while (!int.TryParse(oddString, out odd));
        return odd;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // Implementation of leveling calculation
    static double[] CalculateSquareRoots(int a, int b, int c)
    {
        try
        {
            Console.WriteLine("\nCalculate");
            Console.WriteLine("--------------------------------------------------");

            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            Console.WriteLine($"Discriminant = {discriminant}");
            double[] root;

            if (discriminant < 0)
            {
                var ex = new DiscriminantException("No real values found");
                ex.Data.Add("a", a);
                ex.Data.Add("b", b);
                ex.Data.Add("c", c);
                throw ex;
            }
            else
            {
                var x1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                var x2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                root = x1 == x2 ? [x1] : [x1, x2];
            }
            return root;
        }
        catch (DiscriminantException)
        {
            throw;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Exception Handling
    enum Severity { Attention, Warning, Error };
    private static void FormatData(string message, Severity severity, IDictionary data)
    {
        if (severity == Severity.Error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            MessageBlock(message);
            PrintIDictionary(data);
            Console.WriteLine("--------------------------------------------------");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
        else if (severity == Severity.Warning)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            MessageBlock(message);
            PrintIDictionary(data);
            Console.WriteLine("--------------------------------------------------");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
        else if (severity == Severity.Attention)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            MessageBlock(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }

    static void PrintIDictionary(IDictionary data)
    {
        foreach (DictionaryEntry kvp in data)
            Console.WriteLine($"{kvp.Key} = {kvp.Value}");
    }

    static void MessageBlock(string message)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.Write(message);
        Console.WriteLine("\n--------------------------------------------------");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
