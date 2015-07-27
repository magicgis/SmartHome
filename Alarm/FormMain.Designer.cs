namespace Alarm {
    partial class FormMain {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tlpMaster = new System.Windows.Forms.TableLayoutPanel();
            this.gbAlarms = new System.Windows.Forms.GroupBox();
            this.lbAlarms = new System.Windows.Forms.ListBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.lblCurTime = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.cbSun = new System.Windows.Forms.CheckBox();
            this.cbSat = new System.Windows.Forms.CheckBox();
            this.cbFri = new System.Windows.Forms.CheckBox();
            this.cbThu = new System.Windows.Forms.CheckBox();
            this.cbWed = new System.Windows.Forms.CheckBox();
            this.cbTue = new System.Windows.Forms.CheckBox();
            this.cbMon = new System.Windows.Forms.CheckBox();
            this.lblColoumnMinutesSeconds = new System.Windows.Forms.Label();
            this.lblColoumnHoursMinutes = new System.Windows.Forms.Label();
            this.tbSeconds = new System.Windows.Forms.TextBox();
            this.tbMinutes = new System.Windows.Forms.TextBox();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lblEnabled = new System.Windows.Forms.Label();
            this.lblWeekdays = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblIDValue = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.tlpMaster.SuspendLayout();
            this.gbAlarms.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.gbTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMaster
            // 
            this.tlpMaster.ColumnCount = 2;
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.94949F));
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.05051F));
            this.tlpMaster.Controls.Add(this.gbAlarms, 0, 0);
            this.tlpMaster.Controls.Add(this.gbInfo, 1, 0);
            this.tlpMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMaster.Location = new System.Drawing.Point(0, 0);
            this.tlpMaster.Name = "tlpMaster";
            this.tlpMaster.RowCount = 1;
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaster.Size = new System.Drawing.Size(521, 554);
            this.tlpMaster.TabIndex = 0;
            // 
            // gbAlarms
            // 
            this.gbAlarms.Controls.Add(this.lbAlarms);
            this.gbAlarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlarms.Location = new System.Drawing.Point(3, 3);
            this.gbAlarms.Name = "gbAlarms";
            this.gbAlarms.Size = new System.Drawing.Size(176, 548);
            this.gbAlarms.TabIndex = 0;
            this.gbAlarms.TabStop = false;
            this.gbAlarms.Text = "Alarms";
            // 
            // lbAlarms
            // 
            this.lbAlarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAlarms.FormattingEnabled = true;
            this.lbAlarms.Location = new System.Drawing.Point(3, 16);
            this.lbAlarms.Name = "lbAlarms";
            this.lbAlarms.Size = new System.Drawing.Size(170, 529);
            this.lbAlarms.TabIndex = 0;
            this.lbAlarms.SelectedIndexChanged += new System.EventHandler(this.lbAlarms_SelectedIndexChanged);
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.gbTime);
            this.gbInfo.Controls.Add(this.btnRefresh);
            this.gbInfo.Controls.Add(this.btnNew);
            this.gbInfo.Controls.Add(this.btnSave);
            this.gbInfo.Controls.Add(this.btnDelete);
            this.gbInfo.Controls.Add(this.tbName);
            this.gbInfo.Controls.Add(this.lblName);
            this.gbInfo.Controls.Add(this.cbEnabled);
            this.gbInfo.Controls.Add(this.cbSun);
            this.gbInfo.Controls.Add(this.cbSat);
            this.gbInfo.Controls.Add(this.cbFri);
            this.gbInfo.Controls.Add(this.cbThu);
            this.gbInfo.Controls.Add(this.cbWed);
            this.gbInfo.Controls.Add(this.cbTue);
            this.gbInfo.Controls.Add(this.cbMon);
            this.gbInfo.Controls.Add(this.lblColoumnMinutesSeconds);
            this.gbInfo.Controls.Add(this.lblColoumnHoursMinutes);
            this.gbInfo.Controls.Add(this.tbSeconds);
            this.gbInfo.Controls.Add(this.tbMinutes);
            this.gbInfo.Controls.Add(this.tbHours);
            this.gbInfo.Controls.Add(this.lblEnabled);
            this.gbInfo.Controls.Add(this.lblWeekdays);
            this.gbInfo.Controls.Add(this.lblTime);
            this.gbInfo.Controls.Add(this.lblIDValue);
            this.gbInfo.Controls.Add(this.lblID);
            this.gbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInfo.Location = new System.Drawing.Point(185, 3);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(333, 548);
            this.gbInfo.TabIndex = 1;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Info";
            // 
            // gbTime
            // 
            this.gbTime.Controls.Add(this.lblCurTime);
            this.gbTime.Location = new System.Drawing.Point(9, 365);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(315, 171);
            this.gbTime.TabIndex = 26;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Time";
            // 
            // lblCurTime
            // 
            this.lblCurTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurTime.Location = new System.Drawing.Point(3, 16);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(309, 152);
            this.lblCurTime.TabIndex = 0;
            this.lblCurTime.Text = "00:00:00";
            this.lblCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(10, 336);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(91, 336);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 24;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(172, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(253, 336);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(73, 50);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(148, 20);
            this.tbName.TabIndex = 21;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 53);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 20;
            this.lblName.Text = "Name:";
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(73, 304);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbEnabled.TabIndex = 19;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // cbSun
            // 
            this.cbSun.AutoSize = true;
            this.cbSun.Location = new System.Drawing.Point(73, 261);
            this.cbSun.Name = "cbSun";
            this.cbSun.Size = new System.Drawing.Size(62, 17);
            this.cbSun.TabIndex = 18;
            this.cbSun.Text = "Sunday";
            this.cbSun.UseVisualStyleBackColor = true;
            // 
            // cbSat
            // 
            this.cbSat.AutoSize = true;
            this.cbSat.Location = new System.Drawing.Point(73, 238);
            this.cbSat.Name = "cbSat";
            this.cbSat.Size = new System.Drawing.Size(68, 17);
            this.cbSat.TabIndex = 17;
            this.cbSat.Text = "Saturday";
            this.cbSat.UseVisualStyleBackColor = true;
            // 
            // cbFri
            // 
            this.cbFri.AutoSize = true;
            this.cbFri.Location = new System.Drawing.Point(73, 215);
            this.cbFri.Name = "cbFri";
            this.cbFri.Size = new System.Drawing.Size(54, 17);
            this.cbFri.TabIndex = 16;
            this.cbFri.Text = "Friday";
            this.cbFri.UseVisualStyleBackColor = true;
            // 
            // cbThu
            // 
            this.cbThu.AutoSize = true;
            this.cbThu.Location = new System.Drawing.Point(73, 192);
            this.cbThu.Name = "cbThu";
            this.cbThu.Size = new System.Drawing.Size(70, 17);
            this.cbThu.TabIndex = 15;
            this.cbThu.Text = "Thursday";
            this.cbThu.UseVisualStyleBackColor = true;
            // 
            // cbWed
            // 
            this.cbWed.AutoSize = true;
            this.cbWed.Location = new System.Drawing.Point(73, 169);
            this.cbWed.Name = "cbWed";
            this.cbWed.Size = new System.Drawing.Size(83, 17);
            this.cbWed.TabIndex = 14;
            this.cbWed.Text = "Wednesday";
            this.cbWed.UseVisualStyleBackColor = true;
            // 
            // cbTue
            // 
            this.cbTue.AutoSize = true;
            this.cbTue.Location = new System.Drawing.Point(73, 146);
            this.cbTue.Name = "cbTue";
            this.cbTue.Size = new System.Drawing.Size(67, 17);
            this.cbTue.TabIndex = 13;
            this.cbTue.Text = "Tuesday";
            this.cbTue.UseVisualStyleBackColor = true;
            // 
            // cbMon
            // 
            this.cbMon.AutoSize = true;
            this.cbMon.Location = new System.Drawing.Point(73, 123);
            this.cbMon.Name = "cbMon";
            this.cbMon.Size = new System.Drawing.Size(64, 17);
            this.cbMon.TabIndex = 12;
            this.cbMon.Text = "Monday";
            this.cbMon.UseVisualStyleBackColor = true;
            // 
            // lblColoumnMinutesSeconds
            // 
            this.lblColoumnMinutesSeconds.AutoSize = true;
            this.lblColoumnMinutesSeconds.Location = new System.Drawing.Point(168, 84);
            this.lblColoumnMinutesSeconds.Name = "lblColoumnMinutesSeconds";
            this.lblColoumnMinutesSeconds.Size = new System.Drawing.Size(10, 13);
            this.lblColoumnMinutesSeconds.TabIndex = 11;
            this.lblColoumnMinutesSeconds.Text = ":";
            // 
            // lblColoumnHoursMinutes
            // 
            this.lblColoumnHoursMinutes.AutoSize = true;
            this.lblColoumnHoursMinutes.Location = new System.Drawing.Point(112, 84);
            this.lblColoumnHoursMinutes.Name = "lblColoumnHoursMinutes";
            this.lblColoumnHoursMinutes.Size = new System.Drawing.Size(10, 13);
            this.lblColoumnHoursMinutes.TabIndex = 10;
            this.lblColoumnHoursMinutes.Text = ":";
            // 
            // tbSeconds
            // 
            this.tbSeconds.Location = new System.Drawing.Point(184, 81);
            this.tbSeconds.Name = "tbSeconds";
            this.tbSeconds.Size = new System.Drawing.Size(34, 20);
            this.tbSeconds.TabIndex = 9;
            // 
            // tbMinutes
            // 
            this.tbMinutes.Location = new System.Drawing.Point(128, 81);
            this.tbMinutes.Name = "tbMinutes";
            this.tbMinutes.Size = new System.Drawing.Size(34, 20);
            this.tbMinutes.TabIndex = 8;
            // 
            // tbHours
            // 
            this.tbHours.Location = new System.Drawing.Point(73, 81);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(34, 20);
            this.tbHours.TabIndex = 7;
            // 
            // lblEnabled
            // 
            this.lblEnabled.AutoSize = true;
            this.lblEnabled.Location = new System.Drawing.Point(6, 305);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(49, 13);
            this.lblEnabled.TabIndex = 6;
            this.lblEnabled.Text = "Enabled:";
            // 
            // lblWeekdays
            // 
            this.lblWeekdays.AutoSize = true;
            this.lblWeekdays.Location = new System.Drawing.Point(6, 123);
            this.lblWeekdays.Name = "lblWeekdays";
            this.lblWeekdays.Size = new System.Drawing.Size(61, 13);
            this.lblWeekdays.TabIndex = 4;
            this.lblWeekdays.Text = "Weekdays:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(6, 84);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(33, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Time:";
            // 
            // lblIDValue
            // 
            this.lblIDValue.AutoSize = true;
            this.lblIDValue.Location = new System.Drawing.Point(73, 31);
            this.lblIDValue.Name = "lblIDValue";
            this.lblIDValue.Size = new System.Drawing.Size(0, 13);
            this.lblIDValue.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(6, 31);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 554);
            this.Controls.Add(this.tlpMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.Text = "Alarm";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tlpMaster.ResumeLayout(false);
            this.gbAlarms.ResumeLayout(false);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.gbTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMaster;
        private System.Windows.Forms.GroupBox gbAlarms;
        private System.Windows.Forms.ListBox lbAlarms;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.CheckBox cbSun;
        private System.Windows.Forms.CheckBox cbSat;
        private System.Windows.Forms.CheckBox cbFri;
        private System.Windows.Forms.CheckBox cbThu;
        private System.Windows.Forms.CheckBox cbWed;
        private System.Windows.Forms.CheckBox cbTue;
        private System.Windows.Forms.CheckBox cbMon;
        private System.Windows.Forms.Label lblColoumnMinutesSeconds;
        private System.Windows.Forms.Label lblColoumnHoursMinutes;
        private System.Windows.Forms.TextBox tbSeconds;
        private System.Windows.Forms.TextBox tbMinutes;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lblEnabled;
        private System.Windows.Forms.Label lblWeekdays;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblIDValue;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label lblCurTime;
    }
}

