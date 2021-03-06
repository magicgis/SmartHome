﻿using MySql.Data.MySqlClient;
using Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music {
    internal static class MusicDbConnector {
        private const string DATABASE = "com_projectgame_music_music";

        private const string TABLE_INTERPRETERS = "interpreters";
        private const string TABLE_ALBUMS = "albums";
        private const string TABLE_SONGS = "songs";

        private const string COLOUMN_INTERPRETERS_ID = "ID";
        private const string COLOUMN_INTERPRETERS_NAME = "Name";

        private const string COLOUMN_ALBUMS_ID = "ID";
        private const string COLOUMN_ALBUMS_INTERPRETER_ID = "Interpreter_ID";
        private const string COLOUMN_ALBUMS_NAME = "Name";

        private const string COLOUMN_SONGS_ID = "ID";
        private const string COLOUMN_SONGS_ALBUM_ID = "Album_ID";
        private const string COLOUMN_SONGS_NAME = "Name";
        private const string COLOUMN_SONGS_FILE = "File";

        private static int _debugChannel;
        private static MySqlConnection _connection;
        private static bool _connectionOpen;

        public static void Init(MySqlConnection connection) {
            _debugChannel = Debug.AddChannel("com.projectgame.music.musicdbconnector");
            Debug.Log(_debugChannel, "Initializing...");
            _connection = connection;

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS `com_projectgame_music_music`.`albums` (" +
                                                "`ID` int(11) NOT NULL AUTO_INCREMENT," +
                                                "`Interpreter_ID` int(11) NOT NULL, " +
                                                "`Name` varchar(45) NOT NULL," +
                                                "PRIMARY KEY(`ID`))", _connection);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand( "CREATE TABLE IF NOT EXISTS `com_projectgame_music_music`.`interpreters` (" +
                                    "`ID` int(11) NOT NULL AUTO_INCREMENT," +
                                    "`Name` varchar(50) NOT NULL," +
                                    "PRIMARY KEY(`ID`))", _connection);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand( "CREATE TABLE IF NOT EXISTS `com_projectgame_music_music`.`songs` (" +
                                    "`ID` int(11) NOT NULL AUTO_INCREMENT," +
                                    "`Album_ID` int(11) NOT NULL," +
                                    "`Name` varchar(45) NOT NULL," +
                                    "`File` longtext NOT NULL," +
                                    "PRIMARY KEY(`ID`))", _connection);
            cmd.ExecuteNonQuery();

            _connection.Close();
            _connectionOpen = false;
        }

        public static List<int> GetInterpreters() {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            List<int> interpreters = new List<int>();

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_INTERPRETERS_ID + " from " + TABLE_INTERPRETERS, _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    interpreters.Add(reader.GetInt32(COLOUMN_INTERPRETERS_ID));
                }                              
            }   catch(Exception e) {
                interpreters = new List<int>();
            }

            _connection.Close();
            _connectionOpen = false;
            return interpreters;
        }
        public static List<int> GetAlbums(int interpreter) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            List<int> albums = new List<int>();

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_ALBUMS_ID + " from " + TABLE_ALBUMS + " WHERE " + COLOUMN_ALBUMS_INTERPRETER_ID + "='" + interpreter + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    albums.Add(reader.GetInt32(COLOUMN_ALBUMS_ID));
                }

            } catch(Exception e) {
                albums = new List<int>();
            }

            _connection.Close();
            _connectionOpen = false;
            return albums;
        }
        public static List<int> GetSongs(int album) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            List<int> songs = new List<int>();

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_SONGS_ID + " from " + TABLE_SONGS + " WHERE " + COLOUMN_SONGS_ALBUM_ID + "='" + album + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    songs.Add(reader.GetInt32(COLOUMN_ALBUMS_ID));
                }

            } catch (Exception e) {
                songs = new List<int>();
            }

            _connection.Close();
            _connectionOpen = false;
            return songs;
        }

        public static string GetInterpreterName(int interpreter) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            String name = null;

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_INTERPRETERS_NAME + " from " + TABLE_INTERPRETERS + " WHERE " + COLOUMN_INTERPRETERS_ID + "='" + interpreter + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    name = reader.GetString(COLOUMN_INTERPRETERS_NAME);
            } catch(Exception e) {
                name = null;
            }

            _connection.Close();
            _connectionOpen = false;
            return name;
        }

        public static string GetAlbumName(int album) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            String name = null;

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_ALBUMS_NAME + " from " + TABLE_ALBUMS + " WHERE " + COLOUMN_ALBUMS_ID + "='" + album + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    name = reader.GetString(COLOUMN_ALBUMS_NAME);
            } catch (Exception e) {
                name = null;
            }

            _connection.Close();
            _connectionOpen = false;
            return name;
        }

        public static string GetSongName(int song) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            String name = null;

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_SONGS_NAME + " from " + TABLE_SONGS + " WHERE " + COLOUMN_SONGS_ID + "='" + song + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    name = reader.GetString(COLOUMN_SONGS_NAME);
            } catch (Exception e) {
                name = null;
            }

            _connection.Close();
            _connectionOpen = false;
            return name;
        }
        public static byte[] GetSongFile(int song) {

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            byte[] file = null;

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT " + COLOUMN_SONGS_FILE + " from " + TABLE_SONGS + " WHERE " + COLOUMN_SONGS_ID + "='" + song + "'", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    file = Convert.FromBase64String(reader.GetString(COLOUMN_SONGS_FILE));
            } catch (Exception e) {
                file = null;
            }

            _connection.Close();
            _connectionOpen = false;
            return file;
        }

        public static int AddSong(string interpreter, string album, string song, byte[] data) {
            int i = -1;
            int a = -1;
            int s = -1;
            
            foreach (int ci in GetInterpreters()) {
                if (GetInterpreterName(ci).Equals(interpreter)) {
                    i = ci;
                    break;
                }
            }

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            if (i == -1) {
                MySqlCommand cmd1 = new MySqlCommand(
                    "INSERT INTO " + TABLE_INTERPRETERS + " (" + 
                    COLOUMN_INTERPRETERS_NAME + ") VALUES('" +
                    interpreter + "'); " +
                    "SELECT LAST_INSERT_ID();"
                    , _connection);
                i = Convert.ToInt32(cmd1.ExecuteScalar());
            }

            foreach(int ca in GetAlbums(i)) {
                if (GetAlbumName(ca).Equals(album)) {
                    a = ca;
                    break;
                }
            }

            if (!_connectionOpen) {
                _connectionOpen = true;
                _connection.Open();
            }

            if (a == -1) {
                MySqlCommand cmd1 = new MySqlCommand(
                    "INSERT INTO " + TABLE_ALBUMS + " (" +
                    COLOUMN_ALBUMS_INTERPRETER_ID + ", " + COLOUMN_ALBUMS_NAME + ") VALUES('" +
                    i + "', '" + album + "'); " +
                    "SELECT LAST_INSERT_ID();"
                    , _connection);
                a = Convert.ToInt32(cmd1.ExecuteScalar());
            }                              
                                           
            MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO " + TABLE_SONGS + " (" +
                COLOUMN_SONGS_NAME + ", " + COLOUMN_SONGS_FILE + ", " + COLOUMN_SONGS_ALBUM_ID + ") VALUES('" +
                song + "', '" + Convert.ToBase64String(data) + "', '" + a + "'); " +
                "SELECT LAST_INSERT_ID();"
                , _connection);
            object sId = cmd.ExecuteScalar();
            s = Convert.ToInt32(sId);

            _connectionOpen = false;  
            _connection.Close();
            return s;
        }
    }
}
