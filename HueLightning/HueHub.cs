using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q42.HueApi;
using Q42.HueApi.NET;
using Plugin;

namespace HueLightning {
    internal static class HueHub {          
        private static int _debugChannel;

        public static void Init() {
            _debugChannel = Debug.AddChannel("com.projectgame.huelightning.huehub");                                  
        }
    }
}
