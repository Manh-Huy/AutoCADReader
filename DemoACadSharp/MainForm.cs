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

namespace DemoACadSharp
{
    public partial class MainForm : Form
    {
        List<EntityInfo> _listAllEntities = new List<EntityInfo>();

        List<EntityInfo> _listUniqueEntities = new List<EntityInfo>();

        List<EntityInfo> _listSelectedEntities = new List<EntityInfo>();

        List<EntityInfo> _listAllSelectedEntities = new List<EntityInfo>();

        List<CheckBox> _listCheckBoxes = new List<CheckBox>();

        // List các type entity cần thiết

        List<List<EntityInfo>> _allListType;

        List<EntityInfo> _listTypeLwPolyline = new List<EntityInfo>();

        List<EntityInfo> _listTypeLine = new List<EntityInfo>();

        List<EntityInfo> _listOtherType = new List<EntityInfo>();


        List<string> _listTypeEntityFilePath = new List<string>(); // lưu path file của các loại entity

        public MainForm()
        {
            InitializeComponent();
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
                    .Select(group => new EntityInfo(null, group.Key.LayerName, group.Key.ObjectType, null))
                    .ToList();

                AddEntityToHierachy(_listUniqueEntities, _listAllEntities);

                //// Lưu danh sách thông số các đối tượng vào tệp văn bản
                //string allObjectPropertiesFilePath = "..\\..\\File Text\\All_Entities_Properties.txt";
                //SaveListEntitiesInfoToFile(_listAllEntities, allObjectPropertiesFilePath);

                //string uniqueObjectPath = "..\\..\\File Text\\All_Unique_Entities.txt";
                //SaveListEntitiesInfoToFile(_listUniqueEntities, uniqueObjectPath);


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

        // Event check in node parent
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                // Thay đổi trạng thái kiểm tra của tất cả nút con
                CheckChildNodes(e.Node, e.Node.Checked);
            }
        }

        // Event save file to JSON(JavaScript Object Notation =))))))))))) )
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<EntityInfo> selectedEntities = GetSelectedEntities(treeView1.Nodes, _listAllEntities);

            string json = JsonConvert.SerializeObject(_listAllEntities, Formatting.Indented);

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
        public List<EntityInfo> GetAllEntities(string file)
        {
            List<EntityInfo> listAllEntity = new List<EntityInfo>();

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
                    EntityInfo entityInfo = new EntityInfo(coutId, layerName, objectType, coordinates);
                    listAllEntity.Add(entityInfo);
                    coutId++;
                }
            }
            return listAllEntity;
        }

        public void AddEntityToHierachy(List<EntityInfo> listUniqueEntities, List<EntityInfo> listAllEntities)
        {

            treeView1.CheckBoxes = true;

            foreach (EntityInfo UniEntity in listUniqueEntities)
            {
                TreeNode parentNode = new TreeNode($"{UniEntity.LayerName} ({UniEntity.ObjectType})");
                treeView1.Nodes.Add(parentNode);
                foreach (EntityInfo entity in listAllEntities)
                {
                    if (UniEntity.LayerName == entity.LayerName && UniEntity.ObjectType == entity.ObjectType)
                    {
                        TreeNode childNode = new TreeNode($"{UniEntity.LayerName} ({UniEntity.ObjectType})");
                        parentNode.Nodes.Add(childNode);
                        childNode.Tag = entity;
                        //objectDictionary[childNode] = entity;
                    }
                }
                //toolStripProgressBar.Value++;
            }
        }


        // Method support check in node parent
        private void CheckChildNodes(TreeNode treeNode, bool isChecked)
        {
            foreach (TreeNode childNode in treeNode.Nodes)
            {
                childNode.Checked = isChecked;

                // Đệ quy để thay đổi trạng thái của các nút con của nút con
                CheckChildNodes(childNode, isChecked);
            }
        }

        // Function call Recursion to support for btn SaveFile
        private List<EntityInfo> GetSelectedEntities(TreeNodeCollection nodes, List<EntityInfo> entityList)
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
        }

        #endregion

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
                        string json = File.ReadAllText(openFileDialog.FileName);

                        _listAllEntities.Clear();

                        // Xóa nút hiện tại trong TreeView
                        treeView1.Nodes.Clear();
                        treeView1.CheckBoxes = true;

                        // Chuyển đổi JSON thành mảng đối tượng
                        JArray jsonArray = JArray.Parse(json);

                        // Tạo nút gốc cho mỗi loại LayerName
                        var layerGroups = jsonArray.GroupBy(obj => obj.Value<string>("LayerName"));
                        foreach (var layerGroup in layerGroups)
                        {
                            string layerName = layerGroup.Key;
                            TreeNode layerNode = treeView1.Nodes.Add(layerName);

                            // Thêm các nút con cho mỗi đối tượng cùng loại LayerName
                            foreach (var obj in layerGroup)
                            {
                                string objectType = obj.Value<string>("ObjectType");
                                string nodeText = $"{layerName} ({objectType})";
                                layerNode.Nodes.Add(nodeText);
                                string layerNameObj = obj.Value<string>("LayerName"); // Lấy layerName từ obj

                                EntityInfo entity = new EntityInfo(null, layerNameObj, objectType, new List<string>());
                                _listAllEntities.Add(entity);
                            }
                        }

                        // Mở tất cả các nút gốc
                        treeView1.ExpandAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc tệp JSON: " + ex.Message);
                    }
                }
            }
        }

        private void AddNode(JToken token, TreeNode parentNode)
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
        }

    }
}
