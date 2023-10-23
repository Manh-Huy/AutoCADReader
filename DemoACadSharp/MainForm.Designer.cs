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
            this.components = new System.ComponentModel.Container();
            this.menuStripAllFeature = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripLoadTime = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSelect = new System.Windows.Forms.Button();
            this.pictureBoxThumbNail = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportToJSON = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.treeViewSelectedEntity = new System.Windows.Forms.TreeView();
            this.pictureBoxSelected = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTopFloor = new System.Windows.Forms.TextBox();
            this.cbNumberFloor = new System.Windows.Forms.ComboBox();
            this.txtNameHouse = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStripAllFeature.SuspendLayout();
            this.statusStripLoadTime.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbNail)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripAllFeature
            // 
            this.menuStripAllFeature.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripAllFeature.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.menuStripAllFeature.Location = new System.Drawing.Point(0, 0);
            this.menuStripAllFeature.Name = "menuStripAllFeature";
            this.menuStripAllFeature.Size = new System.Drawing.Size(981, 28);
            this.menuStripAllFeature.TabIndex = 0;
            this.menuStripAllFeature.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.exportJSONToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.importFileToolStripMenuItem.Text = "Import (dwg)";
            this.importFileToolStripMenuItem.Click += new System.EventHandler(this.importFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // exportJSONToolStripMenuItem
            // 
            this.exportJSONToolStripMenuItem.Name = "exportJSONToolStripMenuItem";
            this.exportJSONToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.exportJSONToolStripMenuItem.Text = "Export (JSON)";
            this.exportJSONToolStripMenuItem.Click += new System.EventHandler(this.exportJSONToolStripMenuItem_Click);
            // 
            // statusStripLoadTime
            // 
            this.statusStripLoadTime.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripLoadTime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStripLoadTime.Location = new System.Drawing.Point(0, 489);
            this.statusStripLoadTime.Name = "statusStripLoadTime";
            this.statusStripLoadTime.Size = new System.Drawing.Size(981, 26);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(981, 413);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSelect);
            this.tabPage1.Controls.Add(this.pictureBoxThumbNail);
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(973, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Location = new System.Drawing.Point(3, 349);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(355, 29);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // pictureBoxThumbNail
            // 
            this.pictureBoxThumbNail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxThumbNail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxThumbNail.Image = global::DemoACadSharp.Properties.Resources._345668002_207268608768377_4716940698160860698_n;
            this.pictureBoxThumbNail.Location = new System.Drawing.Point(364, 6);
            this.pictureBoxThumbNail.Name = "pictureBoxThumbNail";
            this.pictureBoxThumbNail.Size = new System.Drawing.Size(601, 375);
            this.pictureBoxThumbNail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxThumbNail.TabIndex = 0;
            this.pictureBoxThumbNail.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(3, 6);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(355, 337);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(973, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnExportToJSON);
            this.panel1.Controls.Add(this.propertyGrid1);
            this.panel1.Controls.Add(this.treeViewSelectedEntity);
            this.panel1.Controls.Add(this.pictureBoxSelected);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(957, 414);
            this.panel1.TabIndex = 3;
            // 
            // btnExportToJSON
            // 
            this.btnExportToJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportToJSON.Location = new System.Drawing.Point(3, 346);
            this.btnExportToJSON.Name = "btnExportToJSON";
            this.btnExportToJSON.Size = new System.Drawing.Size(351, 29);
            this.btnExportToJSON.TabIndex = 7;
            this.btnExportToJSON.Text = "Export to JSON";
            this.btnExportToJSON.UseVisualStyleBackColor = true;
            this.btnExportToJSON.Click += new System.EventHandler(this.btnExportToJSON_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.CanShowVisualStyleGlyphs = false;
            this.propertyGrid1.Location = new System.Drawing.Point(360, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.propertyGrid1.SelectedObject = this.treeViewSelectedEntity;
            this.propertyGrid1.Size = new System.Drawing.Size(413, 369);
            this.propertyGrid1.TabIndex = 3;
            // 
            // treeViewSelectedEntity
            // 
            this.treeViewSelectedEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewSelectedEntity.Location = new System.Drawing.Point(3, 3);
            this.treeViewSelectedEntity.Name = "treeViewSelectedEntity";
            this.treeViewSelectedEntity.Size = new System.Drawing.Size(351, 341);
            this.treeViewSelectedEntity.TabIndex = 0;
            this.treeViewSelectedEntity.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSelectedEntity_NodeMouseClick);
            this.treeViewSelectedEntity.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSelectedEntity_NodeMouseDoubleClick);
            // 
            // pictureBoxSelected
            // 
            this.pictureBoxSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSelected.Image = global::DemoACadSharp.Properties.Resources._376371757_161526873661412_820728656325930674_n;
            this.pictureBoxSelected.Location = new System.Drawing.Point(779, 3);
            this.pictureBoxSelected.Name = "pictureBoxSelected";
            this.pictureBoxSelected.Size = new System.Drawing.Size(175, 369);
            this.pictureBoxSelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSelected.TabIndex = 2;
            this.pictureBoxSelected.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.wallToolStripMenuItem,
            this.stairToolStripMenuItem,
            this.doorToolStripMenuItem,
            this.powerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 124);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.noneToolStripMenuItem.Text = "None";
            // 
            // wallToolStripMenuItem
            // 
            this.wallToolStripMenuItem.Name = "wallToolStripMenuItem";
            this.wallToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.wallToolStripMenuItem.Text = "Wall";
            // 
            // stairToolStripMenuItem
            // 
            this.stairToolStripMenuItem.Name = "stairToolStripMenuItem";
            this.stairToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.stairToolStripMenuItem.Text = "Stair";
            // 
            // doorToolStripMenuItem
            // 
            this.doorToolStripMenuItem.Name = "doorToolStripMenuItem";
            this.doorToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.doorToolStripMenuItem.Text = "Door";
            // 
            // powerToolStripMenuItem
            // 
            this.powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            this.powerToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.powerToolStripMenuItem.Text = "Power";
            // 
            // txtTopFloor
            // 
            this.txtTopFloor.Enabled = false;
            this.txtTopFloor.Location = new System.Drawing.Point(846, 40);
            this.txtTopFloor.Name = "txtTopFloor";
            this.txtTopFloor.Size = new System.Drawing.Size(123, 22);
            this.txtTopFloor.TabIndex = 18;
            // 
            // cbNumberFloor
            // 
            this.cbNumberFloor.FormattingEnabled = true;
            this.cbNumberFloor.Location = new System.Drawing.Point(544, 38);
            this.cbNumberFloor.Name = "cbNumberFloor";
            this.cbNumberFloor.Size = new System.Drawing.Size(143, 24);
            this.cbNumberFloor.TabIndex = 17;
            this.cbNumberFloor.Text = "1";
            this.cbNumberFloor.SelectedIndexChanged += new System.EventHandler(this.cbNumberFloor_SelectedIndexChanged);
            // 
            // txtNameHouse
            // 
            this.txtNameHouse.Enabled = false;
            this.txtNameHouse.Location = new System.Drawing.Point(168, 40);
            this.txtNameHouse.Name = "txtNameHouse";
            this.txtNameHouse.Size = new System.Drawing.Size(123, 22);
            this.txtNameHouse.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(745, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Top Floor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(373, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number Floors:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name Of House:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(693, 33);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(55, 37);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 515);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTopFloor);
            this.Controls.Add(this.cbNumberFloor);
            this.Controls.Add(this.txtNameHouse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStripLoadTime);
            this.Controls.Add(this.menuStripAllFeature);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.menuStripAllFeature;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStripAllFeature.ResumeLayout(false);
            this.menuStripAllFeature.PerformLayout();
            this.statusStripLoadTime.ResumeLayout(false);
            this.statusStripLoadTime.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbNail)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripAllFeature;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripLoadTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TreeView treeViewSelectedEntity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxThumbNail;
        private System.Windows.Forms.PictureBox pictureBoxSelected;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.TextBox txtTopFloor;
        private System.Windows.Forms.ComboBox cbNumberFloor;
        private System.Windows.Forms.TextBox txtNameHouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportToJSON;
        private System.Windows.Forms.ToolStripMenuItem exportJSONToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd;
    }
}