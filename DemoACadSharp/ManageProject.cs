using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class ManageProject
    {
        List<Project> listProject;
        string appName = "HKTArchitecture";

        public List<Project> ListProject { get => listProject; set => listProject = value; }

        public ManageProject()
        {
            ListProject = new List<Project>();
        }

        public void AddProject(string name, string path)
        {
            Project newProject = new Project(name, path, DateTime.Now);

            ListProject.Add(newProject);
        }

        public List<Project> GetProjects()
        {
            return ListProject;
        }

        public bool GetAppDataFolderPath(string appDataFolder, string appNameFoler)
        {
            if (Directory.Exists(appNameFoler))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EnsureAppFolderExists(Project newProject)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appNameFoler = Path.Combine(appDataFolder, appName);
            bool isExist = GetAppDataFolderPath(appDataFolder, appNameFoler);
            string filePath = Path.Combine(appNameFoler, "ListProject.json");

            // Tạo thư mục con cho ứng dụng trong thư mục AppData nếu chưa tồn tại
            if (!isExist)
            {
                Directory.CreateDirectory(appNameFoler);
                ReadFileJSON(filePath, newProject);
            }
            else
            {
                ReadFileJSON(filePath, newProject);
            }

        }

        public void ReadFileJSON(string filePath, Project newProject)
        {
            List<Project> projectList = new List<Project>();
            if (File.Exists(filePath))
            {
                // Nếu tồn tại, đọc danh sách dự án từ tệp tin
                string jsonContent = File.ReadAllText(filePath);
                projectList = JsonConvert.DeserializeObject<List<Project>>(jsonContent);
            }
            else
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(projectList, Formatting.Indented));
            }

            // Thêm dự án mới vào danh sách
            projectList.Add(newProject);

            // Lưu trữ danh sách dự án dưới định dạng JSON
            File.WriteAllText(filePath, JsonConvert.SerializeObject(projectList, Formatting.Indented));
        }

        public bool isExistProjectInAppData(string filePath, string name)
        {
            bool isFound = false;
            List<Project> projectList = new List<Project>();
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appNameFoler = Path.Combine(appDataFolder, appName);
            bool isExist = GetAppDataFolderPath(appDataFolder, appNameFoler);
            if (isExist)
            {
                string filePathJson = Path.Combine(appNameFoler, "ListProject.json");
                if (File.Exists(filePathJson))
                {
                    string jsonContent = File.ReadAllText(filePathJson);
                    projectList = JsonConvert.DeserializeObject<List<Project>>(jsonContent);
                    if(projectList.Count > 0)
                    {
                        foreach(Project project in projectList)
                        {
                            if (project.Path == filePath && project.NameProject == name)
                            {
                                isFound = true;
                                break;
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
                
            }    
            
            return isFound;
        }
    }
}
