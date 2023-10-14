using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Document
    {
        private List<ParentEntity> allEntity = new List<ParentEntity>();



        public List<ParentEntity> AllEntity { get => allEntity; set => allEntity = value; }
    }
}
