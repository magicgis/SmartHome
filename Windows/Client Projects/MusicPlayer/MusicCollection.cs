using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer {
    class MusicCollection {
        public List<Artist> Artists { get; private set; }

        public MusicCollection() {
            Artists = new List<Artist>();
        }

        public void Sort() {
            Artists = Artists.OrderBy(o => o.Name).ToList();

            foreach (Artist artist in Artists)
                artist.Sort();
        }
    }
}
