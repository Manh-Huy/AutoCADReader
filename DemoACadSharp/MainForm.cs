using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;
using Aspose.CAD.FileFormats.Cad;
using Aspose.CAD.ImageOptions;
using CSMath;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Color = System.Drawing.Color;
using Line = ACadSharp.Entities.Line;
using System.Diagnostics;

namespace DemoACadSharp
{
    public partial class MainForm : Form
    {
        Architecture _architecture = Architecture.getInstance();

        List<AcadEntity> _listAllEntities = new List<AcadEntity>();

        List<AcadEntity> _listUniqueEntities = new List<AcadEntity>();

        int _currentFloor = 1;
        private TreeNode _selectedNode;

        const string _TempFolderPath = "..\\..\\Temp\\";
        const string _PictureFolderPath = "..\\..\\Picture Autocad2D\\";
        const string _UnityPath = "..\\..\\Build\\";
        const string _UnityExePath = "..\\..\\Build\\CreateObjectByCode.exe";
        const string _DebugBuildJsonPath = "..\\..\\bin\\Debug\\Build\\house2.json";
        public enum UnityEntitiesEnum
        {
            None,
            Wall,
            Door,
            Stair,
            Window,
            Power,
            Remove
        }

        public MainForm()
        {
            InitializeComponent();
            PopulateContextMenuStrip();
            if (!Directory.Exists(_TempFolderPath))
            {
                Directory.CreateDirectory(_TempFolderPath);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cbNumberFloor.Text = 1.ToString();
            cbNumberFloor.Items.Add(1);
        }


        #region Event

        private void importFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importFile();
        }

        private void importFile()
        {
            //gán tầng
            _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            // Dọn Cache
            _listAllEntities.Clear();
            _listUniqueEntities.Clear();
            treeView1.Nodes.Clear();
            treeViewSelectedEntity.Nodes.Clear();

            // Hiển thị hộp thoại mở tệp và cho phép người dùng chọn tệp DWG
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AutoCAD Drawing Files (*.dwg)|*.dwg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                _listAllEntities = GetAllEntities(filePath);

                _listAllEntities = clearLayer0(_listAllEntities);

                // Chỉ lấy Entity Đại Diện
                _listUniqueEntities = _listAllEntities
                    .GroupBy(entity => new { entity.LayerName, entity.ObjectType })
                    .Select(group => new AcadEntity(null, group.Key.LayerName, group.Key.ObjectType, null))
                    .ToList();

                _architecture.Floors[_currentFloor].ListAllEntities = new List<AcadEntity>(_listAllEntities);
                _architecture.Floors[_currentFloor].ListUniqueEntities = new List<AcadEntity>(_listUniqueEntities);

                //Thêm Entity vào TreeView
                AddEntityToHierachy(_listUniqueEntities, _listAllEntities);

                //Load ảnh
                using (CadImage cadImage = (CadImage)Aspose.CAD.Image.Load(filePath))
                {
                    // Create output options for the image (e.g., PNG)
                    PngOptions pngOptions = new PngOptions();

                    // Save the image as PNG
                    string fileName = GenerateRandomImageName(8) + ".png";
                    string outputPath = _TempFolderPath + fileName; // Output file path
                    _architecture.Floors[_currentFloor].ImageURL = outputPath;
                    cadImage.Save(outputPath, pngOptions);

                    // Display the image on the PictureBox
                    using (Bitmap bitmap = new Bitmap(outputPath))
                    {
                        pictureBoxThumbNail.Image = new Bitmap(bitmap);
                        pictureBoxThumbNail.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
        }

        private List<AcadEntity> clearLayer0(List<AcadEntity> listEntities)
        {
            List<AcadEntity> listNo0Entities = new List<AcadEntity>();
            foreach (AcadEntity entity in listEntities)
            {
                if (entity.LayerName != "0")
                {
                    listNo0Entities.Add(entity);
                }
            }

            return listNo0Entities;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            // Mở hộp thoại OpenFileDialog để chọn tệp JSON
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
                        _listAllEntities.Clear();

                        string json = File.ReadAllText(openFileDialog.FileName);
                        Floor floor = JsonConvert.DeserializeObject<Floor>(json);

                        foreach (AcadEntity token in floor.ListAllEntities)
                        {
                            AcadEntity entity = token;
                            _listAllEntities.Add(entity);
                        }

                        _architecture.Floors[_currentFloor].Order = _currentFloor + 1;
                        _architecture.Floors[_currentFloor].ListAllEntities = new List<AcadEntity>(_listAllEntities);
                        _listUniqueEntities = _architecture.Floors[_currentFloor].getUniqueEntities();
                        _architecture.Floors[_currentFloor].ListUniqueEntities = new List<AcadEntity>(_listUniqueEntities);
                        _architecture.Floors[_currentFloor].ImageURL = floor.ImageURL;
                        string imagePath = floor.ImageURL;
                        pictureBoxThumbNail.Image = LoadImage(imagePath);

                        setDataToTreeView_View(_currentFloor);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc tệp JSON: " + ex.Message);
                    }


                }
            }
        }

