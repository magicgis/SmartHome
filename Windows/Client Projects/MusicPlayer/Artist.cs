using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer {
    class Artist {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Album> Albums { get; private set; }

        public Artist(int id, string name) {
            ID = id;
            Name = name;
            Albums = new List<Album>();
        }

        public void Sort() {
            Albums = Albums.OrderBy(o => o.Name).ToList();

            foreach (Album album in Albums)
                album.Sort();
        }
    }
}
