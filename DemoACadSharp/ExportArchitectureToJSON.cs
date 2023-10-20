using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class ExportArchitectureToJSON
    {
        string nameFloor;
        List<UnityEntity> listToExport = new List<UnityEntity>();

        public string NameFloor { get => nameFloor; set => nameFloor = value; }
        public List<UnityEntity> ListToExport { get => listToExport; set => listToExport = value; }
    }
}
