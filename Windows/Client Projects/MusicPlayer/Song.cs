using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer {
    class Song {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public Song(int id, string name) {
            ID = id;
            Name = name;
        }
    }
}
