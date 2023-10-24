using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DemoACadSharp
{
    public class CustomMessageBox : Form
    {
        private Button importButton;
        private Button openButton;
        private Label messageLabel;

        public CustomMessageBox(string message)
        {
            InitializeComponents();
            messageLabel.Text = message;
        }

        private void InitializeComponents()
        {
            importButton = new Button();
            importButton.Text = "Import";
            importButton.Click += ImportButton_Click;

            openButton = new Button();
            openButton.Text = "Open";
            openButton.Click += OpenButton_Click;

            messageLabel = new Label();
            messageLabel.AutoSize = true;
            messageLabel.Location = new System.Drawing.Point(20, 20);

            Controls.Add(importButton);
            Controls.Add(openButton);
            Controls.Add(messageLabel);

            importButton.Location = new System.Drawing.Point(20, 50);
            openButton.Location = new System.Drawing.Point(110, 50);

            // Tính toán kích thước của cửa sổ
            int width = 220;
            int height = 120;
            Size = new System.Drawing.Size(width, height);

            // Cấu hình các thuộc tính của cửa sổ
            Text = "Custom MessageBox";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn vào nút Import
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // Xử lý khi nhấn vào nút Open
            DialogResult = DialogResult.Retry;
            Close();
        }
    }
}
