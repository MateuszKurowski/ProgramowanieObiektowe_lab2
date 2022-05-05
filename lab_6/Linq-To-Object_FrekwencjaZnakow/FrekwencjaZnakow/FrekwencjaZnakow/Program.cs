using System;
using System.Collections.Generic;
using System.Linq;

namespace FrekwencjaZnakow
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var cases = int.Parse(Console.ReadLine());
            //var cases = 1;
            string[] senntencesTab = new string[cases];
            string[] queriesTab = new string[cases];
            for (int i = 0; i < cases; i++)
            {
                //senntencesTab[i] = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."
                senntencesTab[i] = Console.ReadLine()
                                .Replace("!", "")
                                .Replace("?", "")
                                .Replace(",", "")
                                .Replace(".", "")
                                .Replace("-", "")
                                .Replace(":", "")
                                .Replace(";", "")
                                .Replace("(", "")
                                .Replace(")", "")
                                .Replace("%", "")
                                .Replace("/", "")
                                .Replace("1", "")
                                .Replace("2", "")
                                .Replace("3", "")
                                .Replace("4", "")
                                .Replace("5", "")
                                .Replace("6", "")
                                .Replace("7", "")
                                .Replace("8", "")
                                .Replace("9", "")
                                .Replace("0", "")
                                .Replace(" ", "")
                                .Replace("  ", "")
                                .Replace("   ", "")
                                .Replace("\'", "")
                                .Replace("\"", "")
                                .Replace("\\", "")
                                .ToLower();
                //queriesTab[i] = "first 4 byfreq asc byletter desc"
                queriesTab[i] = Console.ReadLine()
                                .ToLower();
            }
            // 0: Pierwsze / ostatnie
            // 1: Liczba wyrazów
            // 2: Częstość wystąpień / ze względu na znaki, leksykograficznie
            // 3: Sortowanie
            // -- W przypdaku równości --
            // 4: Częstość wystąpień / ze względu na znaki, leksykograficznie
            // 5: Sortowanie

            for (int i = 0; i < cases; i++)
            {
                var query = queriesTab[i].Split(' ');
                var countOfWords = int.Parse(query[1]);
                var lettersList = new List<char>();
                lettersList.AddRange(senntencesTab[i]);
                var resultList = lettersList.GroupBy(x => x).Select(y => new { letter = y.Key, amount = y.Count() }).ToList();

                if (query.Length < 5)
                    switch (query[2])
                    {
                        case "byfreq":
                            {
                                if (query[3] == "asc")
                                    resultList = resultList.OrderBy(x => x.amount).ToList();
                                else if (query[3] == "desc")
                                    resultList = resultList.OrderByDescending(x => x.amount).ToList();
                            }
                            break;
                        case "byletter":
                            {
                                if (query[3] == "asc")
                                    resultList = resultList.OrderBy(x => x.letter).ToList();
                                else if (query[3] == "desc")
                                    resultList = resultList.OrderByDescending(x => x.letter).ToList();
                            }
                            break;
                    }
                else if (query.Length >= 5)
                    switch (query[2])
                    {
                        case "byfreq":
                            {
                                if (query[3] == "asc" && query[5] == "asc")
                                    resultList = resultList.OrderBy(x => x.amount).ThenBy(y => y.letter).ToList();
                                if (query[3] == "desc" && query[5] == "desc")
                                    resultList = resultList.OrderByDescending(x => x.amount).ThenByDescending(y => y.letter).ToList();
                                if (query[3] == "asc" && query[5] == "desc")
                                    resultList = resultList.OrderBy(x => x.amount).ThenByDescending(y => y.letter).ToList();
                                if (query[3] == "desc" && query[5] == "asc")
                                    resultList = resultList.OrderByDescending(x => x.amount).ThenBy(y => y.letter).ToList();
                            }
                            break;
                        case "byletter":
                            {
                                if (query[3] == "asc" && query[5] == "asc")
                                    resultList = resultList.OrderBy(x => x.letter).ThenBy(y => y.amount).ToList();
                                if (query[3] == "desc" && query[5] == "desc")
                                    resultList = resultList.OrderByDescending(x => x.letter).ThenByDescending(y => y.amount).ToList();
                                if (query[3] == "asc" && query[5] == "desc")
                                    resultList = resultList.OrderBy(x => x.letter).ThenByDescending(y => y.amount).ToList();
                                if (query[3] == "desc" && query[5] == "asc")
                                    resultList = resultList.OrderByDescending(x => x.letter).ThenBy(y => y.amount).ToList();
                            }
                            break;
                    }

                var results = resultList.Take(resultList.Count);
                switch(query[0])
                {
                    case "first":
                        {
                            results = resultList.Take(countOfWords);
                            
                        }break;
                    case "last":
                        {
                            results = resultList.Skip(resultList.Count - countOfWords);
                        }
                        break;
                }

                foreach (var result in results)
                {
                    Console.WriteLine($"{result.letter} {result.amount}");
                }

                if (cases > i)
                    Console.WriteLine();
            }
        }
    }
}