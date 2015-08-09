using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking;
using IXP;
using IXPCommunication;

namespace Alarm {
    public partial class FormMain : Form {
        public const string TIME_SERVICE_FUNCTION = "time_service_function";
        public const string ALARM_SERVICE_FUNCTION = "alarm_service_function";

        private Client _client;
        private bool _didclose;
        private List<Alarm> _alarms;

        public FormMain() {
            InitializeComponent();
            _client = new Client("127.0.0.1", 10250);
            if (!_client.Connect()) {
                _client = null;
                return;
            }
            _client.Disconnected += Client_Disconnected;
            _client.MessageReceived += Client_MessageReceived;
            FormClosing += FormMain_FormClosing;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            if(_client == null) {
                MessageBox.Show("Could not connect to server");
                Application.Exit();
                return;
            }

            _client.RegisterToAlarmService(ALARM_SERVICE_FUNCTION);
            //_client.RegisterToTimeService(TIME_SERVICE_FUNCTION);
            RefreshAlarms();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            _didclose = true;
            _client.Disconnect();
        }

        private void RefreshAlarms() {
            _alarms = _client.GetAlarms();

            lbAlarms.Items.Clear();
            foreach (Alarm alarm in _alarms)
                lbAlarms.Items.Add(alarm.Name);

            lbAlarms.SelectedIndex = -1;
        }
        private void CallAlarm(IXPFile file) {
            int id = int.Parse(file.GetInfoValue("alarm_id"));
            Alarm a = _alarms.FirstOrDefault(al => al.ID == id);

            MessageBox.Show("ALARM: " + a.Name);
        }  
        private void UpdarteTime(IXPFile file) {
            Invoke((MethodInvoker)delegate { lblCurTime.Text = String.Format("{0:00}:{1:00}:{2:00}", int.Parse(file.GetInfoValue("hours")), int.Parse(file.GetInfoValue("minutes")), int.Parse(file.GetInfoValue("seconds"))); });
        }

        private void Client_Disconnected() {
            if (_didclose)
                return;

            MessageBox.Show("Server closed");
            Application.Exit();
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

            if (file.NetworkFunction.Equals(ALARM_SERVICE_FUNCTION))
                CallAlarm(file);

            if (file.NetworkFunction.Equals(TIME_SERVICE_FUNCTION))
                UpdarteTime(file);
        }

        private void lbAlarms_SelectedIndexChanged(object sender, EventArgs e) {
            lblIDValue.Text = "";
            tbName.Text = "";
            tbHours.Text = "";
            tbMinutes.Text = "";
            tbSeconds.Text = "";
            cbMon.Checked = false;
            cbTue.Checked = false;
            cbWed.Checked = false;
            cbThu.Checked = false;
            cbFri.Checked = false;
            cbSat.Checked = false;
            cbSun.Checked = false;
            cbEnabled.Checked = false;

            if (lbAlarms.SelectedIndex == -1)
                return;

            Alarm alarm = _alarms[lbAlarms.SelectedIndex];
            lblIDValue.Text = "" + alarm.ID;
            tbName.Text = alarm.Name;
            tbHours.Text = "" + alarm.Hours;
            tbMinutes.Text = "" + alarm.Minutes;
            tbSeconds.Text = "" + alarm.Seconds;
            cbMon.Checked = alarm.Mon;
            cbTue.Checked = alarm.Tue;
            cbWed.Checked = alarm.Wed;
            cbThu.Checked = alarm.Thu;
            cbFri.Checked = alarm.Fri;
            cbSat.Checked = alarm.Sat;
            cbSun.Checked = alarm.Sun;
            cbEnabled.Checked = alarm.Enabled;
        }
        private void btnRefresh_Click(object sender, EventArgs e) {
            RefreshAlarms();
        }
        private void btnNew_Click(object sender, EventArgs e) {
            int hours = -1;
            int minutes = -1;
            int seconds = -1;

            bool hr = int.TryParse(tbHours.Text, out hours);
            bool mr = int.TryParse(tbMinutes.Text, out minutes);
            bool sr = int.TryParse(tbSeconds.Text, out seconds);

            if(!hr || !mr || !sr) {
                MessageBox.Show("Could not parse time!");
                return;
            }

            Alarm alarm = new Alarm(-1, tbName.Text, hours, minutes, seconds, cbMon.Checked, cbTue.Checked, cbWed.Checked, cbThu.Checked, cbFri.Checked, cbSat.Checked, cbSun.Checked, cbEnabled.Checked);
            alarm = _client.AddAlarm(alarm);
            _alarms.Add(alarm);
            lbAlarms.Items.Add(alarm.Name);
        }
        private void btnSave_Click(object sender, EventArgs e) {
            if (lbAlarms.SelectedIndex == -1)
                return;

            int index = lbAlarms.SelectedIndex;

            int hours = -1;
            int minutes = -1;
            int seconds = -1;

            bool hr = int.TryParse(tbHours.Text, out hours);
            bool mr = int.TryParse(tbMinutes.Text, out minutes);
            bool sr = int.TryParse(tbSeconds.Text, out seconds);

            if (!hr || !mr || !sr) {
                MessageBox.Show("Could not parse time!");
                return;
            }

            Alarm alarm = _alarms[lbAlarms.SelectedIndex];
            alarm.Name = tbName.Text;
            alarm.Hours = hours;
            alarm.Minutes = minutes;
            alarm.Seconds = seconds;
            alarm.Mon = cbMon.Checked;
            alarm.Tue = cbTue.Checked;
            alarm.Wed = cbWed.Checked;
            alarm.Thu = cbThu.Checked;
            alarm.Fri = cbFri.Checked;
            alarm.Sat = cbSat.Checked;
            alarm.Sun = cbSun.Checked;
            alarm.Enabled = cbEnabled.Checked;

            _client.UpdateAlarm(alarm);

            lbAlarms.Items.RemoveAt(index);
            lbAlarms.Items.Insert(index, alarm.Name);
        }
        private void btnDelete_Click(object sender, EventArgs e) {
            if (lbAlarms.SelectedIndex == -1)
                return;

            Alarm alarm = _alarms[lbAlarms.SelectedIndex];
            _client.DeleteAlarm(alarm);
            lbAlarms.Items.RemoveAt(lbAlarms.SelectedIndex);
        }
    }
}
