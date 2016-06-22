using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyclingAnalizer.Model;

namespace CyclingAnalizer.ViewModel
{
    class OvertimeLineChartViewModel
    {
        private readonly IEnumerable<IDateData> _data;

        public OvertimeLineChartViewModel(IEnumerable<IDateData> data)
        {
            _data = data;
        }

        public Dictionary<DateTime,int> Data
        {
            get { return _data.ToDictionary(x=>x.Date,x=>x.Num); }
        }
    }
}
