using SteamGameRandomizer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGameRandomizerNet
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLocator locator = new GameLocator();
            var libVdfPath = @"C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf";
            var shuffleAgain = false;

            try
            {
                Console.WriteLine("Retrieving list of games...");

                var gamesList = locator.GetAllSteamGamesFromLibraryVdf(libVdfPath);
                if (gamesList != null && gamesList.Any())
                {
                    do
                    {
                        GenerateRandomGameSelection(gamesList);

                        var input = Console.ReadLine();

                        if (input.ToLower() == "y")
                        {
                            shuffleAgain = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"Done, son");
                            shuffleAgain = false;
                        }
                    } while (shuffleAgain);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }

        static void GenerateRandomGameSelection(List<string> gamesList)
        {

            Console.WriteLine($"\n\nRandom Game Selection: {gamesList[new Random().Next(gamesList.Count - 1)]}");

            Console.WriteLine("\nWould you like to shuffle again? ");
        }
    }
}
