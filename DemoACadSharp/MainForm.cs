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
                foreach(EntityInfo entity in listAllEntities)
                {
                    if(UniEntity.LayerName == entity.LayerName && UniEntity.ObjectType == entity.ObjectType)
                    {
                        TreeNode childNode = new TreeNode($"{UniEntity.LayerName} ({UniEntity.ObjectType})");
                        parentNode.Nodes.Add(childNode);
                    }
                }
                toolStripProgressBar.Value++;
            }
        }

        #endregion

    }
}