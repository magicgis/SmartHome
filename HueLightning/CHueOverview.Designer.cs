namespace HueLightning {
    partial class CHueOverview {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbLights = new System.Windows.Forms.ComboBox();
            this.lblLights = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbOn = new System.Windows.Forms.CheckBox();
            this.tbHue = new System.Windows.Forms.TextBox();
            this.tbBri = new System.Windows.Forms.TextBox();
            this.tbSat = new System.Windows.Forms.TextBox();
            this.lblHue = new System.Windows.Forms.Label();
            this.lblBri = new System.Windows.Forms.Label();
            this.lblSat = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(221, 286);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbLights
            // 
            this.cbLights.FormattingEnabled = true;
            this.cbLights.Location = new System.Drawing.Point(262, 58);
            this.cbLights.Name = "cbLights";
            this.cbLights.Size = new System.Drawing.Size(300, 21);
            this.cbLights.TabIndex = 2;
            this.cbLights.SelectedIndexChanged += new System.EventHandler(this.cbLights_SelectedIndexChanged);
            // 
            // lblLights
            // 
            this.lblLights.AutoSize = true;
            this.lblLights.Location = new System.Drawing.Point(218, 61);
            this.lblLights.Name = "lblLights";
            this.lblLights.Size = new System.Drawing.Size(38, 13);
            this.lblLights.TabIndex = 3;
            this.lblLights.Text = "Lights:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(218, 103);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(262, 100);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(300, 20);
            this.tbName.TabIndex = 5;
            // 
            // cbOn
            // 
            this.cbOn.AutoSize = true;
            this.cbOn.Location = new System.Drawing.Point(262, 140);
            this.cbOn.Name = "cbOn";
            this.cbOn.Size = new System.Drawing.Size(40, 17);
            this.cbOn.TabIndex = 6;
            this.cbOn.Text = "On";
            this.cbOn.UseVisualStyleBackColor = true;
            // 
            // tbHue
            // 
            this.tbHue.Location = new System.Drawing.Point(262, 173);
            this.tbHue.Name = "tbHue";
            this.tbHue.Size = new System.Drawing.Size(55, 20);
            this.tbHue.TabIndex = 7;
            // 
            // tbBri
            // 
            this.tbBri.Location = new System.Drawing.Point(262, 208);
            this.tbBri.Name = "tbBri";
            this.tbBri.Size = new System.Drawing.Size(55, 20);
            this.tbBri.TabIndex = 8;
            // 
            // tbSat
            // 
            this.tbSat.Location = new System.Drawing.Point(262, 246);
            this.tbSat.Name = "tbSat";
            this.tbSat.Size = new System.Drawing.Size(55, 20);
            this.tbSat.TabIndex = 9;
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Location = new System.Drawing.Point(230, 176);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(30, 13);
            this.lblHue.TabIndex = 10;
            this.lblHue.Text = "Hue:";
            // 
            // lblBri
            // 
            this.lblBri.AutoSize = true;
            this.lblBri.Location = new System.Drawing.Point(234, 211);
            this.lblBri.Name = "lblBri";
            this.lblBri.Size = new System.Drawing.Size(22, 13);
            this.lblBri.TabIndex = 11;
            this.lblBri.Text = "Bri:";
            // 
            // lblSat
            // 
            this.lblSat.AutoSize = true;
            this.lblSat.Location = new System.Drawing.Point(230, 249);
            this.lblSat.Name = "lblSat";
            this.lblSat.Size = new System.Drawing.Size(26, 13);
            this.lblSat.TabIndex = 12;
            this.lblSat.Text = "Sat:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(487, 286);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.button2_Click);
            // 
            // CHueOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblSat);
            this.Controls.Add(this.lblBri);
            this.Controls.Add(this.lblHue);
            this.Controls.Add(this.tbSat);
            this.Controls.Add(this.tbBri);
            this.Controls.Add(this.tbHue);
            this.Controls.Add(this.cbOn);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblLights);
            this.Controls.Add(this.cbLights);
            this.Controls.Add(this.btnRefresh);
            this.Name = "CHueOverview";
            this.Size = new System.Drawing.Size(1070, 535);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cbLights;
        private System.Windows.Forms.Label lblLights;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox cbOn;
        private System.Windows.Forms.TextBox tbHue;
        private System.Windows.Forms.TextBox tbBri;
        private System.Windows.Forms.TextBox tbSat;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.Label lblBri;
        private System.Windows.Forms.Label lblSat;
        private System.Windows.Forms.Button btnUpdate;
    }
}
