using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Stair : UnityEntity
    {
        public Stair(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public Stair(string typeOfUnityEntity) : base(typeOfUnityEntity) { }

        public Stair() { }


    }
}
