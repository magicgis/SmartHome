namespace HueLightning {
    /// <summary>
    /// A component for the <see cref="DHueOverview"/> plugin
    /// </summary>
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
            this.tbBri = new System.Windows.Forms.TextBox();
            this.tbSat = new System.Windows.Forms.TextBox();
            this.lblHue = new System.Windows.Forms.Label();
            this.lblBri = new System.Windows.Forms.Label();
            this.lblSat = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BrightnessSlider = new System.Windows.Forms.TrackBar();
            this.PickerImage = new System.Windows.Forms.Panel();
            this.tbHue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(165, 356);
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
            // tbBri
            // 
            this.tbBri.Location = new System.Drawing.Point(264, 6);
            this.tbBri.Name = "tbBri";
            this.tbBri.Size = new System.Drawing.Size(55, 20);
            this.tbBri.TabIndex = 8;
            // 
            // tbSat
            // 
            this.tbSat.Location = new System.Drawing.Point(158, 6);
            this.tbSat.Name = "tbSat";
            this.tbSat.Size = new System.Drawing.Size(55, 20);
            this.tbSat.TabIndex = 9;
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Location = new System.Drawing.Point(12, 9);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(30, 13);
            this.lblHue.TabIndex = 10;
            this.lblHue.Text = "Hue:";
            // 
            // lblBri
            // 
            this.lblBri.AutoSize = true;
            this.lblBri.Location = new System.Drawing.Point(236, 9);
            this.lblBri.Name = "lblBri";
            this.lblBri.Size = new System.Drawing.Size(22, 13);
            this.lblBri.TabIndex = 11;
            this.lblBri.Text = "Bri:";
            // 
            // lblSat
            // 
            this.lblSat.AutoSize = true;
            this.lblSat.Location = new System.Drawing.Point(126, 9);
            this.lblSat.Name = "lblSat";
            this.lblSat.Size = new System.Drawing.Size(26, 13);
            this.lblSat.TabIndex = 12;
            this.lblSat.Text = "Sat:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(246, 356);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(377, 140);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox1.Size = new System.Drawing.Size(476, 357);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.BrightnessSlider, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.PickerImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(9);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 326);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BrightnessSlider
            // 
            this.BrightnessSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrightnessSlider.Location = new System.Drawing.Point(399, 9);
            this.BrightnessSlider.Margin = new System.Windows.Forms.Padding(0);
            this.BrightnessSlider.Name = "BrightnessSlider";
            this.BrightnessSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.BrightnessSlider.Size = new System.Drawing.Size(50, 208);
            this.BrightnessSlider.TabIndex = 0;
            this.BrightnessSlider.TickFrequency = 999999;
            this.BrightnessSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // PickerImage
            // 
            this.PickerImage.BackColor = System.Drawing.Color.Yellow;
            this.PickerImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PickerImage.Location = new System.Drawing.Point(9, 9);
            this.PickerImage.Margin = new System.Windows.Forms.Padding(0);
            this.PickerImage.Name = "PickerImage";
            this.PickerImage.Size = new System.Drawing.Size(390, 208);
            this.PickerImage.TabIndex = 1;
            // 
            // tbHue
            // 
            this.tbHue.Location = new System.Drawing.Point(48, 6);
            this.tbHue.Name = "tbHue";
            this.tbHue.Size = new System.Drawing.Size(55, 20);
            this.tbHue.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbBri);
            this.panel1.Controls.Add(this.tbHue);
            this.panel1.Controls.Add(this.lblBri);
            this.panel1.Controls.Add(this.tbSat);
            this.panel1.Controls.Add(this.lblHue);
            this.panel1.Controls.Add(this.lblSat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(9, 217);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(390, 100);
            this.panel1.TabIndex = 2;
            // 
            // CHueOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cbOn);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblLights);
            this.Controls.Add(this.cbLights);
            this.Controls.Add(this.btnRefresh);
            this.Name = "CHueOverview";
            this.Size = new System.Drawing.Size(1070, 535);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox tbBri;
        private System.Windows.Forms.TextBox tbSat;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.Label lblBri;
        private System.Windows.Forms.Label lblSat;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar BrightnessSlider;
        private System.Windows.Forms.Panel PickerImage;
        private System.Windows.Forms.TextBox tbHue;
        private System.Windows.Forms.Panel panel1;
    }
}
