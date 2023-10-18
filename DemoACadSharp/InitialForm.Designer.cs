namespace DemoACadSharp
{
    partial class InitialForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameHouse = new System.Windows.Forms.TextBox();
            this.txtNumberFloors = new System.Windows.Forms.TextBox();
            this.cbTopFloor = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name Of House:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number Of Floors:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Top Floor:";
            // 
            // txtNameHouse
            // 
            this.txtNameHouse.Location = new System.Drawing.Point(216, 40);
            this.txtNameHouse.Name = "txtNameHouse";
            this.txtNameHouse.Size = new System.Drawing.Size(142, 22);
            this.txtNameHouse.TabIndex = 3;
            // 
            // txtNumberFloors
            // 
            this.txtNumberFloors.Location = new System.Drawing.Point(216, 126);
            this.txtNumberFloors.Name = "txtNumberFloors";
            this.txtNumberFloors.Size = new System.Drawing.Size(142, 22);
            this.txtNumberFloors.TabIndex = 4;
            // 
            // cbTopFloor
            // 
            this.cbTopFloor.FormattingEnabled = true;
            this.cbTopFloor.Items.AddRange(new object[] {
            "Rooftop",
            "Roof"});
            this.cbTopFloor.Location = new System.Drawing.Point(216, 221);
            this.cbTopFloor.Name = "cbTopFloor";
            this.cbTopFloor.Size = new System.Drawing.Size(142, 24);
            this.cbTopFloor.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(140, 274);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(98, 41);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 327);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbTopFloor);
            this.Controls.Add(this.txtNumberFloors);
            this.Controls.Add(this.txtNameHouse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InitialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InitialForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameHouse;
        private System.Windows.Forms.TextBox txtNumberFloors;
        private System.Windows.Forms.ComboBox cbTopFloor;
        private System.Windows.Forms.Button btnConfirm;
    }
}