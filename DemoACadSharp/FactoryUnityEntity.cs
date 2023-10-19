using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class FactoryUnityEntity
    {
        public static dynamic createObjectUnity(UnityEntity unityEntity)
        {
            FactoryUnityEntity factory = new FactoryUnityEntity();
            switch (unityEntity.TypeOfUnityEntity)
            {
                case "None":
                    UnityEntity tempEntity = new UnityEntity("None");
                    return tempEntity;
                case "Wall":
                    Wall wallEntity = new Wall("Wall");
                    wallEntity.Id = unityEntity.Id;
                    wallEntity.LayerName = unityEntity.LayerName;
                    wallEntity.ObjectType = unityEntity.ObjectType;
                    wallEntity.Coordinates = unityEntity.Coordinates;
                    wallEntity.TypeOfUnityEntity = unityEntity.TypeOfUnityEntity;
                    return wallEntity;
                case "Stair":
                    Stair stairEntity = new Stair("Stair");
                    stairEntity.Id = unityEntity.Id;
                    stairEntity.LayerName = unityEntity.LayerName;
                    stairEntity.ObjectType = unityEntity.ObjectType;
                    stairEntity.Coordinates = unityEntity.Coordinates;
                    stairEntity.TypeOfUnityEntity = unityEntity.TypeOfUnityEntity;
                    return stairEntity;
                case "Door":
                    Door doorEntity = new Door("Door");
                    doorEntity.Id = unityEntity.Id;
                    doorEntity.LayerName = unityEntity.LayerName;
                    doorEntity.ObjectType = unityEntity.ObjectType;
                    doorEntity.Coordinates = unityEntity.Coordinates;
                    doorEntity.TypeOfUnityEntity = unityEntity.TypeOfUnityEntity;
                    return doorEntity;
                case "Power":
                    Power powerEntity = new Power("Power");
                    powerEntity.Id = unityEntity.Id;
                    powerEntity.LayerName = unityEntity.LayerName;
                    powerEntity.ObjectType = unityEntity.ObjectType;
                    powerEntity.Coordinates = unityEntity.Coordinates;
                    powerEntity.TypeOfUnityEntity = unityEntity.TypeOfUnityEntity;
                    return powerEntity;
            }
            return null;
        }


        // Em Tín ráng viết hàm này để tránh duplicate mà đé o được =))))))))
        private dynamic copyProperties(UnityEntity unityEntity)
        {
            var copyObject = unityEntity;
            copyObject.Id = unityEntity.Id;
            copyObject.LayerName = unityEntity.LayerName;
            copyObject.ObjectType = unityEntity.ObjectType;
            copyObject.Coordinates = unityEntity.Coordinates;
            copyObject.TypeOfUnityEntity = unityEntity.TypeOfUnityEntity;
            return copyObject;
        }
    }
}
