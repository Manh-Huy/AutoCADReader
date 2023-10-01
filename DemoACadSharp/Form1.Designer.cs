namespace DemoACadSharp
{
    partial class Form1
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
            this.inputButton = new System.Windows.Forms.Button();
            this.lblpathout = new System.Windows.Forms.Label();
            this.outputButton = new System.Windows.Forms.Button();
            this.convertButton = new System.Windows.Forms.Button();
            this.outputConvertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputButton
            // 
            this.inputButton.Location = new System.Drawing.Point(218, 100);
            this.inputButton.Name = "inputButton";
            this.inputButton.Size = new System.Drawing.Size(105, 66);
            this.inputButton.TabIndex = 0;
            this.inputButton.Text = "Input";
            this.inputButton.UseVisualStyleBackColor = true;
            this.inputButton.Click += new System.EventHandler(this.inputButton_Click);
            // 
            // lblpathout
            // 
            this.lblpathout.AutoSize = true;
            this.lblpathout.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpathout.Location = new System.Drawing.Point(171, 222);
            this.lblpathout.Name = "lblpathout";
            this.lblpathout.Size = new System.Drawing.Size(128, 29);
            this.lblpathout.TabIndex = 1;
            this.lblpathout.Text = "link output:";
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(380, 100);
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(105, 66);
            this.outputButton.TabIndex = 2;
            this.outputButton.Text = "Output";
            this.outputButton.UseVisualStyleBackColor = true;
            this.outputButton.Click += new System.EventHandler(this.outputButton_Click);
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(218, 273);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(105, 66);
            this.convertButton.TabIndex = 3;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // outputConvertButton
            // 
            this.outputConvertButton.Location = new System.Drawing.Point(380, 273);
            this.outputConvertButton.Name = "outputConvertButton";
            this.outputConvertButton.Size = new System.Drawing.Size(105, 66);
            this.outputConvertButton.TabIndex = 4;
            this.outputConvertButton.Text = "Output Convert";
            this.outputConvertButton.UseVisualStyleBackColor = true;
            this.outputConvertButton.Click += new System.EventHandler(this.outputConvertButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.outputConvertButton);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputButton);
            this.Controls.Add(this.lblpathout);
            this.Controls.Add(this.inputButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button inputButton;
        private System.Windows.Forms.Label lblpathout;
        private System.Windows.Forms.Button outputButton;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.Button outputConvertButton;
    }
}

