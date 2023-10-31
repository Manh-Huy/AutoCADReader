using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class FactoryUnityEntity
    {
        public static dynamic createObjectUnity(AcadEntity entity, string typeOfEntityUnity)
        {
            switch (typeOfEntityUnity)
            {
                case "None":
                    AcadEntity tempEntity = entity;
                    return tempEntity;
                case "Wall":
                    Wall wallEntity = new Wall("Wall");
                    wallEntity.Id = entity.Id;
                    wallEntity.LayerName = entity.LayerName;
                    wallEntity.ObjectType = entity.ObjectType;
                    wallEntity.Coordinates = entity.Coordinates;
                    return wallEntity;
                case "Stair":
                    Stair stairEntity = new Stair("Stair");
                    stairEntity.Id = entity.Id;
                    stairEntity.LayerName = entity.LayerName;
                    stairEntity.ObjectType = entity.ObjectType;
                    stairEntity.Coordinates = entity.Coordinates;
                    return stairEntity;
                case "Door":
                    Door doorEntity = new Door("Door");
                    doorEntity.Id = entity.Id;
                    doorEntity.LayerName = entity.LayerName;
                    doorEntity.ObjectType = entity.ObjectType;
                    doorEntity.Coordinates = entity.Coordinates;
                    return doorEntity;
                case "Window":
                    Window windowEntity = new Window("Window");
                    windowEntity.Id = entity.Id;
                    windowEntity.LayerName = entity.LayerName;
                    windowEntity.ObjectType = entity.ObjectType;
                    windowEntity.Coordinates = entity.Coordinates;
                    return windowEntity;
                case "Power":
                    Power powerEntity = new Power("Power");
                    powerEntity.Id = entity.Id;
                    powerEntity.LayerName = entity.LayerName;
                    powerEntity.ObjectType = entity.ObjectType;
                    powerEntity.Coordinates = entity.Coordinates;
                    return powerEntity;
            }
            return null;
        }


        // Em Tín ráng viết hàm này để tránh duplicate mà đé o được =))))))))
        /*private dynamic copyProperties(entity entity)
        {
            var copyObject = entity;
            copyObject.Id = entity.Id;
            copyObject.LayerName = entity.LayerName;
            copyObject.ObjectType = entity.ObjectType;
            copyObject.Coordinates = entity.Coordinates;
            copyObject.TypeOfentity = entity.TypeOfentity;
            return copyObject;
        }*/
    }
}
