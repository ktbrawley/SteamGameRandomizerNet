using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGameRandomizer.Services
{
    internal interface IGameLocator
    {
        /// <summary>
        /// Retrieves a list of games in your stream libraries using the libraryfolders.vdf file in local steam installation dir
        /// </summary>
        /// <param name="libVdfFilePath"></param>
        /// <returns></returns>
        List<string> GetAllSteamGamesFromLibraryVdf(string libVdfFilePath);
    }
}