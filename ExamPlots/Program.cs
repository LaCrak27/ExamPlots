using System;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;
using ScottPlot;
using System.Windows.Forms;
using System.IO;
using System.Threading;

public class Program
{
    static List<float> marks = new List<float>();
    static double[] freqArr = new double[10];
    static List<double> marksAccuNorm = new List<double>();

    public static void Main(string[] args)
    {
        Directory.CreateDirectory("./graphs");
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
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press any key to process data.");
        Console.ReadKey();

        for (int i = 0; i<10;i++)
        {
            for (int j = 0; j < marks.Count; j++)
            {
                if ((int)marks[j] == i)
                {
                    freqArr[i]++;
                }
            }
        }


        double accumulated = 0;
        foreach (double mark in freqArr)
        {
            accumulated += mark;
            marksAccuNorm.Add(accumulated / marks.Sum());
        }

        Console.Clear();
        float avg = marks.Average();
        float median = marks.Median();
        float stdv = (float)marks.StandardDeviation();


        Console.WriteLine("Average: " + avg);
        Console.WriteLine("Median: " + median);
        Console.WriteLine("St. Deviation: " + stdv);

        Console.WriteLine("Press any key to generate graphs (they will be saved as well as displayed).");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Average: " + avg);
        Console.WriteLine("Median: " + median);
        Console.WriteLine("St. Deviation: " + stdv);

        ThreadStart barsRef = new ThreadStart(renderBarPlot);
        Thread barsThread = new Thread(barsRef);
        barsThread.Start();

        ThreadStart accuRef = new ThreadStart(renderAccuPlot);
        Thread accuThread = new Thread(accuRef);
        accuThread.Start();

        /////////////////////////////////////////////////
        Console.WriteLine("Press any key to finish execution...");
        Console.ReadKey();
    }

    static void renderBarPlot()
    {
        Plot barsPlot = new();

        double[] xs1 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        double[] ys1 = freqArr;
        barsPlot.Add.Bars(xs1, ys1);

        barsPlot.SavePng("./graphs/bars.png", 400, 300);


        var barsForm = new Form();
        barsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
        barsForm.Name = "Bars plot.";
        barsForm.Size = new Size(420, 340);
        PictureBox barsBox = new PictureBox() { Dock = DockStyle.Fill };
        barsBox.Image = System.Drawing.Image.FromFile("./graphs/bars.png");
        barsForm.Controls.Add(barsBox);

        barsForm.ShowDialog();
    }

    static void renderAccuPlot()
    {

        Plot accumulatedPlot = new();

        for (int i = 0; i < marksAccuNorm.Count - 1; i++)
        {
            var line = accumulatedPlot.Add.Line(i, marksAccuNorm[i], i + 1, marksAccuNorm[i + 1]);
            line.LineWidth = 1;
            line.MarkerSize = 3;
        }

        accumulatedPlot.SavePng("./graphs/line.png", 400, 300);


        var accuForm = new Form();
        accuForm.FormBorderStyle = FormBorderStyle.FixedDialog;
        accuForm.Name = "Line plot.";
        accuForm.Size = new Size(420, 340);
        PictureBox accuBox = new PictureBox() { Dock = DockStyle.Fill };
        accuBox.Image = System.Drawing.Image.FromFile("./graphs/line.png");
        accuForm.Controls.Add(accuBox);

        accuForm.ShowDialog();
    }
}