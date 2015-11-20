using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace Notes {
    /// <summary>
    /// Manages Notes
    /// </summary>
    public class PNotes : Plugin.Plugin {
        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.notes.notes";
            }
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.Version"/>
        /// </summary>
        public override int Version {
            get {
                return 1;
            }
        }


    }
}
