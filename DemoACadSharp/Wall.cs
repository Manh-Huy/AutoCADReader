using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Wall : UnityEntity
    {
        public Wall(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public Wall(string typeOfUnityEntity) : base(typeOfUnityEntity) { }
    }
}
