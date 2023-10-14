﻿using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DemoACadSharp
{
    public class ParentEntity
    {
        string parentLayerName;
        string parentObjectType;
        List<EntityInfo> entityInfos = new List<EntityInfo>();

        
        public List<EntityInfo> EntityInfos { get => entityInfos; set => entityInfos = value; }
        public string ParentLayerName { get => parentLayerName; set => parentLayerName = value; }
        public string ParentObjectType { get => parentObjectType; set => parentObjectType = value; }
    }
}