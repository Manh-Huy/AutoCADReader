using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class UnityEntity : AcadEntity
    {
        string typeOfUnityEntity;
        string color;
        double height;

        public UnityEntity(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public UnityEntity(string _typeOfUnityEntity)
        {
            this.typeOfUnityEntity = _typeOfUnityEntity;
        }

        public string TypeOfUnityEntity { get => typeOfUnityEntity; set => typeOfUnityEntity = value; }
        public string Color { get => color; set => color = value; }
        public double Height { get => height; set => height = value; }
    }
}
