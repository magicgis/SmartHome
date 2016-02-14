using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminTools {
    /// <summary>
    /// The visible control for the <see cref="DPNetworkFunctions"/> plugin
    /// </summary>
    public partial class CNetworkFunctions : UserControl {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public CNetworkFunctions() {
            InitializeComponent();
        }

        /// <summary>
        /// Refreshes the currently avaliable network functions
        /// </summary>
        public void RefreshFunctions() {
            //if (!this.Created)
            //    return;
            if (this.IsDisposed)
                return;

            this.Invoke((MethodInvoker)delegate {
                lbFunctions.Items.Clear();

                List<string> functions = Plugin.NetworkManager.NetworkFunctions.ToList();
                functions.Sort();

                foreach(string function in functions) {
                    lbFunctions.Items.Add(function);
                }
            });
        }

        private void lbFunctions_SelectedIndexChanged(object sender, EventArgs e) {
            if(lbFunctions.SelectedIndex == -1) {
                lbFunctionParameters.Items.Clear();
                lblReturnType.Text = "";
                lblFunctionName.Text = "";
            }

            string strfunc = (string)lbFunctions.Items[lbFunctions.SelectedIndex];
            Plugin.FunctionInfo func = Plugin.NetworkManager.GetNetworkFunction(strfunc);
            lblFunctionName.Text = func.Name;
            lblReturnType.Text = func.ReturnType.ToString();

            lbFunctionParameters.Items.Clear();
            foreach(string param in func.Parameters) {
                Type type = func.GetParameterType(param);

                lbFunctionParameters.Items.Add(type.ToString() + " " + param);
            }
        }
    }
}
