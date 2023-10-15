using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Document
    {
        private List<ParentEntity> parentEntity = new List<ParentEntity>();


        public List<ParentEntity> ParentEntity { get => parentEntity; set => parentEntity = value; }

        public List<EntityInfo> getAllEntity()
        {
            List<EntityInfo> allEntity = new List<EntityInfo>();
            foreach(ParentEntity parentEntity in ParentEntity)
            {
                foreach(EntityInfo childEntity in parentEntity.EntityInfos)
                {
                    allEntity.Add(childEntity);
                }
            }

            return allEntity;
        }
    }
}
