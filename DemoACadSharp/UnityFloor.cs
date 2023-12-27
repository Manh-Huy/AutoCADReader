using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{   
    // add comment
    public class UnityFloor
    {
        int order;
        List<UnityEntity> listEntities;

        public int Order { get => order; set => order = value; }
        public List<UnityEntity> ListEntities { get => listEntities; set => listEntities = value; }
    }
}
