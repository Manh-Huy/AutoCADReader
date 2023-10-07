using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;
using CSMath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Aspose.CAD.ImageOptions;
using Aspose.CAD.FileFormats.Cad;
using Aspose.CAD.FileFormats.Cad.CadObjects;

namespace DemoACadSharp
{
    public partial class ReadFileAutoCadForm : Form
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

        public static List<EntityInfo> GetObjectProperties(string file)
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

        public void OpenFile(string filePath)
        {
            try
            {
                // Kiểm tra xem tệp có tồn tại không
                if (File.Exists(filePath))
                {
                    // Sử dụng Process.Start để mở tệp với ứng dụng mặc định
                    Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show("Tệp không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi mở tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SaveListEntitiesInfoToFile(List<EntityInfo> listEntity, string filePath)
        {
            try
            {
                //if (File.Exists(filePath))
                //{
                //    var result = MessageBox.Show("Tệp đã tồn tại. Có muốn ghi đè lên nó không?", "Xác nhận ghi đè", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (result == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (EntityInfo entity in listEntity)
                    {
                        writer.WriteLine($"Id: {entity.Id}, Layer: {entity.LayerName}, Object Type: {entity.ObjectType}");
                        if (entity.Coordinates != null)
                        {
                            foreach (var coordinates in entity.Coordinates)
                            {
                                writer.WriteLine(coordinates);
                            }
                        }
                    }
                }

                // Nếu không có lỗi xảy ra, hiển thị thông báo thành công.
                MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra, hiển thị thông báo lỗi.
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SaveDetailEntityInfoToFile(EntityInfo entity, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Id: {entity.Id}, Layer: {entity.LayerName}, Object Type: {entity.ObjectType}");
                    if (entity.Coordinates != null)
                    {
                        foreach (var coordinates in entity.Coordinates)
                        {
                            writer.WriteLine(coordinates);
                        }
                    }
                }

                // Nếu không có lỗi xảy ra, hiển thị thông báo thành công.
                MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra, hiển thị thông báo lỗi.
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ReadFileAutoCadForm()
        {
            InitializeComponent();

            _allListType = new List<List<EntityInfo>>
            {
                _listTypeLwPolyline,
                _listTypeLine,
                _listOtherType
            };
        }

        private void buttonInputFile_Click(object sender, EventArgs e)
        {
            _listAllEntities.Clear();
            _listUniqueEntities.Clear();
            // Hiển thị hộp thoại mở tệp và cho phép người dùng chọn tệp DWG
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AutoCAD Drawing Files (*.dwg)|*.dwg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;


                _listAllEntities = GetObjectProperties(filePath);

                _listUniqueEntities = _listAllEntities
                    .GroupBy(entity => new { entity.LayerName, entity.ObjectType })
                    .Select(group => new EntityInfo(null, group.Key.LayerName, group.Key.ObjectType, null))
                    .ToList();

                // Lưu danh sách thông số các đối tượng vào tệp văn bản
                string allObjectPropertiesFilePath = "..\\..\\File Text\\All_Entities_Properties.txt";
                SaveListEntitiesInfoToFile(_listAllEntities, allObjectPropertiesFilePath);

                string uniqueObjectPath = "..\\..\\File Text\\All_Unique_Entities.txt";
                SaveListEntitiesInfoToFile(_listUniqueEntities, uniqueObjectPath);


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
                        pictureBox2DAutoCad.Image = new Bitmap(bitmap);
                    }
                }
            }
        }

        private void buttonDisplayEntities_Click(object sender, EventArgs e)
        {
            tableLayoutPanelEntityList.Controls.Clear();
            tableLayoutPanelEntityList.AutoSize = true;
            _listCheckBoxes.Clear(); // Xóa danh sách checkBoxes

            for (int i = 0; i < _listUniqueEntities.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = $"Layer: {_listUniqueEntities[i].LayerName}, Object Type: {_listUniqueEntities[i].ObjectType}";
                checkBox.AutoSize = true;
                tableLayoutPanelEntityList.Controls.Add(checkBox);

                // Thêm checkBox vào danh sách checkBoxes
                _listCheckBoxes.Add(checkBox);
            }
        }

        private void buttonSelectEntities_Click(object sender, EventArgs e)
        {
            _listSelectedEntities.Clear();

            foreach (CheckBox checkBox in _listCheckBoxes)
            {
                if (checkBox.Checked)
                {
                    // Lấy vị trí của checkBox trong danh sách checkBoxes
                    int index = _listCheckBoxes.IndexOf(checkBox);

                    // Lấy thông tin Entity tương ứng từ _listUniqueEntity
                    if (index >= 0 && index < _listUniqueEntities.Count)
                    {
                        EntityInfo entity = _listUniqueEntities[index];
                        _listSelectedEntities.Add(entity);
                    }
                }
            }

            if (_listSelectedEntities.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất một entity", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string selectedEntitiesPath = "..\\..\\File Text\\Selected_Entities.txt";
                SaveListEntitiesInfoToFile(_listSelectedEntities, selectedEntitiesPath);
            }
        }

        private void buttonClassifySelectedEntitiesByType_Click(object sender, EventArgs e)
        {
            _listAllSelectedEntities.Clear();
            _listTypeLwPolyline.Clear();
            _listTypeLine.Clear();
            _listOtherType.Clear();
            _listTypeEntityFilePath.Clear();

            // Lặp qua từng phần tử trong _listSelectedEntities
            foreach (EntityInfo selectedEntity in _listSelectedEntities)
            {
                // Sử dụng LINQ để tìm các đối tượng trong _listAllEntities có cùng tên và loại
                IEnumerable<EntityInfo> matching = _listAllEntities.Where(entity =>
                    entity.LayerName == selectedEntity.LayerName &&
                    entity.ObjectType == selectedEntity.ObjectType);

                // Thêm tất cả các đối tượng tìm thấy vào danh sách _listPositionAllSelectedEntities
                _listAllSelectedEntities.AddRange(matching);
            }

            foreach (EntityInfo entity in _listAllSelectedEntities)
            {
                if (entity.ObjectType == "LwPolyline")
                {
                    _listTypeLwPolyline.Add(entity);
                }
                else if (entity.ObjectType == "Line")
                {
                    _listTypeLine.Add(entity);
                }
                else
                {
                    _listOtherType.Add(entity);
                }
            }

            // Lưu path vào list _listTypeEntityFilePath để in file text

            int temp = 0;
            foreach (List<EntityInfo> listType in _allListType)
            {
                if (listType.Count != 0)
                {
                    string listTypepath = $"..\\..\\File Text\\Type_Entities_{temp}.txt";
                    _listTypeEntityFilePath.Add(listTypepath);
                    SaveListEntitiesInfoToFile(listType, listTypepath);
                    temp++;
                }
            }
        }

        private void buttonHandleSelectedEntities_Click(object sender, EventArgs e)
        {
            if (_listTypeLwPolyline.Count != 0)
            {
                int temp = 0;
                foreach (EntityInfo entity in _listTypeLwPolyline)
                {
                    // tạo các file text chứa thông tin từng lwpolyline
                    string listTypePath = $"..\\..\\File Text\\LwPolyline Entities\\LwPolyline_Entities_{temp}.txt";
                    _listTypeEntityFilePath.Add(listTypePath);
                    SaveDetailEntityInfoToFile(entity, listTypePath);

                    temp++;
                }
            }
            // xử lý tiếp tục các loại entity khác
        }

        private void buttonDisplayAllEntitiesProperties_Click(object sender, EventArgs e)
        {
            string allObjectPropertiesFilePath = "..\\..\\File Text\\All_Entities_Properties.txt";
            OpenFile(allObjectPropertiesFilePath);
        }

        private void buttonDisplayEntitiesInFile_Click(object sender, EventArgs e)
        {
            string uniqueObjectPath = "..\\..\\File Text\\All_Unique_Entities.txt";
            OpenFile(uniqueObjectPath);
        }

        private void buttonDisplaySelectedEntitiesInFile_Click(object sender, EventArgs e)
        {
            string selectedEntitiesPath = "..\\..\\File Text\\Selected_Entities.txt";
            OpenFile(selectedEntitiesPath);
        }

        private void buttonDisplayClassifySelectedEntitiesByType_Click(object sender, EventArgs e)
        {
            foreach (string path in _listTypeEntityFilePath)
            {
                OpenFile(path);
            }
        }
    }
}
