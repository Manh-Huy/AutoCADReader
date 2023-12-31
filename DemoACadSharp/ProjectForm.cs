﻿using Aspose.CAD.Xmp.Schemas.XmpDm;
using Newtonsoft.Json;
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
    public partial class ProjectForm : Form
    {
        int rowNumber = 0;
        bool isDeleteProject = false;

        public ProjectForm()
        {
            InitializeComponent();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewProject f = new NewProject();
            f.ShowDialog();
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ManageProject manageProject = new ManageProject();
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Chọn một thư mục";
                folderBrowserDialog.ShowNewFolderButton = true; // Cho phép tạo thư mục mới
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer; // Thư mục mặc định khi mở dialog

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Lấy đường dẫn đầy đủ của thư mục được chọn
                        string selectedFolderPath = folderBrowserDialog.SelectedPath;

                        string folderPathWithoutFileName = Path.GetDirectoryName(selectedFolderPath);

                        // Lấy tên của thư mục từ đường dẫn
                        string folderName = Path.GetFileName(selectedFolderPath);


                        if (isCorrectFormatFolder(selectedFolderPath) &&
                            manageProject.isExistProjectInAppData(folderPathWithoutFileName, folderName))
                        {
                            string jsonFileName = Path.GetFileName(selectedFolderPath) + ".json";
                            string ImageFolder = Path.Combine(selectedFolderPath, "Image");
                            MainForm f = new MainForm();
                            MainForm._isOpen = true;
                            MainForm._nameProject = folderName;
                            MainForm._pathProject = folderPathWithoutFileName;
                            MainForm._pathJsonFile = Path.Combine(selectedFolderPath, jsonFileName);
                            MainForm._pathfolderImage = ImageFolder;
                            f.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("This project is not valid!");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private bool isCorrectFormatFolder(string folderPath)
        {
            if (HasJsonFile(folderPath) && HasImageFolder(folderPath))
            { return true; }
            return false;
        }

        private bool HasJsonFile(string folderPath)
        {
            // Kiểm tra xem có ít nhất một tệp tin JSON trong thư mục hay không
            return Directory.GetFiles(folderPath, "*.json").Length > 0;
        }

        private bool HasImageFolder(string folderPath)
        {
            // Kiểm tra xem có thư mục có tên là "Image" hay không
            return Directory.GetDirectories(folderPath, "Image").Length > 0;
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            LoadRecentProject(isDeleteProject);
        }

        private void LoadRecentProject(bool isDelete)
        {
            ManageProject manageProject = new ManageProject();
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appNameFoler = Path.Combine(appDataFolder, "HKTArchitecture");
            bool isExist = manageProject.GetAppDataFolderPath(appDataFolder, appNameFoler);
            if (isExist)
            {
                string filePath = Path.Combine(appNameFoler, "ListProject.json");

                if (File.Exists(filePath) || Directory.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    manageProject.ListProject = JsonConvert.DeserializeObject<List<Project>>(jsonContent);
                    manageProject.ListProject.Sort((p1, p2) => p2.DateTime.CompareTo(p1.DateTime));
                    List<Project> recentProjects = manageProject.ListProject.Take(5).ToList();

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("ProjectName", typeof(string));
                    dataTable.Columns.Add("Path", typeof(string));
                    dataTable.Columns.Add("DateTime", typeof(DateTime));

                    foreach (Project project in recentProjects)
                    {
                        dataTable.Rows.Add(project.NameProject, project.Path, project.DateTime);
                    }

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Columns["ProjectName"].Width = 160;
                    dataGridView1.Columns["Path"].Width = 230;
                    dataGridView1.Columns["DateTime"].Width = 130;

                    if (isDelete == false)
                    {
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.Name = "Options";
                        buttonColumn.HeaderText = "Options";
                        buttonColumn.Text = "..."; // Văn bản hiển thị trên nút
                        buttonColumn.UseColumnTextForButtonValue = true;

                        // Thêm cột nút vào DataGridView
                        dataGridView1.Columns.Add(buttonColumn);
                        dataGridView1.Columns["Options"].Width = 44;
                    }

                    dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Thiết lập căn giữa cho văn bản trong các ô
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Visible = true;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 3 && e.ColumnIndex != dataGridView1.Columns["Options"].Index) // Kiểm tra người dùng đã nhấp vào một dòng hợp lệ hay không
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong dòng
                string name = row.Cells["ProjectName"].Value.ToString();
                string path = row.Cells["Path"].Value.ToString();

                try
                {
                    ManageProject manageProject = new ManageProject();

                    // Lấy đường dẫn đầy đủ của thư mục được chọn
                    string selectedFolderPath = Path.Combine(path, name);

                    string folderPathWithoutFileName = Path.GetDirectoryName(selectedFolderPath);

                    if (isCorrectFormatFolder(selectedFolderPath) &&
                        manageProject.isExistProjectInAppData(folderPathWithoutFileName, name))
                    {
                        string jsonFileName = Path.GetFileName(selectedFolderPath) + ".json";
                        string ImageFolder = Path.Combine(selectedFolderPath, "Image");
                        MainForm f = new MainForm();
                        MainForm._isOpen = true;
                        MainForm._nameProject = name;
                        MainForm._pathProject = folderPathWithoutFileName;
                        MainForm._pathJsonFile = Path.Combine(selectedFolderPath, jsonFileName);
                        MainForm._pathfolderImage = ImageFolder;
                        f.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("This project is not valid!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                DataGridViewButtonCell buttonCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                if (buttonCell != null) // Xác định xem ô nút được nhấp
                {
                    if (dataGridView1.ContextMenuStrip != null)
                    {
                        dataGridView1.ContextMenuStrip.Show(dataGridView1, dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location); // Hiển thị ContextMenuStrip tại vị trí của ô nút
                        rowNumber = e.RowIndex;
                    }
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            if (clickedItem != null)
            {
                string itemName = clickedItem.Text;
                if (itemName == "Remove Project From List")
                {
                    isDeleteProject = true;

                    DataGridViewRow row = dataGridView1.Rows[rowNumber];
                    string projectName = row.Cells["ProjectName"].Value.ToString();
                    string path = row.Cells["Path"].Value.ToString();
                    DateTime dateTime = (DateTime)row.Cells["DateTime"].Value;
                    Project currentProject = new Project(projectName, path, dateTime);
                    ManageProject manageProject = new ManageProject();
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string appNameFoler = Path.Combine(appDataFolder, "HKTArchitecture");
                    bool isExist = manageProject.GetAppDataFolderPath(appDataFolder, appNameFoler);

                    if (isExist)
                    {
                        List<Project> tempList = new List<Project>();
                        string filePath = Path.Combine(appNameFoler, "ListProject.json");
                        if (File.Exists(filePath) || Directory.Exists(filePath))
                        {
                            string jsonContent = File.ReadAllText(filePath);
                            manageProject.ListProject = JsonConvert.DeserializeObject<List<Project>>(jsonContent);
                            foreach (Project project in manageProject.ListProject)
                            {
                                if (project.NameProject != currentProject.NameProject)
                                {
                                    tempList.Add(project);
                                }
                            }
                        }

                        manageProject.ListProject.Clear();
                        manageProject.ListProject = tempList;

                        File.WriteAllText(filePath, JsonConvert.SerializeObject(manageProject.ListProject, Formatting.Indented));

                        LoadRecentProject(isDeleteProject);
                    }
                }
            }
        }
    }


}
