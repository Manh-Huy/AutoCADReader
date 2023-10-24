﻿using ACadSharp.Entities;
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

namespace DemoACadSharp
{
    public partial class MainForm : Form
    {
        Architecture architecture = Architecture.getInstance();

        List<AcadEntity> _listAllEntities = new List<AcadEntity>();

        List<AcadEntity> _listUniqueEntities = new List<AcadEntity>();

        int currentFloor = 1;
        private TreeNode selectedNode;

        public MainForm()
        {
            InitializeComponent();
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
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            // Dọn Cache
            _listAllEntities.Clear();
            _listUniqueEntities.Clear();

            // Hiển thị hộp thoại mở tệp và cho phép người dùng chọn tệp DWG
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AutoCAD Drawing Files (*.dwg)|*.dwg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                _listAllEntities = GetAllEntities(filePath);

                // Chỉ lấy Entity Đại Diện
                _listUniqueEntities = _listAllEntities
                    .GroupBy(entity => new { entity.LayerName, entity.ObjectType })
                    .Select(group => new AcadEntity(null, group.Key.LayerName, group.Key.ObjectType, null))
                    .ToList();

                architecture.Floors[currentFloor].ListAllEntities = new List<AcadEntity>(_listAllEntities);
                architecture.Floors[currentFloor].ListUniqueEntities = new List<AcadEntity>(_listUniqueEntities);

                AddEntityToHierachy(_listUniqueEntities, _listAllEntities);

                using (CadImage cadImage = (CadImage)Aspose.CAD.Image.Load(filePath))
                {
                    // Create output options for the image (e.g., PNG)
                    PngOptions pngOptions = new PngOptions();

                    // Save the image as PNG
                    string outputPath = "..\\..\\Picture Autocad2D\\output_image.png"; // Output file path
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
                        currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
                        _listAllEntities.Clear();

                        string json = File.ReadAllText(openFileDialog.FileName);
                        Floor floor = JsonConvert.DeserializeObject<Floor>(json);

                        foreach (AcadEntity token in floor.ListAllEntities)
                        {
                            AcadEntity entity = token;
                            _listAllEntities.Add(entity);
                        }

                        architecture.Floors[currentFloor].Order = currentFloor + 1;
                        architecture.Floors[currentFloor].ListAllEntities = new List<AcadEntity>(_listAllEntities);
                        _listUniqueEntities = architecture.Floors[currentFloor].getUniqueEntities();
                        architecture.Floors[currentFloor].ListUniqueEntities = new List<AcadEntity>(_listUniqueEntities);
                        string imagePath = floor.ImageURL;
                        pictureBoxThumbNail.Image = LoadImage(imagePath);

                        setDataToTreeView_View(currentFloor);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc tệp JSON: " + ex.Message);
                    }


                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Architecture.getInstance().Floors[currentFloor].ImageURL = SaveImageToLocalFolder(pictureBoxThumbNail);
            string json = JsonConvert.SerializeObject(Architecture.getInstance().Floors[currentFloor], Formatting.Indented);

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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool isHaveCheck = false;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Checked) isHaveCheck = true;
            }
            if (isHaveCheck)
            {
                getDataFromTreeView_View();
                setDataToTreeView_Config();
                //copyDataFromAcadEntityToUnityEntity();
                MessageBox.Show("Selected Successfully!");
            }
            else
            {
                MessageBox.Show("You have not selected any Entity yet!!");
            }
        }

        private void treeViewSelectedEntity_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Floor floor = Architecture.getInstance().Floors[currentFloor];


            if (e.Node.Nodes.Count == 0)
            {
                int idEntity = getIdEntity(e.Node.Text);
                string layerName = deleteId(getLayer(e.Node.Text));
                string objectType = getObjectType(e.Node.Text);

                foreach (UnityEntity unityEntity in floor.ListUnityEntities)
                {
                    if (unityEntity.LayerName == layerName && unityEntity.ObjectType == objectType
                        && unityEntity.Id == idEntity)
                    {
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
            }
        }

        private void cbNumberFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            if (Architecture.getInstance().Floors[currentFloor].ListAllEntities == null ||
                    Architecture.getInstance().Floors[currentFloor].ListAllEntities.Count == 0)
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
                setDataToTreeView_View(currentFloor);
                setDataToTreeView_Config();
            }
        }

        private void treeViewSelectedEntity_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedNode = e.Node;
                if (e.Node != null && e.Node.Nodes.Count > 0)
                {
                    e.Node.ContextMenuStrip = contextMenuStrip1;
                }
            }
        }

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
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Floor floor = Architecture.getInstance().Floors[currentFloor];
            string layerName = "", objectType = "";
            string typeOfUnityEntity = e.ClickedItem.Text;

            if (selectedNode != null)
            {
                layerName = getLayer(selectedNode.Text);
                objectType = getObjectType(selectedNode.Text);
            }

            /*foreach (UnityEntity childEntity in floor.ListUnityEntities)
            {
                if (childEntity.LayerName == layerName && childEntity.ObjectType == objectType)
                {
                    childEntity.TypeOfUnityEntity = typeOfUnityEntity;
                    var templateEntity = FactoryUnityEntity.createObjectUnity(childEntity);
                    switch (templateEntity)
                    {
                        case Wall wall:
                            floor.ListWalls.Add(wall);
                            break;

                        case Stair stair:
                            floor.ListStairs.Add(stair);
                            break;

                        case Door door:
                            floor.ListDoors.Add(door);
                            break;

                        case Power power:
                            floor.ListPowers.Add(power);
                            break;
                    }
                }
            }*/

            foreach (AcadEntity entity in floor.ListSelectedEntities)
            {
                if (entity.LayerName == layerName && entity.ObjectType == objectType)
                {
                    var templateEntity = FactoryUnityEntity.createObjectUnity(entity, typeOfUnityEntity);
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

                        case Power power:
                            floor.ListUnityEntities.Add(power);
                            break;
                    }
                }
            }


        }

        private void btnExportToJSON_Click(object sender, EventArgs e)
        {
            /*currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Floor floor = Architecture.getInstance().Floors[currentFloor];
            List<UnityEntity> listToExport = new List<UnityEntity>();
            listToExport.AddRange(floor.ListWalls);
            listToExport.AddRange(floor.ListStairs);
            listToExport.AddRange(floor.ListDoors);
            listToExport.AddRange(floor.ListPowers);

            string json = JsonConvert.SerializeObject(listToExport, Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.Title = "Save JSON File";



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, json);
                MessageBox.Show("Save Successfully!");
            }*/
        }

        private void exportJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<ExportArchitectureToJSON> listToExport = new List<ExportArchitectureToJSON>();
            foreach (Floor floor in Architecture.getInstance().Floors)
            {
                ExportArchitectureToJSON floorEntity = new ExportArchitectureToJSON();
                floorEntity.NameFloor = "Floor " + floor.Order.ToString();
                floorEntity.ListToExport = floor.ListUnityEntities;

                listToExport.Add(floorEntity);
            }

            string json = JsonConvert.SerializeObject(listToExport, Formatting.Indented);

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

        #region Method

        /*        private void copyDataFromAcadEntityToUnityEntity()
                {
                    currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
                    Floor floor = Architecture.getInstance().Floors[currentFloor];
                    floor.ListUnityEntities = new List<UnityEntity>();
                    foreach (AcadEntity entity in floor.ListSelectedEntities)
                    {
                        UnityEntity unityEntity = new UnityEntity(entity.Id, entity.LayerName, entity.ObjectType, entity.Coordinates);
                        floor.ListUnityEntities.Add(unityEntity);
                    }
                    floor.ListWalls = new List<Wall>();
                    floor.ListStairs = new List<Stair>();
                    floor.ListDoors = new List<Door>();
                    floor.ListPowers = new List<Power>();
                }*/

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

        // Hàm này con chó nào đụng dzô tao đấm chếch mọe à
        private void getDataFromTreeView_View()
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            Architecture.getInstance().Floors[currentFloor].ListSelectedEntities = new List<AcadEntity>();

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

                    foreach (TreeNode childNode in currentNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            int idChildEntity = getIdEntity(childNode.Text);
                            foreach (AcadEntity childEntity in architecture.Floors[currentFloor].ListAllEntities)
                            {
                                if (childEntity.LayerName == layerName && childEntity.ObjectType == objectType &&
                                     idChildEntity == childEntity.Id)
                                {
                                    architecture.Floors[currentFloor].ListSelectedEntities.Add(childEntity);
                                }
                            }
                        }
                    }
                }
            }

            architecture.Floors[currentFloor].ListSelectedEntities.Reverse();
        }

        private void setDataToTreeView_View(int currentFloor)
        {

            treeView1.Nodes.Clear();
            treeView1.CheckBoxes = true;


            foreach (AcadEntity parentEntity in architecture.Floors[currentFloor].ListUniqueEntities)
            {
                TreeNode parentNode = treeView1.Nodes.Add($"{parentEntity.LayerName} ({parentEntity.ObjectType})");


                foreach (AcadEntity childEntity in architecture.Floors[currentFloor].ListAllEntities)
                {
                    if (parentEntity.LayerName == childEntity.LayerName && parentEntity.ObjectType == childEntity.ObjectType)
                    {
                        TreeNode childNode = parentNode.Nodes.Add($"{childEntity.Id}: {childEntity.LayerName} ({childEntity.ObjectType})");
                    }
                }
            }
        }

        private void setDataToTreeView_Config()
        {
            treeViewSelectedEntity.Nodes.Clear();
            int currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            List<AcadEntity> listUniqueSelectedEntity = new List<AcadEntity>();
            listUniqueSelectedEntity = architecture.Floors[currentFloor].getUniqueSelectedEntities();

            if (listUniqueSelectedEntity != null)
            {
                foreach (AcadEntity entity in listUniqueSelectedEntity)
                {
                    TreeNode parentNode = treeViewSelectedEntity.Nodes.Add($"{entity.LayerName} ({entity.ObjectType})");
                    parentNode.ForeColor = Color.Black;

                    foreach (AcadEntity childEntity in Architecture.getInstance().Floors[currentFloor].ListSelectedEntities)
                    {
                        if (entity.LayerName == childEntity.LayerName && entity.ObjectType == childEntity.ObjectType)
                        {
                            TreeNode childNode = parentNode.Nodes.Add($"{childEntity.Id}: {childEntity.LayerName} ({childEntity.ObjectType})");
                        }
                    }
                }
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

        public string SaveImageToLocalFolder(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                string fileName = GenerateRandomImageName(8) + ".jpg";
                string folderPath = "..\\..\\Picture Autocad2D\\";

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

        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            architecture.NumberOfFloor++;
            architecture.Floors.Add(new Floor());
            cbNumberFloor.Text = architecture.NumberOfFloor.ToString();
            cbNumberFloor.Items.Add(architecture.NumberOfFloor);
            treeView1.Nodes.Clear();
            pictureBoxThumbNail.Image = null;
        }
    }
}
