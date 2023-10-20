using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;
using Aspose.CAD.FileFormats.Cad;
using Aspose.CAD.ImageOptions;
using CSMath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Aspose.CAD.FileFormats.GLB.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Aspose.CAD.FileFormats.Ifc.IFC2X3.Types;
using Aspose.CAD.FileFormats.GLB;
using Aspose.CAD.FileFormats.Collada.FileParser.Elements;
using static ACadSharp.Objects.XRecrod;
using Aspose.CAD;
using Color = System.Drawing.Color;
using TreeView = System.Windows.Forms.TreeView;
using System.Collections;
using Line = ACadSharp.Entities.Line;

namespace DemoACadSharp
{
    public partial class MainForm : Form
    {
        House house = new House();
        Document document = new Document();



        List<AcadEntity> _listAllEntities = new List<AcadEntity>();

        List<AcadEntity> _listUniqueEntities = new List<AcadEntity>();

        string nameArchitecture = InitialForm.nameHouse;
        int numberOfFloors = InitialForm.numberOfFloors;
        string topFloor = InitialForm.topFloor;

        int currentFloor = 1;
        private TreeNode selectedNode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Architecture architecture = new Architecture(nameArchitecture, numberOfFloors, topFloor);

            txtNameHouse.Text = Architecture.getInstance().NameArchitecture;
            for (int i = 1; i <= Architecture.getInstance().NumberOfFloor; i++)
            {
                cbNumberFloor.Items.Add(i);
            }
            txtTopFloor.Text = Architecture.getInstance().TypeOfRoof;
        }

        #region Event
        private void importFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

                Architecture.getInstance().Floors[currentFloor].ListAllEntities = _listAllEntities;
                Architecture.getInstance().Floors[currentFloor].ListUniqueEntities = _listUniqueEntities;

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
                        JArray jsonArray = JArray.Parse(json);

                        foreach (JToken token in jsonArray)
                        {
                            AcadEntity entity = token.ToObject<AcadEntity>();
                            _listAllEntities.Add(entity);
                        }

                        Architecture.getInstance().Floors[currentFloor].Order = currentFloor + 1;

                        Architecture.getInstance().Floors[currentFloor].ListAllEntities = _listAllEntities;
                        _listUniqueEntities = Architecture.getInstance().Floors[currentFloor].getUniqueEntities();
                        // Xóa nút hiện tại trong TreeView

                        setDataToTreeView_View();
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
            //List<EntityInfo> selectedEntities = GetSelectedEntities(treeView1.Nodes, _listAllEntities);

            string json = JsonConvert.SerializeObject(Architecture.getInstance().Floors[currentFloor].ListAllEntities, Formatting.Indented);

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
        private void cbNumberFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;

            if (Architecture.getInstance().Floors[currentFloor].ListAllEntities == null ||
                    Architecture.getInstance().Floors[currentFloor].ListAllEntities.Count == 0)
            {
                treeView1.Nodes.Clear();
                treeViewSelectedEntity.Nodes.Clear();
                MessageBox.Show("Rhyderrr");
            }
            else
            {
                setDataToTreeView_View();
                setDataToTreeView_Config();
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
                copyDataFromAcadEntityToUnityEntity();
                MessageBox.Show("Selected Successfully!");
            }
            else
            {
                MessageBox.Show("You have not selected any Entity yet!!");
            }
        }

        private void copyDataFromAcadEntityToUnityEntity()
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
        }

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
                                foreach (Wall wall in floor.ListWalls)
                                {
                                    if (wall.Id == idEntity)
                                        propertyGrid1.SelectedObject = wall;
                                }
                                break;
                            case "Stair":
                                foreach (Stair stair in floor.ListStairs)
                                {
                                    if (stair.Id == idEntity)
                                        propertyGrid1.SelectedObject = stair;
                                }
                                break;
                            case "Door":
                                foreach (Door door in floor.ListDoors)
                                {
                                    if (door.Id == idEntity)
                                        propertyGrid1.SelectedObject = door;
                                }
                                break;
                            case "Power":
                                foreach (Power power in floor.ListPowers)
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

        #endregion

        #region Method
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
                        TreeNode childNode = new TreeNode($"{UniEntity.LayerName} ({UniEntity.ObjectType})");
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
                            foreach (AcadEntity childEntity in Architecture.getInstance().Floors[currentFloor].ListAllEntities)
                            {
                                if (childEntity.LayerName == layerName && childEntity.ObjectType == objectType &&
                                     idChildEntity == childEntity.Id)
                                {
                                    Architecture.getInstance().Floors[currentFloor].ListSelectedEntities.Add(childEntity);
                                }
                            }
                        }
                    }
                }
            }

            Architecture.getInstance().Floors[currentFloor].ListSelectedEntities.Reverse();
        }

        private void setDataToTreeView_View()
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
            treeView1.Nodes.Clear();
            treeView1.CheckBoxes = true;


            foreach (AcadEntity parentEntity in _listUniqueEntities)
            {
                TreeNode parentNode = treeView1.Nodes.Add($"{parentEntity.LayerName} ({parentEntity.ObjectType})");


                foreach (AcadEntity childEntity in Architecture.getInstance().Floors[currentFloor].ListAllEntities)
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
            listUniqueSelectedEntity = Architecture.getInstance().Floors[currentFloor].getUniqueSelectedEntities();

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


        #endregion

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

            foreach (UnityEntity childEntity in floor.ListUnityEntities)
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
            }


        }

        private void btnExportToJSON_Click(object sender, EventArgs e)
        {
            currentFloor = Int32.Parse(cbNumberFloor.Text) - 1;
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
            }
        }

        private void exportJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = false;
            List<string> listFloorNoData = new List<string>();
            for(int i = 0; i < Architecture.getInstance().Floors.Count; i++)
            {
                if (Architecture.getInstance().Floors[i].ListUnityEntities == null)
                {
                    listFloorNoData.Add((i + 1).ToString());
                }    
            }

            foreach (Floor floorCheck in Architecture.getInstance().Floors)
            {
                if(floorCheck.ListUnityEntities == null)
                {
                    check = false;
                    break;
                } else
                {
                    check = true;
                }    
            }    

            if(check)
            {
                List<ExportArchitectureToJSON> listToExport = new List<ExportArchitectureToJSON>();
                foreach (Floor floor in Architecture.getInstance().Floors)
                {
                    ExportArchitectureToJSON floorEntity = new ExportArchitectureToJSON();
                    floorEntity.NameFloor = "Floor " + floor.Order.ToString();
                    if (floor.ListWalls.Count > 0)
                    {
                        floorEntity.ListToExport.AddRange(floor.ListWalls);
                    }
                    if (floor.ListStairs.Count > 0)
                    {
                        floorEntity.ListToExport.AddRange(floor.ListStairs);
                    }
                    if (floor.ListDoors.Count > 0)
                    {
                        floorEntity.ListToExport.AddRange(floor.ListDoors);
                    }
                    if (floor.ListPowers.Count > 0)
                    {
                        floorEntity.ListToExport.AddRange(floor.ListPowers);
                    }

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
            } else
            {
                string numberOfFloorNoData = "";
                foreach(string number in listFloorNoData)
                {
                    numberOfFloorNoData += "'" + number + "'" + " ";
                }
                MessageBox.Show("Error! Floor " + numberOfFloorNoData + "have not been data imported yet!");
            }    
            
        }
    }
}
