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

        public List<EntityInfo> getAllEntity()
        {
            List<EntityInfo> allEntity = new List<EntityInfo>();
            foreach(ParentEntity parentEntity in AllEntity)
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
