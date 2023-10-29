using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class UnityEntity : AcadEntity
    {
        string typeOfUnityEntity;
        Color color;
        double height;

        public UnityEntity(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public UnityEntity(string _typeOfUnityEntity)
        {
            this.typeOfUnityEntity = _typeOfUnityEntity;
        }

        public string TypeOfUnityEntity { get => typeOfUnityEntity; set => typeOfUnityEntity = value; }
        public double Height { get => height; set => height = value; }
        public Color Color { get => color; set => color = value; }
    }
}