        //Đổ data lên TreeView_View
        private void setDataToTreeView_View(int _currentFloor)
        {

            treeView1.Nodes.Clear();
            treeView1.CheckBoxes = true;


            foreach (AcadEntity parentEntity in _architecture.Floors[_currentFloor].ListUniqueEntities)
            {
                TreeNode parentNode = treeView1.Nodes.Add($"{parentEntity.LayerName} ({parentEntity.ObjectType})");


                foreach (AcadEntity childEntity in _architecture.Floors[_currentFloor].ListAllEntities)
                {
                    if (parentEntity.LayerName == childEntity.LayerName && parentEntity.ObjectType == childEntity.ObjectType)
                    {
                        TreeNode childNode = parentNode.Nodes.Add($"{childEntity.Id}: {childEntity.LayerName} ({childEntity.ObjectType})");
                    }
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Architecture.getInstance().Floors[_currentFloor].ImageURL = SaveImageToLocalFolder(pictureBoxThumbNail);
            string json = JsonConvert.SerializeObject(Architecture.getInstance().Floors[_currentFloor], Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.Title = "Save JSON File";



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, json);
                MessageBox.Show("Save Successfully!");
                //DeleteTempFolder(_TempFolderPath);
            }
        }

        private void DeleteTempFolder(string folderPath)
        {
            try
            {
                // Check if the folder exists
                if (System.IO.Directory.Exists(folderPath))
                {
                    // Get a list of all files in the folder
                    string[] files = System.IO.Directory.GetFiles(folderPath);

                    // Delete each file in the folder
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);
                    }

                    MessageBox.Show("All files in the folder have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool isHaveCheck = false;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Checked)
                {
                    isHaveCheck = true;
                    break;
                }
            }
            if (isHaveCheck)
            {
                getDataFromTreeView_View();
                setDataToTreeView_Config();
                _architecture.Floors[_currentFloor].ListUnityEntities = SynchronizedListWhenReselect();
                tabControl1.SelectedIndex = 1;
                propertyGrid1.SelectedObject = null;
                MessageBox.Show("Selected Successfully!", "Select Entity", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You have not selected any Entity yet!!");
            }
        }

        // Hàm này con chó nào đụng dzô tao đấm chếch mọe à
        private void getDataFromTreeView_View()
        {
            _architecture.Floors[_currentFloor].ListSelectedEntities.Clear();
            _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            Stack<TreeNode> nodeStack = new Stack<TreeNode>();

            foreach (TreeNode node in treeView1.Nodes)
            {
                nodeStack.Push(node);
            }

            while (nodeStack.Count > 0)
            {
                TreeNode currentNode = nodeStack.Pop();

                if (currentNode.Checked)
                {
                    string layerName = getLayer(currentNode.Text);
                    string objectType = getObjectType(currentNode.Text);

                    //Lấy node con
                    foreach (TreeNode childNode in currentNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            int idChildEntity = getIdEntity(childNode.Text);
                            foreach (AcadEntity childEntity in _architecture.Floors[_currentFloor].ListAllEntities)
                            {
                                if (childEntity.LayerName == layerName && childEntity.ObjectType == objectType &&
                                     idChildEntity == childEntity.Id)
                                {
                                    if (!_architecture.Floors[_currentFloor].ListSelectedEntities.Contains(childEntity))
                                    {
                                        _architecture.Floors[_currentFloor].ListSelectedEntities.Add(childEntity);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            _architecture.Floors[_currentFloor].ListSelectedEntities.Reverse();
        }

        private List<UnityEntity> SynchronizedListWhenReselect()
        {
            List<UnityEntity> newList = new List<UnityEntity>();
            if (_architecture.Floors[_currentFloor].ListUnityEntities.Count > 0)
            {
                List<AcadEntity> listUniqueSelectedEntity = _architecture.Floors[_currentFloor].getUniqueSelectedEntities();

                foreach (AcadEntity uniqueSelectedEntity in listUniqueSelectedEntity)
                {
                    foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                    {
                        if (uniqueSelectedEntity.LayerName == unityEntity.LayerName && uniqueSelectedEntity.ObjectType == unityEntity.ObjectType)
                        {
                            newList.Add(unityEntity);
                        }
                    }
                }
            }
            else return _architecture.Floors[_currentFloor].ListUnityEntities;

            return newList;
        }
        private void setDataToTreeView_Config()
        {
            treeViewSelectedEntity.Nodes.Clear();
            int _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            List<AcadEntity> listUniqueSelectedEntity = _architecture.Floors[_currentFloor].getUniqueSelectedEntities();

            if (listUniqueSelectedEntity != null)
            {
                foreach (AcadEntity entity in listUniqueSelectedEntity)
                {
                    TreeNode parentNode = treeViewSelectedEntity.Nodes.Add($"{entity.LayerName} ({entity.ObjectType})");
                    if (_architecture.Floors[_currentFloor].ListUnityEntities.Count > 0)
                    {
                        foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                        {
                            if (unityEntity.LayerName == entity.LayerName && unityEntity.ObjectType == entity.ObjectType
                                && unityEntity.TypeOfUnityEntity != "")
                            {
                                parentNode.ForeColor = Color.OrangeRed;
                                parentNode.NodeFont = new Font(treeViewSelectedEntity.Font, FontStyle.Underline);
                                break;
                            }
                        }
                    }


                    foreach (AcadEntity childEntity in Architecture.getInstance().Floors[_currentFloor].ListSelectedEntities)
                    {
                        if (entity.LayerName == childEntity.LayerName && entity.ObjectType == childEntity.ObjectType)
                        {
                            TreeNode childNode = parentNode.Nodes.Add($"{childEntity.Id}: {childEntity.LayerName} ({childEntity.ObjectType})");
                        }
                    }
                }
            }
        }

        //sự kiện doubleClick vào node con
        private void treeViewSelectedEntity_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Floor floor = Architecture.getInstance().Floors[_currentFloor];

            if (e.Node.Nodes.Count == 0)
            {
                int idEntity = getIdEntity(e.Node.Text);
                string layerName = deleteId(getLayer(e.Node.Text));
                string objectType = getObjectType(e.Node.Text);
                bool seleted = false;
                foreach (UnityEntity unityEntity in floor.ListUnityEntities)
                {
                    if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType
                        && unityEntity.Id == idEntity)
                    {
                        seleted = true;
                        switch (unityEntity.TypeOfUnityEntity)
                        {
                            case "Wall":
                                if (unityEntity is Wall wall)
                                {
                                    if (wall.Id == idEntity)
                                        propertyGrid1.SelectedObject = wall;
                                }
                                break;
                            case "Stair":
                                if (unityEntity is Stair stair)

                                {
                                    if (stair.Id == idEntity)
                                        propertyGrid1.SelectedObject = stair;
                                }
                                break;
                            case "Door":
                                if (unityEntity is Door door)
                                {
                                    if (door.Id == idEntity)
                                        propertyGrid1.SelectedObject = door;
                                }
                                break;
                            case "Window":
                                if (unityEntity is Window window)
                                {
                                    if (window.Id == idEntity)
                                        propertyGrid1.SelectedObject = window;
                                }
                                break;
                            case "Power":
                                if (unityEntity is Power power)

                                {
                                    if (power.Id == idEntity)
                                        propertyGrid1.SelectedObject = power;
                                }
                                break;
                        }
                    }
                }
                if (seleted == false) propertyGrid1.SelectedObject = null;
            }
        }

        private void cbNumberFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            if (Architecture.getInstance().Floors[_currentFloor].ListAllEntities == null ||
                    Architecture.getInstance().Floors[_currentFloor].ListAllEntities.Count == 0)
            {
                treeView1.Nodes.Clear();
                treeViewSelectedEntity.Nodes.Clear();
                pictureBoxThumbNail.Image = null;

                CustomMessageBox customMessageBox = new CustomMessageBox("This floor does not have data yet!");
                DialogResult result = customMessageBox.ShowDialog();

                if (result == DialogResult.OK)
                {
                    importFile();
                }
                else if (result == DialogResult.Retry)
                {
                    openFile();
                }
            }
            else
            {
                setDataToTreeView_View(_currentFloor);
                setDataToTreeView_Config();
                string imgName = Architecture.getInstance().Floors[_currentFloor].ImageURL;
                pictureBoxThumbNail.Image = LoadImage(imgName);
            }
        }

        //Sự kiện bắt chuột trái 
        private void treeViewSelectedEntity_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectedNode = e.Node;
                if (e.Node != null && e.Node.Nodes.Count > 0)
                {
                    string layerName = getLayer(e.Node.Text);
                    string objectType = getObjectType(e.Node.Text);

                    //chưa chọn UnityEntity
                    if (_architecture.Floors[_currentFloor].ListUnityEntities.Count == 0)
                    {
                        SetNoneMenuItemToChecked();
                    }
                    else
                    {
                        foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                        {
                            bool isBreak = false; //break when found at least 1 same type
                            if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType)
                            {
                                foreach (ToolStripMenuItem toolStripMenuItem in contextMenuStrip1.Items)
                                {
                                    toolStripMenuItem.Checked = false;
                                    if (toolStripMenuItem.Text == unityEntity.TypeOfUnityEntity.ToString())
                                    {
                                        //clean option before checks
                                        foreach (ToolStripMenuItem toolStripMenuItem1 in contextMenuStrip1.Items)
                                        {
                                            toolStripMenuItem1.Checked = false;
                                        }
                                        toolStripMenuItem.Checked = true;
                                        isBreak = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                SetNoneMenuItemToChecked();
                            }
                            if (isBreak == true) break;
                        }
                    }

                    e.Node.ContextMenuStrip = contextMenuStrip1;
                }
            }
        }

        private void SetNoneMenuItemToChecked()
        {
            foreach (ToolStripMenuItem toolStripMenuItem in contextMenuStrip1.Items)
            {
                toolStripMenuItem.Checked = false;
                if (toolStripMenuItem.Text == "None")
                {
                    toolStripMenuItem.Checked = true;
                }
            }
        }

        //hàm chọn tất cả node con khi chọn node cha
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                // Lặp qua tất cả các node con và đặt trạng thái check của chúng giống với node cha
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = e.Node.Checked;
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            _currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Floor floor = Architecture.getInstance().Floors[_currentFloor];
            string layerName = "", objectType = "";
            string typeOfUnityEntity = e.ClickedItem.Text;
            string typeOfUnityEntityBefore = "";
            foreach (ToolStripMenuItem toolStripMenuItem in contextMenuStrip1.Items)
            {
                if (toolStripMenuItem.Checked)
                {
                    typeOfUnityEntityBefore = toolStripMenuItem.Text;
                }
            }

            if (_selectedNode != null)
            {
                layerName = getLayer(_selectedNode.Text);
                objectType = getObjectType(_selectedNode.Text);
            }

            if (typeOfUnityEntity == "Remove")
            {
                string treeNodeName = layerName + " (" + objectType + ")";
                foreach (TreeNode treeNode in treeViewSelectedEntity.Nodes)
                {
                    if (treeNode.Text == treeNodeName)
                    {
                        treeViewSelectedEntity.Nodes.Remove(treeNode);
                        if (typeOfUnityEntityBefore != "None")
                        {
                            _architecture.Floors[_currentFloor].ListUnityEntities = DeleteUnityEntityWhenRemoveTreeNode(layerName, objectType, typeOfUnityEntityBefore);
                        }
                        break;
                    }
                }
            }
            else
            {
                foreach (AcadEntity entity in floor.ListSelectedEntities)
                {
                    if (entity.LayerName == layerName && entity.ObjectType == objectType)
                    {
                        var templateEntity = FactoryUnityEntity.createObjectUnity(entity, typeOfUnityEntity);
                        _architecture.Floors[_currentFloor].ListUnityEntities = DeleteUnityEntityWhenChangeTypeUnity(layerName, objectType, templateEntity.Id, typeOfUnityEntity, typeOfUnityEntityBefore);
                        switch (templateEntity)
                        {
                            case Wall wall:
                                floor.ListUnityEntities.Add(wall);
                                break;

                            case Stair stair:
                                floor.ListUnityEntities.Add(stair);
                                break;

                            case Door door:
                                floor.ListUnityEntities.Add(door);
                                break;

                            case Window window:
                                floor.ListUnityEntities.Add(window);
                                break;

                            case Power power:
                                floor.ListUnityEntities.Add(power);
                                break;

                            case AcadEntity none:
                                break;
                        }
                    }
                }

                ResponsiveTreeNode(layerName, objectType, typeOfUnityEntity);
            }
        }

        private void btnExportToJSON_Click(object sender, EventArgs e)
        {
            UnityArchitecture unityArchitecture = new UnityArchitecture();
            unityArchitecture.NameArchitecture = txtNameHouse.Text;
            unityArchitecture.NumberOfFloor = _architecture.NumberOfFloor;
            unityArchitecture.TypeOfRoof = cbBoxTopRoof.Text;
            foreach (Floor floor in _architecture.Floors)
            {
                UnityFloor newFloor = new UnityFloor();
                newFloor.Order = floor.Order;
                newFloor.ListEntities = new List<UnityEntity>(floor.ListUnityEntities);
                unityArchitecture.ListFloor.Add(newFloor);
            }

            string json = JsonConvert.SerializeObject(unityArchitecture, Formatting.Indented);

            File.WriteAllText(_DebugBuildJsonPath, json);

            // Path to the Unity executable (Unity.exe)
            string unityPath = _UnityExePath;
            // Path to the Unity project folder
            string unityProjectPath = _UnityPath;
            // Command to run the Unity script
            string command = $"\"{unityPath}\" -projectPath \"{unityProjectPath}\" -executeMethod BuildAndRun.BuildAndRunGame";

            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            // Execute the Unity script to build and run
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
        }

        private void exportJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {

            UnityArchitecture unityArchitecture = new UnityArchitecture();
            unityArchitecture.NameArchitecture = txtNameHouse.Text;
            unityArchitecture.NumberOfFloor = _architecture.NumberOfFloor;
            unityArchitecture.TypeOfRoof = cbBoxTopRoof.Text;
            foreach (Floor floor in _architecture.Floors)
            {
                UnityFloor newFloor = new UnityFloor();
                newFloor.Order = floor.Order;
                newFloor.ListEntities = new List<UnityEntity>(floor.ListUnityEntities);
                unityArchitecture.ListFloor.Add(newFloor);
            }

            string json = JsonConvert.SerializeObject(unityArchitecture, Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.Title = "Save JSON File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, json);
                MessageBox.Show("Save Successfully!");
            }

        }

        #endregion

        #region 

        public List<AcadEntity> GetAllEntities(string file)
        {
            List<AcadEntity> listAllEntity = new List<AcadEntity>();

            using (DwgReader reader = new DwgReader(file))
            {
                CadDocument doc = reader.Read();
                int coutId = 0;
                foreach (Entity entity in doc.Entities)
                {
                    string layerName = entity.Layer.Name;
                    string objectType = entity.GetType().Name;
                    List<string> coordinates = new List<string>();

                    if (entity is LwPolyline)
                    {
                        LwPolyline lwPolyline = (LwPolyline)entity;

                        List<LwPolyline.Vertex> vertices = lwPolyline.Vertices;

                        foreach (LwPolyline.Vertex vertex in vertices)
                        {
                            XY position = vertex.Location;

                            string posString = position.ToString();
                            coordinates.Add(posString);
                        }
                    }

                    if (entity is Insert)
                    {
                        Insert insert = (Insert)entity;

                        XYZ position = insert.InsertPoint;

                        coordinates.Add(position.ToString());
                    }
                    if (entity is Line)
                    {
                        Line line = (Line)entity;

                        XYZ startPoint = line.StartPoint;

                        XYZ endPoint = line.EndPoint;

                        coordinates.Add(startPoint.ToString());

                        coordinates.Add(endPoint.ToString());
                    }
                    if (entity is Arc) // Cần không?
                    {
                        Arc arc = entity as Arc;

                        XYZ center = arc.Center;
                        double radius = arc.Radius;
                        double startAngle = arc.StartAngle;
                        double endAngle = arc.EndAngle;

                        coordinates.Add(center.ToString());
                        coordinates.Add(radius.ToString());
                        coordinates.Add(startAngle.ToString());
                        coordinates.Add(endAngle.ToString());
                    }
                    if (entity is Hatch)
                    {
                        Hatch originalHatch = entity as Hatch;

                        foreach (XY seedPoint in originalHatch.SeedPoints)
                        {
                            coordinates.Add(seedPoint.ToString());
                        }
                    }

                    AcadEntity entityInfo = new AcadEntity(coutId, layerName, objectType, coordinates);
                    listAllEntity.Add(entityInfo);
                    coutId++;
                }
            }
            return listAllEntity;
        }

        public void AddEntityToHierachy(List<AcadEntity> listUniqueEntities, List<AcadEntity> listAllEntities)
        {

            treeView1.CheckBoxes = true;

            foreach (AcadEntity UniEntity in listUniqueEntities)
            {
                TreeNode parentNode = new TreeNode($"{UniEntity.LayerName} ({UniEntity.ObjectType})");
                treeView1.Nodes.Add(parentNode);
                foreach (AcadEntity entity in listAllEntities)
                {
                    if (UniEntity.LayerName == entity.LayerName && UniEntity.ObjectType == entity.ObjectType)
                    {
                        TreeNode childNode = new TreeNode($"{entity.Id}: {entity.LayerName} ({entity.ObjectType})");
                        parentNode.Nodes.Add(childNode);
                        //childNode.Tag = entity;
                        //objectDictionary[childNode] = entity;
                    }
                }
                //toolStripProgressBar.Value++;
            }
        }

        private string getLayer(string input)
        {
            int openBracketIndex = input.IndexOf("(");

            if (openBracketIndex != -1)
            {
                string outsideBrackets = input.Substring(0, openBracketIndex).Trim();
                return outsideBrackets;
            }
            else
            {
                return null;
            }
        }

        private string getObjectType(string input)
        {
            int openBracketIndex = input.IndexOf("(");
            int closeBracketIndex = input.IndexOf(")");

            if (openBracketIndex != -1 && closeBracketIndex != -1)
            {
                string insideBrackets = input.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1).Trim();
                return insideBrackets;
            }
            else
            {
                return null;
            }
        }

        private int getIdEntity(string entity)
        {
            string extractedSubString;
            int indexOfColon = entity.IndexOf(':'); // Tìm vị trí của ký tự ':'
            int indexOfOpeningParenthesis = entity.IndexOf('('); // Tìm vị trí của ký tự '('

            if (indexOfColon != -1 && indexOfOpeningParenthesis != -1)
            {
                extractedSubString = entity.Substring(0, indexOfColon); // Trích xuất phần a từ vị trí 0 đến vị trí của ký tự ':'
                int result = int.Parse(extractedSubString);
                return result;
            }

            return 0;
        }

        //Hàm xử lý chuỗi trên node con
        private string deleteId(string input)
        {
            int colonIndex = input.IndexOf(':');
            if (colonIndex >= 0 && colonIndex < input.Length - 1)
            {
                string y = input.Substring(colonIndex + 1).Trim();
                return y;
            }
            else
            {
                // Trường hợp không tìm thấy ký tự ':' hoặc không có phần tử "y"
                // Xử lý hoặc trả về giá trị mặc định tùy thuộc vào logic của bạn
                return string.Empty;
            }
        }

        public string SaveImageToLocalFolder(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                string fileName = GenerateRandomImageName(8) + ".jpg";
                string folderPath = _PictureFolderPath;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                Bitmap image = new Bitmap(pictureBoxThumbNail.Image);

                image.Save(filePath);

                return filePath;
            }
            return null;
        }

        public string GenerateRandomImageName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] imageName = new char[length];

