using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

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

        public static MusicCollection GetSongs(this Client client) {
            IXPFile request = new IXPFile();
            request.NetworkFunction = "com.projectgame.music.music.getsongs";
            IXPFile response = client.IXPRequest(request);

            MusicCollection musicCollection = new MusicCollection();
            int artistCount = int.Parse(response.GetInfoValue("artist_count"));

            for(int currentArtistIndex = 0; currentArtistIndex < artistCount; currentArtistIndex++) {
                int artistID = int.Parse(response.GetInfoValue("artist_" + currentArtistIndex + "_id"));
                string artistName = response.GetInfoValue("artist_" + currentArtistIndex + "_name");
                Artist artist = new Artist(artistID, artistName);
                musicCollection.Artists.Add(artist);

                int albumCount = int.Parse(response.GetInfoValue("artist_" + currentArtistIndex + "_album_count"));
                
                for(int currentAlbumIndex = 0; currentAlbumIndex < albumCount; currentAlbumIndex++) {
                    int albumID = int.Parse(response.GetInfoValue("artist_" + currentArtistIndex + "_album_" + currentAlbumIndex + "_id"));
                    string albumName = response.GetInfoValue("artist_" + currentArtistIndex + "_album_" + currentAlbumIndex + "_name");
                    Album album = new Album(albumID, albumName);
                    artist.Albums.Add(album);

                    int songCount = int.Parse(response.GetInfoValue("artist_" + currentArtistIndex + "_album_" + currentAlbumIndex + "_song_count"));

                    for(int currentSongIndex = 0; currentSongIndex  < songCount; currentSongIndex++) {
                        int songID = int.Parse(response.GetInfoValue("artist_" + currentArtistIndex + "_album_" + currentAlbumIndex + "_song_" + currentSongIndex + "_id"));
                        string songName = response.GetInfoValue("artist_" + currentArtistIndex + "_album_" + currentAlbumIndex + "_song_" + currentSongIndex + "_name");
                        Song song = new Song(songID, songName);
                        album.Songs.Add(song);
                    }
                }
            }

            return musicCollection;
        }

        public static byte[] GetSongData(this Client client, int songId) {
            IXPFile request = new IXPFile();
            request.NetworkFunction = "com.projectgame.music.music.getsongdata";
            request.PutInfo("song_id", "" + songId);
            String response = client.SimpleRequest(request);
            return Convert.FromBase64String(response);
        }
    }
}
