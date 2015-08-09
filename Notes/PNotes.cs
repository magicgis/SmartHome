using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace Notes {
    public class PNotes : Plugin.Plugin {
        public override string Name {
            get {
                return "com.projectgame.notes.notes";
            }
        }

        public override int Version {
            get {
                return 1;
            }
        }


    }
}
