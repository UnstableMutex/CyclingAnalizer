using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CyclingAnalizer.Commands;
using CyclingAnalizer.Model;
using GalaSoft.MvvmLight.CommandWpf;

namespace CyclingAnalizer.ViewModel
{
    class MainViewModel:VMB,IContent
    {
        private object _content;

        public MainViewModel()
        {
        OpenSettingsCommand = new OpenViewCommand<SettingsViewModel>(this);
            OpenOvertimeLineChartCommand= new SimpleCommand(OpenOvertimeLineChart);
        }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand OpenOvertimeLineChartCommand { get; private set; }

        void OpenOvertimeLineChart()
        {
            IEnumerable<IDateData> data = GetData();
            OvertimeLineChartViewModel vm=new OvertimeLineChartViewModel(data);
            Content = vm;
        }

        private IEnumerable<IDateData> GetData()
        {
            List<IDateData> l = new List<IDateData>();
            AvgCadence c=new AvgCadence();
            c.Cadence = 60;
            c.Date = DateTime.Today.AddMonths(-2);
            l.Add(c);
            c=new AvgCadence();
            c.Cadence = 65;
            c.Date = DateTime.Today.AddMonths(-1);
            l.Add(c);

            return l;
        }

        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }
    }
}
