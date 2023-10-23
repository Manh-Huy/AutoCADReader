using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoACadSharp
{
    public class Architecture
    {
        string nameArchitecture;
        int numberOfFloor;
        string typeOfRoof;
        List<Floor> floors;

        private static Architecture instance;
        private Architecture()
        {

        }

        public Architecture(int _numberOfFloor)
        {
            this.numberOfFloor = _numberOfFloor;
            this.floors = new List<Floor>();
            this.floors.Add(new Floor());
            this.floors[0].Order = 1;

            instance = this;
        }

        public static Architecture getInstance()
        {
            if(instance == null)
            {
                instance = new Architecture(1);
            }    
            return instance;
        }


        public string NameArchitecture { get => nameArchitecture; set => nameArchitecture = value; }
        public int NumberOfFloor { get => numberOfFloor; set => numberOfFloor = value; }
        public string TypeOfRoof { get => typeOfRoof; set => typeOfRoof = value; }
        public List<Floor> Floors { get => floors; set => floors = value; }
    }
}
