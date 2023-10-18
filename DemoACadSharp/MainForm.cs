﻿using ACadSharp.Entities;
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
                MessageBox.Show("Selected Successfully!");
            } else
            {
                MessageBox.Show("You have not selected any Entity yet!!");
            }
        }

        private void treeViewSelectedEntity_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int floor = Int32.Parse(cbNumberFloor.Text) - 1;
            int idEntity = getIdEntity(e.Node.Text);

            foreach (ParentEntity parent in house.ListFloor[floor].EntityOfFloor.ParentEntity)
            {
                if (parent.ParentLayerName == e.Node.Text)
                {
                    propertyGrid1.SelectedObject = parent;
                }
                else foreach (EntityInfo childEntity in parent.EntityInfos)
                    {
                        if (idEntity == childEntity.Id)
                        {
                            propertyGrid1.SelectedObject = childEntity;
                        }
                    }
            }

        }

        /*private void ContextMenuStrip_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem node = sender as System.Windows.Forms.ToolStripMenuItem;
            if (node.Name == "wall")
            {
                MessageBox.Show("Rhyderrr");
            }
        } // Cái hàm này bỏ rồi, đang để đó để tham khảo thôi*/

        /*private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null)
                {
                    e.Node.ContextMenuStrip = contextMenuStrip1;
                }
            }
        } // Cái hàm này là để đnăg ký sự kiện lắng nghe cho các Node

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            *//*ToolStripMenuItem item = sender as ToolStripMenuItem;
            MessageBox.Show(item.GetType().ToString());*//*
            MessageBox.Show(sender.ToString());
        } // Hàm này sẽ thực thi nè*/

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

        // Method support check in node parent
        /*private void CheckChildNodes(TreeNode treeNode, bool isChecked)
        {
            foreach (TreeNode childNode in treeNode.Nodes)
            {
                childNode.Checked = isChecked;

                // Đệ quy để thay đổi trạng thái của các nút con của nút con
                CheckChildNodes(childNode, isChecked);
            }
        }*/

        // Function call Recursion to support for btn SaveFile
        /*private List<EntityInfo> GetSelectedEntities(TreeNodeCollection nodes, List<EntityInfo> entityList)
        {
            List<EntityInfo> selectedEntities = new List<EntityInfo>();

            foreach (TreeNode node in nodes)
            {
                if (node.Checked && node.Tag is EntityInfo entityInfo && entityList.Contains(entityInfo))
                {
                    selectedEntities.Add(entityInfo);
                }

                if (node.Nodes.Count > 0)
                {
                    selectedEntities.AddRange(GetSelectedEntities(node.Nodes, entityList));
                }
            }

            return selectedEntities;
        }*/

        /*private void AddNode(JToken token, TreeNode parentNode)
        {

            if (token.Type == JTokenType.Object)
            {
                JObject obj = (JObject)token;
                string layerName = obj.Value<string>("LayerName");
                string objectType = obj.Value<string>("ObjectType");
                string nodeText = $"{layerName} ({objectType})";

                // Tìm hoặc tạo nút cha dựa trên LayerName
                TreeNode layerNode = parentNode.Nodes.Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text.StartsWith(layerName));
                if (layerNode == null)
                {
                    layerNode = parentNode.Nodes.Add(layerName);
                }

                // Thêm nút con mới dựa trên nodeText
                layerNode.Nodes.Add(nodeText);
            }
            else if (token.Type == JTokenType.Array)
            {
                JArray array = (JArray)token;
                for (int i = 0; i < array.Count; i++)
                {
                    AddNode(array[i], parentNode);
                }
            }
        }*/

        // Hàm này con chó nào đụng dzô tao đấm chếch mọe à
        private void getDataFromTreeView_View()
        {
            /*int floor = Int32.Parse(cbNumberFloor.Text) - 1;
            int numberParentEntity = _listUniqueEntities.Count - 1;
            int numberEntitySelected = -1;
            house.ListFloor[floor].EntityOfFloor.ParentEntity.Clear();
            List<Floor> listFloor = house.ListFloor;


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
                    listFloor[floor].EntityOfFloor.ParentEntity.Add(new ParentEntity());
                    numberEntitySelected++;
                    EntityInfo parentEntityInfo = _listUniqueEntities[numberParentEntity];
                    ParentEntity parentEntity = new ParentEntity();
                    parentEntity.ParentLayerName = parentEntityInfo.LayerName;
                    parentEntity.ParentObjectType = parentEntityInfo.ObjectType;
                    listFloor[floor].EntityOfFloor.ParentEntity[numberEntitySelected] = parentEntity;
                    foreach (TreeNode childNode in currentNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            nodeStack.Push(childNode);
                            int idEntity = getIdEntity(childNode.Text);
                            foreach (EntityInfo entity in _listAllEntities)
                            {
                                if (entity.Id == idEntity)
                                {
                                    listFloor[floor].EntityOfFloor.ParentEntity[numberEntitySelected].EntityInfos.Add(entity);
                                    nodeStack.Pop();
                                    break;
                                }
                            }
                        }
                    }
                }

                numberParentEntity--;

            }
            MessageBox.Show(countCha.ToString() + "_________" + countCon.ToString());*/



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
            int floor = Int32.Parse(cbNumberFloor.Text) - 1;
            treeViewSelectedEntity.CheckBoxes = true;
            List<AcadEntity> listUniqueSelectedEntity = new List<AcadEntity>();
            listUniqueSelectedEntity = Architecture.getInstance().Floors[currentFloor].getUniqueSelectedEntities();

            if (listUniqueSelectedEntity != null)
            {
                foreach (AcadEntity parentEntity in listUniqueSelectedEntity)
                {
                    TreeNode parentNode = treeViewSelectedEntity.Nodes.Add($"{parentEntity.LayerName} ({parentEntity.ObjectType})");


                    foreach (AcadEntity childEntity in Architecture.getInstance().Floors[currentFloor].ListSelectedEntities)
                    {
                        if (parentEntity.LayerName == childEntity.LayerName && parentEntity.ObjectType == childEntity.ObjectType)
                        {
                            TreeNode childNode = parentNode.Nodes.Add($"{childEntity.Id}: {childEntity.LayerName} ({childEntity.ObjectType})");
                        }
                    }
                }
            }
        }

        private string getLayer(string input)
        {
            int startIndex = 0;
            int endIndex = input.IndexOf(" ");
            string x = input.Substring(startIndex, endIndex);
            return x;
        }

        private string getObjectType(string input)
        {
            int startIndex = input.IndexOf("(") + 1;
            int endIndex = input.IndexOf(")");
            string y = input.Substring(startIndex, endIndex - startIndex);
            return y;
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


    }
}
