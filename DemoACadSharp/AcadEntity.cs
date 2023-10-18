using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class AcadEntity
    {
        private int? id;
        private string layerName;
        private string objectType;
        private List<string> coordinates = null;

        public int? Id { get => id; set => id = value; }
        public string LayerName { get => layerName; set => layerName = value; }
        public string ObjectType { get => objectType; set => objectType = value; }
        public List<string> Coordinates { get => coordinates; set => coordinates = value; }
    }
}
