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
            ManageProject manageProject = new ManageProject();
            Project newProject = new Project(txtProjectName.Text, txtPath.Text, DateTime.Now);

            try
            {

                // Xử lý appData
                manageProject.EnsureAppFolderExists(newProject);

                // Xử lý tạo project tại thư mục được chọn
                string fullPath = Path.Combine(newProject.Path, newProject.NameProject);
                // Kiểm tra xem thư mục đã tồn tại chưa
                if (!Directory.Exists(fullPath))
                {
                    // Tạo thư mục mới
                    Directory.CreateDirectory(fullPath);
                    string jsonFilePath = Path.Combine(fullPath, $"{newProject.NameProject}.json");
                    File.WriteAllText(jsonFilePath, "[]");
                    string ImageFolder = Path.Combine(fullPath, "Image");
                    Directory.CreateDirectory(ImageFolder);
                    MainForm mainForm = new MainForm();
                    MainForm._isCreate = true;
                    MainForm._pathProject = txtPath.Text;
                    MainForm._nameHouse = txtNameHouse.Text;
                    MainForm._pathJsonFile = jsonFilePath;
                    MainForm._pathfolderImage = ImageFolder;
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
