using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Power : UnityEntity
    {
        public Power(int? id, string layerName, string objectType, List<string> coordinates) : base(id, layerName, objectType, coordinates)
        {
        }

        public Power(string typeOfUnityEntity) : base(typeOfUnityEntity) { }

    }
}
