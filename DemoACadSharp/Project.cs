using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Project
    {
        string nameProject;
        string path;
        DateTime dateTime;

        public Project(string nameProject, string path, DateTime dateTime)
        {
            this.NameProject = nameProject;
            this.Path = path;
            this.DateTime = dateTime;
        }

        public string NameProject { get => nameProject; set => nameProject = value; }
        public string Path { get => path; set => path = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
