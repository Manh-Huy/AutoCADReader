using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class UnityArchitecture
    {
        string nameArchitecture;
        int numberOfFloor;
        string typeOfRoof;
        List<UnityFloor> listFloor = new List<UnityFloor>();

        public string NameArchitecture { get => nameArchitecture; set => nameArchitecture = value; }
        public int NumberOfFloor { get => numberOfFloor; set => numberOfFloor = value; }
        public string TypeOfRoof { get => typeOfRoof; set => typeOfRoof = value; }
        public List<UnityFloor> ListFloor { get => listFloor; set => listFloor = value; }
    }
}
