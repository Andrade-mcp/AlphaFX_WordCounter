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
        /// Project....: Alpha - Word Counter
        /// </summary>
        static void Main(string[] args)
        {
            // Display title as the C# word counter app
            Console.WriteLine("Alpha FX - Word Counter\n");
            
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
            
            // print total word count
            Console.WriteLine("\rTotal number of words: {0} \n", stats.Count);

            // states the number of top words to be show
            Console.WriteLine("\rTop {0} word list:\n", topWordList);

            Console.WriteLine("[Word]\t\t[Count]\n");

            // print occurrence of each top 10 word 
            foreach (var pair in orderedStats.Take(topWordList))
            {
                Console.WriteLine(" {0} \t\t {1} ", pair.Key, pair.Value);
            }

            Console.ReadLine();
            Console.ReadLine();
        }
       
    }
}
