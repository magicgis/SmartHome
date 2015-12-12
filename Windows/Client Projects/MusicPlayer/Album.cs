using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer {
    class Album {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Song> Songs { get; private set; }

        public Album(int id, string name) {
            ID = id;
            Name = name;
            Songs = new List<Song>();
        }

        public void Sort() {
            Songs = Songs.OrderBy(o => o.Name).ToList();
        }
    }
}
