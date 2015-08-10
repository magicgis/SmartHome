using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Plugin;

namespace HueLightning {
    internal static class HueHub {          
        private static int _debugChannel;

        public static void Init() {
            _debugChannel = Debug.AddChannel("com.projectgame.huelightning.huehub");                                  
        }
    }
}