            for (int i = 0; i < length; i++)
            {
                imageName[i] = chars[random.Next(chars.Length)];
            }

            return new string(imageName);
        }

        public Bitmap LoadImage(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    return new Bitmap(imagePath);
                }
                else
                {
                    Console.WriteLine("Image file does not exist.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        private void PopulateContextMenuStrip()
        {
            foreach (UnityEntitiesEnum option in Enum.GetValues(typeof(UnityEntitiesEnum)))
            {
                ToolStripMenuItem item1 = new ToolStripMenuItem(option.ToString());
                contextMenuStrip1.Items.Add(item1);
                item1.Click += ToolStripMenuItem_Click;
            }
        }

        //hàm đánh check khi click vào 1 MenuItem
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reset all checked item
            foreach (ToolStripMenuItem toolStripMenuItem in contextMenuStrip1.Items)
            {
                toolStripMenuItem.Checked = false;
            }

            //checked item picked
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            clickedItem.Checked = !clickedItem.Checked;
        }

        private void ResponsiveTreeNode(string layerName, string objectType, string selected)
        {
            string treeNodeName = layerName + " (" + objectType + ")";
            foreach (TreeNode parentNode in treeViewSelectedEntity.Nodes)
            {
                if(treeNodeName == parentNode.Text)
                {
                    if (selected == "None")
                    {
                        parentNode.ForeColor = Color.Black;
                        parentNode.NodeFont = new Font(treeViewSelectedEntity.Font, FontStyle.Regular);
                    }
                    else
                    {
                        parentNode.ForeColor = Color.OrangeRed;
                        parentNode.NodeFont = new Font(treeViewSelectedEntity.Font, FontStyle.Underline);
                    }
                }
            }
        }

        private List<UnityEntity> DeleteUnityEntityWhenChangeTypeUnity(string layerName, string objectType, int id, string currentType, string beforeType)
        {
            List<UnityEntity> newList = new List<UnityEntity>();
            if (_architecture.Floors[_currentFloor].ListUnityEntities.Count > 0)
            {
                if (currentType == "None")
                {
                    foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                    {
                        if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType && unityEntity.TypeOfUnityEntity == beforeType)
                        {
                        }
                        else
                        {
                            newList.Add(unityEntity);
                        }
                    }
                }
                else
                {
                    foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                    {
                        if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType && unityEntity.Id == id)
                        {
                        }
                        else
                        {
                            newList.Add(unityEntity);
                        }
                    }
                }

            }
            else return _architecture.Floors[_currentFloor].ListUnityEntities;

            return newList;
        }

        private List<UnityEntity> DeleteUnityEntityWhenRemoveTreeNode(string layerName, string objectType, string currentType)
        {
            List<UnityEntity> newList = new List<UnityEntity>();
            if (_architecture.Floors[_currentFloor].ListUnityEntities.Count > 0)
            {
                foreach (UnityEntity unityEntity in _architecture.Floors[_currentFloor].ListUnityEntities)
                {
                    if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType && unityEntity.TypeOfUnityEntity == currentType)
                    {
                    }
                    else
                    {
                        newList.Add(unityEntity);
                    }
                }
            }
            else return _architecture.Floors[_currentFloor].ListUnityEntities;

            return newList;
        }



        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _architecture.NumberOfFloor++;
            _architecture.Floors.Add(new Floor());
            cbNumberFloor.Text = _architecture.NumberOfFloor.ToString();
            cbNumberFloor.Items.Add(_architecture.NumberOfFloor);
            treeView1.Nodes.Clear();
            treeViewSelectedEntity.Nodes.Clear();
            pictureBoxThumbNail.Image = null;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (ToolStripMenuItem toolStripMenuItem in contextMenuStrip1.Items)
            {
                if (toolStripMenuItem.Text == "Remove")
                {
                    toolStripMenuItem.ForeColor = Color.White;
                    toolStripMenuItem.BackColor = Color.Red;
                    toolStripMenuItem.Font = new Font(toolStripMenuItem.Font, FontStyle.Bold);
                }
            }
        }
    }
}
