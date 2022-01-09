using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGameRandomizerNet.Models
{
    internal class SteamLibrary
    {
        public string Path { get; set; }
        public List<string> Games { get; set; }
    }
}
