using Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer {
    public partial class Form1 : Form {
        private Client _client;
        private MusicCollection _musicCollection;

        public Form1() {
            InitializeComponent();
            _client = new Client("127.0.0.1", 10250);
            if (!_client.Connect()) {
                _client = null;
                return;
            }

            _client.Disconnected += Client_Disconnected;
            _client.MessageReceived += Client_MessageReceived;
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            _client.Disconnect();
        }

        private void Client_MessageReceived(byte[] obj) {
            String message = Encoding.UTF8.GetString(obj);

            int xmlCount = Regex.Matches(message, Regex.Escape("<?xml version=")).Count;

            if (xmlCount == 1) {
                HandleMessage(message);
                return;
            }

            List<int> indices = new List<int>();
            foreach (int index in message.IndexesOf("<?xml version="))
                indices.Add(index);

            for (int i = 0; i < indices.Count; i++) {
                string substring = null;

                if (i == indices.Count - 1) {
                    substring = message.Substring(indices[i]);
                } else {
                    substring = message.Substring(indices[i], indices[i + 1] - indices[i]);
                }

                HandleMessage(substring);
            }
        }

        private void HandleMessage(string message) {
            System.Diagnostics.Debug.WriteLine(message);
            IXPFile file = new IXPFile(message);

         }

        private void Client_Disconnected() {
            if (_client == null) {
                MessageBox.Show("Could not connect to server");
                Application.Exit();
                return;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            tbFile.Text = ofd.FileName;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            byte[] data = File.ReadAllBytes(tbFile.Text);
            _client.AddSong(tbSong.Text, tbArtist.Text, tbAlbum.Text, data);
        }

        private void btnReconnect_Click(object sender, EventArgs e) {
            _client = new Client("127.0.0.1", 10250);
            if(_client.Connect()) {
                MessageBox.Show("Connected");
            } else {
                MessageBox.Show("Failed");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            _musicCollection = _client.GetSongs();
            _musicCollection.Sort();

            lbArtists.Items.Clear();
            lbAlbums.Items.Clear();
            lbSongs.Items.Clear();

            for(int i = 0; i < _musicCollection.Artists.Count; i++) {
                lbArtists.Items.Add(_musicCollection.Artists[i].Name);
            }
        }

        private void lbArtists_SelectedIndexChanged(object sender, EventArgs e) {
            lbAlbums.Items.Clear();
            lbSongs.Items.Clear();

            if (lbArtists.SelectedIndex == -1)
                return;

            for(int i = 0; i < _musicCollection.Artists[lbArtists.SelectedIndex].Albums.Count; i++) {
                lbAlbums.Items.Add(_musicCollection.Artists[lbArtists.SelectedIndex].Albums[i].Name);
            }
        }

        private void lbAlbums_SelectedIndexChanged(object sender, EventArgs e) {
            lbSongs.Items.Clear();

            if (lbAlbums.SelectedIndex == -1 || lbArtists.SelectedIndex == -1)
                return;

            for(int i = 0; i < _musicCollection.Artists[lbArtists.SelectedIndex].Albums[lbAlbums.SelectedIndex].Songs.Count; i++) {
                lbSongs.Items.Add(_musicCollection.Artists[lbArtists.SelectedIndex].Albums[lbAlbums.SelectedIndex].Songs[i].Name);
            }
        }
    }
}
