using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace AphaFX.WordCounter
{
    class Program
    {
        /// <summary>
        /// Date.......: 2020-07-23 
        /// Author.....: Anderson Andrade (andrade@mail.uk)
        /// Project....: ATFX - Word Counter
        /// </summary>
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Display title as the C# word counter app
            Console.WriteLine("Alpha FX - Word Counter\r");
            Console.WriteLine("------------------------\n");

            // Ask the user to continue
            Console.WriteLine("Press any key to continue...");            

            string url = "https://archive.org/stream/LordOfTheRingsApocalypticProphecies/Lord%20of%20the%20Rings%20Apocalyptic%20Prophecies_djvu.txt";
            string result = null;

            WebClient client = new WebClient();
            result = client.DownloadString(url);

            Dictionary<string, int> stats = new Dictionary<string, int>();

            char[] chars = { ' ', '.', ',', ';', ':', '?', '\n', '\r' };

            // split words
            string[] words = result.Split(chars);

            // to count words having more than 2 characters
            int minWordLength = 2;

            // determine top words list number (along with the number of occurrences).
            int topWordList = 10;

            int i = 0;
            // iterate over the word collection to count occurrences
            foreach (string word in words)
            {
                i += 1;
                string w = word.Trim().ToLower();
                if (w.Length > minWordLength)
                {
                    if (!stats.ContainsKey(w))
                    {
                        // add new word to collection
                        stats.Add(w, 1);
                    }
                    else
                    {
                        // update word occurrence count
                        stats[w] += 1;
                    }
                }

                int percent = (100 * i / words.Count() );
                Console.Write("\r{0}% complete", percent);
                
            }

            // order the list by word count
            var orderedStats = stats.OrderByDescending(x => x.Value);
            
            Console.Clear();
            Console.WriteLine("Alpha FX - Word Counter");
            Console.WriteLine(".------------------------\n");

            // print total word count
            Console.WriteLine(".------------------------.");
            Console.WriteLine("\r| Total word count: {0} |", stats.Count);
            Console.WriteLine(".------------------------.\n");

            // states the number of top words to be show
            Console.WriteLine("\r Top {0} word list: ", topWordList);

            var col1 = "Word".PadLeft(5).PadRight(5);
            var col2 = "Occurrence".PadLeft(5).PadRight(5);

            Console.WriteLine("----------------------");
            Console.WriteLine("| {0} | {1} |", col1, col2);
            Console.WriteLine("----------------------");

            // print occurrence of each top 10 word 
            foreach (var pair in orderedStats.Take(topWordList))
            {
                var s1 = pair.Key.PadLeft(5).PadRight(5);
                var s2 = pair.Value.ToString().PadLeft(10).PadRight(5);

                Console.WriteLine("| {0} | {1} |", s1, s2);

            }

            Console.WriteLine("----------------------\n");
            Console.ReadLine();
        }
       
    }
}
