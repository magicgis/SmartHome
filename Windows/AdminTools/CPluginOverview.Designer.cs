namespace AdminTools {
    partial class CPluginOverview {
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
            this.lbPlugins = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbPlugins
            // 
            this.lbPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlugins.FormattingEnabled = true;
            this.lbPlugins.Location = new System.Drawing.Point(0, 0);
            this.lbPlugins.Name = "lbPlugins";
            this.lbPlugins.Size = new System.Drawing.Size(822, 518);
            this.lbPlugins.TabIndex = 0;
            // 
            // PluginOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbPlugins);
            this.Name = "PluginOverview";
            this.Size = new System.Drawing.Size(822, 518);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbPlugins;
    }
}
