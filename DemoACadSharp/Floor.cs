using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Floor
    {
        int order;
        string imageURL;
        List<AcadEntity> listAllEntities;
        List<AcadEntity> listSelectedEntities = new List<AcadEntity>();
        List<AcadEntity> listUniqueEntities ;
        List<AcadEntity> listUniqueSelectedEntities = new List<AcadEntity>();
        List<UnityEntity> listUnityEntities = new List<UnityEntity>();
        List<Wall> listWall = new List<Wall>();
        List<Door> listDoor = new List<Door>();
        List<Stair> listStair = new List<Stair>();
        List<Window> listWindow = new List<Window>();

        public int Order { get => order; set => order = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
        public List<AcadEntity> ListAllEntities { get => listAllEntities; set => listAllEntities = value; }
        public List<AcadEntity> ListSelectedEntities { get => listSelectedEntities; set => listSelectedEntities = value; }
        public List<AcadEntity> ListUniqueEntities { get => listUniqueEntities; set => listUniqueEntities = value; }
        public List<AcadEntity> ListUniqueSelectedEntities { get => listUniqueSelectedEntities; set => listUniqueSelectedEntities = value; }
        public List<UnityEntity> ListUnityEntities { get => listUnityEntities; set => listUnityEntities = value; }
        public List<Wall> ListWall { get => listWall; set => listWall = value; }
        public List<Door> ListDoor { get => listDoor; set => listDoor = value; }
        public List<Stair> ListStair { get => listStair; set => listStair = value; }
        public List<Window> ListWindow { get => listWindow; set => listWindow = value; }

        public List<AcadEntity> getAllEntities() { return listAllEntities; }
        public List<AcadEntity> getSelectedEntities() { return listSelectedEntities; }
        public List<AcadEntity> getUniqueEntities()
        {
            if (listUniqueEntities == null)
            {
                bool isNewUniqueEntity = false;
                listUniqueEntities = new List<AcadEntity>();
                AcadEntity currentEntity = listAllEntities.FirstOrDefault();
                listUniqueEntities.Add(currentEntity);
                for (int i = 1; i < listAllEntities.Count; i++)
                {
                    foreach (AcadEntity parentEntity in listUniqueEntities)
                    {
                        if (parentEntity.LayerName == listAllEntities[i].LayerName &&
                            parentEntity.ObjectType == listAllEntities[i].ObjectType)
                        {
                            isNewUniqueEntity = false;
                            break;
                        }
                        else
                        {
                            isNewUniqueEntity = true;
                            currentEntity = listAllEntities[i];
                        }
                    }
                    if (isNewUniqueEntity) listUniqueEntities.Add(currentEntity);
                }
            }
            return listUniqueEntities;
        }
        public List<AcadEntity> getUniqueSelectedEntities()
        {
            if (listSelectedEntities.Count != 0)
            {

                bool isNewUniqueEntity = false;
                listUniqueSelectedEntities = new List<AcadEntity>();
                AcadEntity currentEntity = listSelectedEntities.FirstOrDefault();
                listUniqueSelectedEntities.Add(currentEntity);
                for (int i = 1; i < listSelectedEntities.Count; i++)
                {
                    foreach (AcadEntity parentEntity in listUniqueSelectedEntities)
                    {
                        if (parentEntity.LayerName == listSelectedEntities[i].LayerName &&
                            parentEntity.ObjectType == listSelectedEntities[i].ObjectType)
                        {
                            isNewUniqueEntity = false;
                            break;
                        }
                        else
                        {
                            isNewUniqueEntity = true;
                            currentEntity = listSelectedEntities[i];
                        }
                    }
                    if (isNewUniqueEntity) listUniqueSelectedEntities.Add(currentEntity);
                }

            return listUniqueSelectedEntities;
            }
            return null;
        }

    }
}
