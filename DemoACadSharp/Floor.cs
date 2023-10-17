using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Floor
    {
        string nameFloor;
        Document entityOfFloor = new Document();
        Document allEntityOfFloor = new Document();

        public string NameFloor { get => nameFloor; set => nameFloor = value; }
        public Document EntityOfFloor { get => entityOfFloor; set => entityOfFloor = value; }
        public Document AllEntityOfFloor { get => allEntityOfFloor; set => allEntityOfFloor = value; }
    }
}
