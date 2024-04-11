using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        List<float> marks = new List<float>();
        Console.WriteLine("Write all the marks of the exams, once you write one (only write the number using a decimal POINT), press enter to write another or space to process the data.");
        string currentLine = "";
        while(true) 
        {
            try
            {
                currentLine += Console.ReadLine();
                marks.Add(float.Parse(currentLine));
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("An error has occured processing the mark. Make sure it is written like '5.45' (with no quotes).");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            Console.WriteLine("The mark was added correctly, write another one or press space to process data.");
            ConsoleKeyInfo currentKey = Console.ReadKey();
            if(currentKey.Key == ConsoleKey.Spacebar)
            {
                break;
            }
            else
            {
                currentLine = currentKey.KeyChar.ToString();
            }
        }
        Console.Clear();
        Console.WriteLine("Marks: ");
        Console.ForegroundColor = ConsoleColor.Green;
        foreach(float mark in marks)
        {
            Console.WriteLine(mark);
        }
        Console.ForegroundColor= ConsoleColor.White;    

        /////////////////////////////////////////////////
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}