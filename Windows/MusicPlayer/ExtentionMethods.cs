using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using IXP;
using IXPCommunication;

namespace MusicPlayer {
    internal static class ExtentionMethods {
        public static IEnumerable<int> IndexesOf(this string haystack, string needle) {
            int lastIndex = 0;
            while (true) {
                int index = haystack.IndexOf(needle, lastIndex);
                if (index == -1) {
                    yield break;
                }
                yield return index;
                lastIndex = index + needle.Length;
            }
        } 
        
        public static void AddSong(this Client client, string song, string artist, string album, byte[] file) {
            IXPFile request = new IXPFile();
            request.NetworkFunction = "com.projectgame.music.music.addsong";
            request.PutInfo("name", song);
            request.PutInfo("artist", artist);
            request.PutInfo("album", album);
            request.PutInfo("file", Convert.ToBase64String(file));
            client.NoResponseRequest(request);
        }
    }
}
