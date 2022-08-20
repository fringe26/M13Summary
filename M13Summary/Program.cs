// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Net.Http;
using System.Linq;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Text;

internal class Program
{


     public static void Main(string[] args)
     {
        Console.WriteLine("List Added Time :");
        CheckOptimization(CheckList);
        Console.WriteLine();

        Console.WriteLine("Linked List Added Time :");
        CheckOptimization(CheckLinkedList);
        Console.WriteLine();


    }


    public static void CheckList()
    {
        List<string> list = new List<string>();

        using (var mappedFile1 = MemoryMappedFile.CreateFromFile("Text1.txt"))
        {
            using (Stream mmStream = mappedFile1.CreateViewStream())
            {
                using (StreamReader sr = new StreamReader(mmStream, ASCIIEncoding.ASCII))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var lineWords = line.Split(' ').ToList<string>();
                        lineWords.ForEach(list.Add);
                        
                    }
                }
            }
        }

        //list.ForEach(Console.WriteLine);
    }

    public static void CheckLinkedList()
    {
        LinkedList<string> list = new LinkedList<string>();

        using (var mappedFile1 = MemoryMappedFile.CreateFromFile("Text1.txt"))
        {
            using (Stream mmStream = mappedFile1.CreateViewStream())
            {
                using (StreamReader sr = new StreamReader(mmStream, ASCIIEncoding.ASCII))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var lineWords = line.Split(' ').ToList<string>();
                        foreach(string word in lineWords)
                        {
                            list.AddLast(word);
                        }
                    }
                }
            }
        }

        //list.ToList().ForEach(Console.WriteLine);
    }



    public static void CheckOptimization(Action action)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        action?.Invoke(); // my method
        stopWatch.Stop();
       

        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);
    }
    






    
}