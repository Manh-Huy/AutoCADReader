using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class UnityEntity : AcadEntity
    {
        string color;
        double height;

        public UnityEntity(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public string Color { get => color; set => color = value; }
        public double Height { get => height; set => height = value; }
    }
}
