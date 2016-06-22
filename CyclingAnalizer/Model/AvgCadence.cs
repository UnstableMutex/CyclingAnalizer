using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingAnalizer.Model
{
    class AvgCadence:IDateData
    {
        public DateTime Date { get; set; }
        public int Cadence { get; set; }

    

        int IDateData.Num  {get { return Cadence; } set { Cadence = value; } }
    }
}
