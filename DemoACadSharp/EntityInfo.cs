using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class EntityInfo
    {
        private int? id;
        private string layerName;
        private string objectType;
        private List<string> coordinates = null;

        public string LayerName { get => layerName; set => layerName = value; }
        public string ObjectType { get => objectType; set => objectType = value; }
        public List<string> Coordinates { get => coordinates; set => coordinates = value; }
        public int? Id { get => id; set => id = value; }

        public EntityInfo(int? id, string layerName, string objectType, List<string> coordinates)
        {
            this.Id = id;
            this.LayerName = layerName;
            this.ObjectType = objectType;
            this.Coordinates = coordinates;
        }
    }
}
