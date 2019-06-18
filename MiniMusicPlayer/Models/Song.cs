using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMusicPlayer.Models
{
    public class Song
    {
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }

        public string Path { get; set; }

        public uint Track { get; set; }
        public uint TrackCount { get; set; }
    }
}
