using ACadSharp.IO;
using ACadSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ACadSharp.Entities;
using System.Windows.Forms;
using CSMath;
using ACadSharp.Blocks;
using static ACadSharp.Entities.Hatch.BoundaryPath;
using System.Diagnostics;

namespace DemoACadSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string _outputPath = "";
        string _outputConvertPath = "";
        private void inputButton_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại mở tệp và cho phép người dùng chọn tệp DWG
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AutoCAD Drawing Files (*.dwg)|*.dwg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                List<string> objectProperties = GetAllObjectProperties(filePath);

                // Lưu danh sách thông số các đối tượng vào tệp văn bản
                string outputFilePath = "F:\\Desktop\\object_properties.txt";
                SaveObjectPropertiesToFile(objectProperties, outputFilePath);

                MessageBox.Show("Object properties have been saved to " + outputFilePath);

                _outputPath = outputFilePath;

                lblpathout.Text = "link output: " + outputFilePath;
            }
        }

        public static List<string> GetAllObjectProperties(string file)
        {
            List<string> objectProperties = new List<string>();
            using (DwgReader reader = new DwgReader(file))
            {
                CadDocument doc = reader.Read();
                foreach (Entity entity in doc.Entities)
                {
                    if (entity is LwPolyline)
                    {
                        string properties = $"Layer: {entity.Layer.Name}, Object Type: {entity.GetType().Name}";
                        objectProperties.Add(properties);
                        LwPolyline lwPolyline = (LwPolyline) entity;

                        List<LwPolyline.Vertex> vertices = lwPolyline.Vertices;

                        foreach (LwPolyline.Vertex vertex in vertices)
                        {
                            XY position = vertex.Location;

                            string posString = position.ToString();
                            objectProperties.Add(posString);
                        }

                        //// Lấy tọa độ đầu và cuối của đường thẳng
                        //Line line = (Line)entity;
                        //XYZ startPoint = line.StartPoint;
                        //XYZ endPoint = line.EndPoint;

                        //// Xây dựng chuỗi thông số với tọa độ đầu và cuối của đường thẳng
                        //string properties = $"Layer: {entity.Layer.Name}, Object Type: {entity.GetType().Name}, StartPoint: {startPoint}, EndPoint: {endPoint}";
                        //objectProperties.Add(properties);



                    }
                    else
                    {
                        // Lấy thông số của đối tượng và thêm vào danh sách
                        string properties = GetObjectProperties(entity);
                        objectProperties.Add(properties);
                    }
                }
                
            }
            return objectProperties;
        }

        public static string GetObjectProperties(Entity entity)
        {
            // Lấy tên lớp (layer) của đối tượng
            string layerName = entity.Layer.Name;

            // Đây là nơi bạn có thể truy cập các thông số khác của đối tượng và xây dựng chuỗi thông số
            string properties = $"Layer: {layerName}, Object Type: {entity.GetType().Name}";

            return properties;
        }

        public static void SaveObjectPropertiesToFile(List<string> objectProperties, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string properties in objectProperties)
                {
                    writer.WriteLine(properties);
                }
            }
        }

        private void outputButton_Click(object sender, EventArgs e)
        {
            string filePath = _outputPath;

            try
            {
                // Kiểm tra xem tệp có tồn tại không
                if (System.IO.File.Exists(filePath))
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

        private void convertButton_Click(object sender, EventArgs e)
        {
            // Đọc nội dung từ file ban đầu
            string inputFilePath = _outputPath;
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đọc file: " + ex.Message);
                return;
            }

            // Tạo và ghi nội dung vào file mới
            string outputFilePath = "F:\\Desktop\\convert_position.txt";
            _outputConvertPath = outputFilePath;
            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            if (float.TryParse(parts[0], out float x) && float.TryParse(parts[1], out float z))
                            {
                                writer.WriteLine($"new Vector3({x}f, 0, {z}f),");
                            }
                            //else
                            //{
                            //    MessageBox.Show("Không thể chuyển đổi thành số float ở đây");
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Không có đúng định dạng");
                        }
                    }
                }
                
                MessageBox.Show("Chuyển đổi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi ghi file mới: " + ex.Message);
            }
        }

        private void outputConvertButton_Click(object sender, EventArgs e)
        {
            string filePath = _outputConvertPath;

            try
            {
                // Kiểm tra xem tệp có tồn tại không
                if (System.IO.File.Exists(filePath))
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
    }
}
