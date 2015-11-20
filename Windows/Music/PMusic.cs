﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;
using MySql.Data.MySqlClient;
using IXP;

namespace Music {
    class PMusic : Plugin.Plugin {
        public override string Name {
            get {
                return "com.projectgame.music.music";
            }
        }

        public override int Version {
            get {
                return 1;
            }
        }

        public override void OnPluginLoad() {
            MySqlConnection con = GetDatabaseConnection;
            MusicDbConnector.Init(con);
        }

        [NetworkFunction("com.projectgame.music.music.addsong")]
        public int NetworkAddSong(String name, String album, String artist, String file) {
            int id = MusicDbConnector.AddSong(artist, album, name, Encoding.UTF8.GetBytes(file));
            return id;
        }

        [NetworkFunction("com.projectgame.music.music.getsongs")]
        public IXPFile NetworkGetSongs() {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.music.music.getsongs";

            List<int> artists = MusicDbConnector.GetInterpreters();
            file.PutInfo("artist_count", "" + artists.Count);

            int ic = 0;
            foreach(int artist in artists) {
                file.PutInfo("artist_" + ic + "_id", "" + artist);
                file.PutInfo("artist_" + ic + "_name", MusicDbConnector.GetInterpreterName(artist));

                List<int> albums = MusicDbConnector.GetAlbums(artist);
                file.PutInfo("artist_" + ic + "_album_count", "" + albums.Count);
                int ac = 0;
                foreach(int album in albums) {
                    file.PutInfo("artist_" + ic + "_album_" + ac + "_id", "" + album);
                    file.PutInfo("artist_" + ic + "_album_" + ac + "_name", MusicDbConnector.GetAlbumName(album));

                    List<int> songs = MusicDbConnector.GetSongs(album);
                    file.PutInfo("artist_" + ic + "_album_" + ac + "_song_count", "" + songs.Count);
                    int sc = 0;
                    foreach(int song in songs) {
                        file.PutInfo("artist_" + ic + "_album_" + ac + "_song_" + sc + "_id", "" + song);
                        file.PutInfo("artist_" + ic + "_album_" + ac + "_song_" + sc + "_name", MusicDbConnector.GetSongName(song));

                        sc++;
                    }

                    ac++;
                }                                                

                ic++;
            }

            return file;
        }
    }
}
