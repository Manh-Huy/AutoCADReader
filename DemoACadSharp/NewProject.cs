using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoACadSharp
{
    public partial class NewProject : Form
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    txtPath.Text = selectedPath;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string folderName = txtProjectName.Text;

            // Lấy đường dẫn đầy đủ của thư mục muốn tạo
            string folderPath = txtPath.Text;

            try
            {
                string fullPath = Path.Combine(folderPath, folderName);
                // Kiểm tra xem thư mục đã tồn tại chưa
                if (!Directory.Exists(fullPath))
                {
                    // Tạo thư mục mới
                    Directory.CreateDirectory(fullPath);
                    string ImageFolder = Path.Combine(fullPath, "Image");
                    Directory.CreateDirectory(ImageFolder);
                    MainForm mainForm = new MainForm();
                    MainForm._pathProject = txtPath.Text;
                    MainForm._nameHouse = txtNameHouse.Text;
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Project already exists!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
