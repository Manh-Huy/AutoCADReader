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

        public AcadEntity() { }

        public AcadEntity(int? id, string layerName, string objectType, List<string> coordinates)
        {
            this.Id = id;
            this.LayerName = layerName;
            this.ObjectType = objectType;
            this.Coordinates = coordinates;
        }

        public AcadEntity Clone()
        {
            AcadEntity entity = new AcadEntity();
            entity.Id = this.Id;
            entity.LayerName = this.LayerName;
            entity.objectType = this.objectType;
            entity.Coordinates = this.Coordinates;

            return entity;
        }
    }
}
