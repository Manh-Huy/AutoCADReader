using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class House
    {
        string nameHouse;
        int numberOfFloor;
        string topFloor;
        List<Floor> listFloor;

        public string NameHouse { get => nameHouse; set => nameHouse = value; }
        public int NumberOfFloor { get => numberOfFloor; set => numberOfFloor = value; }
        public string TopFloor { get => topFloor; set => topFloor = value; }
        public List<Floor> ListFloor { get => listFloor; set => listFloor = value; }
    }
}
