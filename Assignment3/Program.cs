using System;
using System.Collections;



class Program
{
    // main
    static void Main()
    {
        // create dictionary for prime ministers - key and value 
        var pMinisters = new Dictionary<int, string>()
        {
            {1998, "Atal Bihari Vajpayee"},
            {2014, "Narendra Modi"},
            {2004, "Manmohan Singh"}
        };
        Console.WriteLine();

        // Find the Prime minister of 2004.

        Console.WriteLine($"Prime minister of 2004: {pMinisters[2004]}");
        Console.WriteLine();

        // Add new prime minister
        // year and Name 
        Console.Write("Enter the year : ");
        var year = Convert.ToInt32((Console.ReadLine()));
        Console.WriteLine();
        Console.Write("Enter the name of prime minister: ");
        var name = Console.ReadLine();
        pMinisters.Add(year, name);
        Console.WriteLine();

        // Sort the dictionary by year 

       // sorted by year

        var pm = from pair in pMinisters
                    orderby pair.Key ascending
                    select pair;


        //  Print dictionary

        foreach (KeyValuePair <int, string> pair in pm)
        {
            Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
        }
      
    }
}

