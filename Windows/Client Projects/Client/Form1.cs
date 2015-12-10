using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking;                    

namespace Client {
    public partial class Form1 : Form {
        private Networking.Client _client;

        public Form1() {
            InitializeComponent();
            _client = new Networking.Client("127.0.0.1", 10250);
            _client.Connect();
            
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            _client.Disconnect();
        }

        private void btnGetTimt_Click(object sender, EventArgs e) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.clock.gettime";
            IXPRequest request = new IXPRequest(_client, file, GetTime_Response);
        }

        private void GetTime_Response(string response) {
            Invoke((MethodInvoker)delegate {
                MessageBox.Show(String.Format("Its {0}", response));
            });
        }

        private void button1_Click(object sender, EventArgs e) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.registeralarm";
            file.PutInfo("name", "This is my test alarm");
            file.PutInfo("hours", "17");
            file.PutInfo("minutes", "45");
            file.PutInfo("seconds", "00");
            file.PutInfo("mon", "TRUE");
            file.PutInfo("tue", "FALSE");
            file.PutInfo("wed", "FALSE");
            file.PutInfo("thu", "FALSE");
            file.PutInfo("fri", "TRUE");
            file.PutInfo("sat", "FALSE");
            file.PutInfo("sun", "TRUE");
            file.PutInfo("enabled", "TRUE");

            IXPRequest request = new IXPRequest(_client, file, AddAlarm_Response);
        }

        private void AddAlarm_Response(string response) {
            Invoke((MethodInvoker)delegate {
                MessageBox.Show(response);
            });
        }
    }
}
