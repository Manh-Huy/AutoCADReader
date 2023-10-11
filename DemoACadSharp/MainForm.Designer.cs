namespace DemoACadSharp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripAllFeature = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxDetailEntities = new System.Windows.Forms.PictureBox();
            this.pictureBoxThumbNail = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.statusStripLoadTime = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStripAllFeature.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetailEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbNail)).BeginInit();
            this.statusStripLoadTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripAllFeature
            // 
            this.menuStripAllFeature.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripAllFeature.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.menuStripAllFeature.Location = new System.Drawing.Point(0, 0);
            this.menuStripAllFeature.Name = "menuStripAllFeature";
            this.menuStripAllFeature.Size = new System.Drawing.Size(1560, 28);
            this.menuStripAllFeature.TabIndex = 0;
            this.menuStripAllFeature.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.importFileToolStripMenuItem.Text = "Import (dwg)";
            this.importFileToolStripMenuItem.Click += new System.EventHandler(this.importFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBoxDetailEntities);
            this.panel1.Controls.Add(this.pictureBoxThumbNail);
            this.panel1.Location = new System.Drawing.Point(409, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 429);
            this.panel1.TabIndex = 3;
            // 
            // pictureBoxDetailEntities
            // 
            this.pictureBoxDetailEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDetailEntities.Location = new System.Drawing.Point(503, 17);
            this.pictureBoxDetailEntities.Name = "pictureBoxDetailEntities";
            this.pictureBoxDetailEntities.Size = new System.Drawing.Size(436, 395);
            this.pictureBoxDetailEntities.TabIndex = 1;
            this.pictureBoxDetailEntities.TabStop = false;
            // 
            // pictureBoxThumbNail
            // 
            this.pictureBoxThumbNail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxThumbNail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxThumbNail.Image = global::DemoACadSharp.Properties.Resources._345668002_207268608768377_4716940698160860698_n;
            this.pictureBoxThumbNail.Location = new System.Drawing.Point(32, 17);
            this.pictureBoxThumbNail.Name = "pictureBoxThumbNail";
            this.pictureBoxThumbNail.Size = new System.Drawing.Size(452, 395);
            this.pictureBoxThumbNail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxThumbNail.TabIndex = 0;
            this.pictureBoxThumbNail.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 54);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(355, 420);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // statusStripLoadTime
            // 
            this.statusStripLoadTime.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripLoadTime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStripLoadTime.Location = new System.Drawing.Point(0, 489);
            this.statusStripLoadTime.Name = "statusStripLoadTime";
            this.statusStripLoadTime.Size = new System.Drawing.Size(1560, 26);
            this.statusStripLoadTime.TabIndex = 5;
            this.statusStripLoadTime.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(63, 20);
            this.toolStripStatusLabel.Text = "Loading";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(250, 18);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1560, 515);
            this.Controls.Add(this.statusStripLoadTime);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStripAllFeature);
            this.MainMenuStrip = this.menuStripAllFeature;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStripAllFeature.ResumeLayout(false);
            this.menuStripAllFeature.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetailEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbNail)).EndInit();
            this.statusStripLoadTime.ResumeLayout(false);
            this.statusStripLoadTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripAllFeature;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxDetailEntities;
        private System.Windows.Forms.PictureBox pictureBoxThumbNail;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStripLoadTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}