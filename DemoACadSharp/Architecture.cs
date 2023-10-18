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

        public Architecture(string _nameArchitecture, int _numberOfFloor, string _typeOfRoof)
        {
            this.nameArchitecture = _nameArchitecture;
            this.numberOfFloor = _numberOfFloor;
            this.typeOfRoof = _typeOfRoof;
            this.floors = new List<Floor>();
            for(int i = 0; i < this.numberOfFloor; i++)
            {
                this.floors.Add(new Floor());
            }    
            instance = this;
        }

        public static Architecture getInstance()
        {
            if(instance == null)
            {
                instance = new Architecture();
            }    
            return instance;
        }


        public string NameArchitecture { get => nameArchitecture; set => nameArchitecture = value; }
        public int NumberOfFloor { get => numberOfFloor; set => numberOfFloor = value; }
        public string TypeOfRoof { get => typeOfRoof; set => typeOfRoof = value; }
        public List<Floor> Floors { get => floors; set => floors = value; }
    }
}
