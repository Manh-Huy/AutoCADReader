namespace DemoACadSharp
{
    partial class ReadFileAutoCadForm
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
            this.buttonHandleSelectedEntities = new System.Windows.Forms.Button();
            this.tableLayoutPanelEntityList = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDisplayClassifySelectedEntitiesByType = new System.Windows.Forms.Button();
            this.buttonDisplaySelectedEntitiesInFile = new System.Windows.Forms.Button();
            this.buttonDisplayEntitiesInFile = new System.Windows.Forms.Button();
            this.buttonClassifySelectedEntitiesByType = new System.Windows.Forms.Button();
            this.buttonSelectEntities = new System.Windows.Forms.Button();
            this.buttonDisplayEntities = new System.Windows.Forms.Button();
            this.buttonDisplayAllEntitiesProperties = new System.Windows.Forms.Button();
            this.buttonInputFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonHandleSelectedEntities
            // 
            this.buttonHandleSelectedEntities.Location = new System.Drawing.Point(861, 39);
            this.buttonHandleSelectedEntities.Name = "buttonHandleSelectedEntities";
            this.buttonHandleSelectedEntities.Size = new System.Drawing.Size(153, 51);
            this.buttonHandleSelectedEntities.TabIndex = 32;
            this.buttonHandleSelectedEntities.Text = "Handle Selected Entities";
            this.buttonHandleSelectedEntities.UseVisualStyleBackColor = true;
            this.buttonHandleSelectedEntities.Click += new System.EventHandler(this.buttonHandleSelectedEntities_Click);
            // 
            // tableLayoutPanelEntityList
            // 
            this.tableLayoutPanelEntityList.ColumnCount = 2;
            this.tableLayoutPanelEntityList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEntityList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEntityList.Location = new System.Drawing.Point(98, 226);
            this.tableLayoutPanelEntityList.Name = "tableLayoutPanelEntityList";
            this.tableLayoutPanelEntityList.RowCount = 2;
            this.tableLayoutPanelEntityList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEntityList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEntityList.Size = new System.Drawing.Size(848, 353);
            this.tableLayoutPanelEntityList.TabIndex = 31;
            // 
            // buttonDisplayClassifySelectedEntitiesByType
            // 
            this.buttonDisplayClassifySelectedEntitiesByType.Location = new System.Drawing.Point(605, 110);
            this.buttonDisplayClassifySelectedEntitiesByType.Name = "buttonDisplayClassifySelectedEntitiesByType";
            this.buttonDisplayClassifySelectedEntitiesByType.Size = new System.Drawing.Size(220, 51);
            this.buttonDisplayClassifySelectedEntitiesByType.TabIndex = 29;
            this.buttonDisplayClassifySelectedEntitiesByType.Text = "Display Classify Selected Entities By Type (file text)";
            this.buttonDisplayClassifySelectedEntitiesByType.UseVisualStyleBackColor = true;
            this.buttonDisplayClassifySelectedEntitiesByType.Click += new System.EventHandler(this.buttonDisplayClassifySelectedEntitiesByType_Click);
            // 
            // buttonDisplaySelectedEntitiesInFile
            // 
            this.buttonDisplaySelectedEntitiesInFile.Location = new System.Drawing.Point(440, 110);
            this.buttonDisplaySelectedEntitiesInFile.Name = "buttonDisplaySelectedEntitiesInFile";
            this.buttonDisplaySelectedEntitiesInFile.Size = new System.Drawing.Size(135, 51);
            this.buttonDisplaySelectedEntitiesInFile.TabIndex = 28;
            this.buttonDisplaySelectedEntitiesInFile.Text = "Display Selected Entities (file text)";
            this.buttonDisplaySelectedEntitiesInFile.UseVisualStyleBackColor = true;
            this.buttonDisplaySelectedEntitiesInFile.Click += new System.EventHandler(this.buttonDisplaySelectedEntitiesInFile_Click);
            // 
            // buttonDisplayEntitiesInFile
            // 
            this.buttonDisplayEntitiesInFile.Location = new System.Drawing.Point(176, 110);
            this.buttonDisplayEntitiesInFile.Name = "buttonDisplayEntitiesInFile";
            this.buttonDisplayEntitiesInFile.Size = new System.Drawing.Size(135, 51);
            this.buttonDisplayEntitiesInFile.TabIndex = 27;
            this.buttonDisplayEntitiesInFile.Text = "Display Unique Entities (file text)";
            this.buttonDisplayEntitiesInFile.UseVisualStyleBackColor = true;
            this.buttonDisplayEntitiesInFile.Click += new System.EventHandler(this.buttonDisplayEntitiesInFile_Click);
            // 
            // buttonClassifySelectedEntitiesByType
            // 
            this.buttonClassifySelectedEntitiesByType.Location = new System.Drawing.Point(639, 39);
            this.buttonClassifySelectedEntitiesByType.Name = "buttonClassifySelectedEntitiesByType";
            this.buttonClassifySelectedEntitiesByType.Size = new System.Drawing.Size(149, 51);
            this.buttonClassifySelectedEntitiesByType.TabIndex = 26;
            this.buttonClassifySelectedEntitiesByType.Text = "Classify Selected Entities By Type";
            this.buttonClassifySelectedEntitiesByType.UseVisualStyleBackColor = true;
            this.buttonClassifySelectedEntitiesByType.Click += new System.EventHandler(this.buttonClassifySelectedEntitiesByType_Click);
            // 
            // buttonSelectEntities
            // 
            this.buttonSelectEntities.Location = new System.Drawing.Point(451, 39);
            this.buttonSelectEntities.Name = "buttonSelectEntities";
            this.buttonSelectEntities.Size = new System.Drawing.Size(110, 51);
            this.buttonSelectEntities.TabIndex = 25;
            this.buttonSelectEntities.Text = "Select Entities";
            this.buttonSelectEntities.UseVisualStyleBackColor = true;
            this.buttonSelectEntities.Click += new System.EventHandler(this.buttonSelectEntities_Click);
            // 
            // buttonDisplayEntities
            // 
            this.buttonDisplayEntities.Location = new System.Drawing.Point(306, 39);
            this.buttonDisplayEntities.Name = "buttonDisplayEntities";
            this.buttonDisplayEntities.Size = new System.Drawing.Size(99, 51);
            this.buttonDisplayEntities.TabIndex = 24;
            this.buttonDisplayEntities.Text = "Display Entities";
            this.buttonDisplayEntities.UseVisualStyleBackColor = true;
            this.buttonDisplayEntities.Click += new System.EventHandler(this.buttonDisplayEntities_Click);
            // 
            // buttonDisplayAllEntitiesProperties
            // 
            this.buttonDisplayAllEntitiesProperties.Location = new System.Drawing.Point(23, 110);
            this.buttonDisplayAllEntitiesProperties.Name = "buttonDisplayAllEntitiesProperties";
            this.buttonDisplayAllEntitiesProperties.Size = new System.Drawing.Size(135, 51);
            this.buttonDisplayAllEntitiesProperties.TabIndex = 23;
            this.buttonDisplayAllEntitiesProperties.Text = "Display All Properties Entities (file text)";
            this.buttonDisplayAllEntitiesProperties.UseVisualStyleBackColor = true;
            this.buttonDisplayAllEntitiesProperties.Click += new System.EventHandler(this.buttonDisplayAllEntitiesProperties_Click);
            // 
            // buttonInputFile
            // 
            this.buttonInputFile.Location = new System.Drawing.Point(115, 39);
            this.buttonInputFile.Name = "buttonInputFile";
            this.buttonInputFile.Size = new System.Drawing.Size(96, 51);
            this.buttonInputFile.TabIndex = 22;
            this.buttonInputFile.Text = "Input File (dwg)";
            this.buttonInputFile.UseVisualStyleBackColor = true;
            this.buttonInputFile.Click += new System.EventHandler(this.buttonInputFile_Click);
            // 
            // ReadFileAutoCadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 642);
            this.Controls.Add(this.buttonHandleSelectedEntities);
            this.Controls.Add(this.tableLayoutPanelEntityList);
            this.Controls.Add(this.buttonDisplayClassifySelectedEntitiesByType);
            this.Controls.Add(this.buttonDisplaySelectedEntitiesInFile);
            this.Controls.Add(this.buttonDisplayEntitiesInFile);
            this.Controls.Add(this.buttonClassifySelectedEntitiesByType);
            this.Controls.Add(this.buttonSelectEntities);
            this.Controls.Add(this.buttonDisplayEntities);
            this.Controls.Add(this.buttonDisplayAllEntitiesProperties);
            this.Controls.Add(this.buttonInputFile);
            this.Name = "ReadFileAutoCadForm";
            this.Text = "ReadFileAutoCadForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHandleSelectedEntities;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEntityList;
        private System.Windows.Forms.Button buttonDisplayClassifySelectedEntitiesByType;
        private System.Windows.Forms.Button buttonDisplaySelectedEntitiesInFile;
        private System.Windows.Forms.Button buttonDisplayEntitiesInFile;
        private System.Windows.Forms.Button buttonClassifySelectedEntitiesByType;
        private System.Windows.Forms.Button buttonSelectEntities;
        private System.Windows.Forms.Button buttonDisplayEntities;
        private System.Windows.Forms.Button buttonDisplayAllEntitiesProperties;
        private System.Windows.Forms.Button buttonInputFile;
    }
}