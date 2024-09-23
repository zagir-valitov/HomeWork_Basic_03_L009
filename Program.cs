using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.X86;

namespace HomeWork_Basic_03;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Home work 3 ---\n");

        Console.WriteLine("a*x^2 + b*x + c = 0\n");
        int errorCount = 0;
        do
        {
            try
            {                
                int oddA, oddB, oddC;            
                errorCount = 0;

                string? a = Input("Input a: ");
                string? b = Input("Input b: ");
                string? c = Input("Input c: ");

                string errorText = "Invalid parameter format ";

                if (!int.TryParse(a, out oddA))
                {
                    errorText += " " + "(a)";
                    errorCount++;
                }
                if (!int.TryParse(b, out oddB))
                {
                    errorText += " " + "(b)";
                    errorCount++;
                }
                if (!int.TryParse(c, out oddC))
                {
                    errorText += " " + "(c)";
                    errorCount++;
                }
                if (errorCount != 0)
                {
                    var ex = new Exception(errorText);
                    ex.Data.Add("a", a);
                    ex.Data.Add("b", b);
                    ex.Data.Add("c", c);
                    throw ex;
                } 
                
                double[] roots = CalculateSquareRoots(oddA, oddB, oddC);
                if (roots is not null)
                {
                    for (int i = 0; i < roots.Length; i++)
                    {
                        Console.WriteLine($"root {i + 1} = {roots[i]}");
                    }
                }
            }
            catch (DiscriminantException ex)
            {
                FormatData(ex.Message, Severity.Error, ex.Data);
            }
            catch (Exception ex)
            {
                FormatData(ex.Message, Severity.Error, ex.Data);
            }

        } while (errorCount != 0);
    }

    class DiscriminantException : Exception
    {
        public DiscriminantException(string message)
            : base(message) { }
    }

    static double[] CalculateSquareRoots(int a, int b, int c)
    {        
        try
        {
            Console.WriteLine("Calculate");

            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            Console.WriteLine($"Discriminant = {discriminant}");
            double[] root;
            
            if (discriminant < 0)
            {
                var ex =  new DiscriminantException("No real values found");
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
        catch(DiscriminantException ex)
        {
            FormatData(ex.Message, Severity.Warning, ex.Data);
            throw;
            //return null;
        }               
    }

    private static void FormatData(string message, Severity severity, IDictionary data)
    {
        if (severity == Severity.Error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            MessageBlock(message);
            Console.WriteLine($"a = {data["a"]}");
            Console.WriteLine($"b = {data["b"]}");
            Console.WriteLine($"c = {data["c"]}");
            Console.WriteLine("--------------------------------------------------\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else if (severity == Severity.Warning)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            MessageBlock (message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    static void MessageBlock(string message)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.Write(message);
        Console.WriteLine("\n--------------------------------------------------");
    }

    enum Severity { Warning, Error };


    private static string? Input(string userText)
    {
        Console.Write(userText);
        return Console.ReadLine();
    }    
    
}
