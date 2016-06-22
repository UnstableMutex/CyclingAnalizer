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
        Dictionary<DateTime,int> ToDic()
        {

            var g = _data.GroupBy(x => x.Date).OrderBy(x=>x.Key);
            Dictionary<DateTime, int> dic = new Dictionary<DateTime, int>();
            foreach (var item in g)
            {
                dic.Add(item.Key, (int)item.Average(x => x.Num));

            }
            return dic;
           
        }

        public Dictionary<DateTime,int> Data
        {
            get { return ToDic(); }
        }
    }
}
