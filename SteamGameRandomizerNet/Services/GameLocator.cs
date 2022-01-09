using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Gameloop.Vdf.Linq;
using Gameloop.Vdf;
using Newtonsoft.Json.Linq;
using Gameloop.Vdf.JsonConverter;
using SteamGameRandomizerNet.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace SteamGameRandomizer.Services
{
    public class GameLocator : IGameLocator
    {
        public List<string> GetAllSteamGamesFromLibraryVdf(string libVdfFilePath)
        {
            List<string> gamesList = new List<string>();
            try
            {
                Console.WriteLine("Converting steam vdf file contents to json");


                JProperty vdfJson;
                var successful = ConvertVdfToJson(libVdfFilePath, out vdfJson);

                if (!successful)
                {
                    Console.WriteLine("Error converting steam vdf to json");
                    return gamesList;
                }

                var gamesRetrieved = GetListOfSteamGames(vdfJson, out gamesList);

                if (!gamesRetrieved)
                {
                    Console.WriteLine("Error finding any games on disk");
                    return gamesList;
                }

                return gamesList;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return gamesList;
        }

        private bool ConvertVdfToJson(string libVdfFilePath, out JProperty vdfProp)
        {
            vdfProp = null;

            if (!File.Exists(libVdfFilePath))
            {
                Console.WriteLine($"Could not find file: {libVdfFilePath}");
                return false;
            }

             vdfProp = VdfConvert.Deserialize(File.ReadAllText(libVdfFilePath)).ToJson();
            
            return true;
        }

        private bool GetListOfSteamGames(JProperty vdfProp, out List<string> steamGames)
        {
            steamGames = new List<string>();
            var libList = vdfProp.Value.Children().Skip(1).Values().ToList();
            var steamLibs = libList.Select(x => x.Value<JObject>().ToObject<SteamLibrary>()).ToList();

            // Get games list for each steam lib
            foreach (var item in steamLibs)
            {
                var path = $"{ item.Path }\\steamapps\\common\\";
                if (Directory.Exists(path))
                {
                    var games = Directory.GetDirectories($"{path}").Select(x => x.Replace(path, ""));

                    if (!games.Any())
                    {
                        continue;
                    }

                    steamGames.AddRange(games);
                }

            }

            return true;

        }
    }
}