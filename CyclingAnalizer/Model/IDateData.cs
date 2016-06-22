using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingAnalizer.Model
{
    interface IDateData
    {
        DateTime Date { get; set; }
        int Num { get; set; }
    }
}
