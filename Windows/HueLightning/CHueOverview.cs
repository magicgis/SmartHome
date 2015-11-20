using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HueLightning.API;
using System.Collections.ObjectModel;

namespace HueLightning {
    public partial class CHueOverview : UserControl {
        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        public CHueOverview() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            ReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            cbLights.Items.Clear();
            foreach (HueLight light in lights)
                cbLights.Items.Add(light);
        }

        private void button2_Click(object sender, EventArgs e) {
            if (cbLights.SelectedIndex == -1) return;
            HueLight light = (HueLight)cbLights.SelectedItem;

            light.Name = tbName.Text;
            light.On = cbOn.Checked;
            light.Brightness = byte.Parse(tbBri.Text);
            light.Hue = ushort.Parse(tbHue.Text);
            light.Saturation = byte.Parse(tbSat.Text);
        }

        private void cbLights_SelectedIndexChanged(object sender, EventArgs e) {
            tbName.Text = "";
            cbOn.Checked = false;
            tbHue.Text = "";
            tbSat.Text = "";
            tbBri.Text = "";

            if (cbLights.SelectedIndex == -1)
                return;

            HueLight light = (HueLight)cbLights.SelectedItem;

            tbName.Text = light.Name;
            cbOn.Checked = light.On;
            tbHue.Text = "" + light.Hue;
            tbSat.Text = "" + light.Saturation;
            tbBri.Text = "" + light.Brightness;
        }
    }
}
