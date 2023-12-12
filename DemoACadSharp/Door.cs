using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Door : UnityEntity
    {
        public Door(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }
        public Door(string typeOfUnityEntity) : base(typeOfUnityEntity) { }

        public Door() { }

    }
}
