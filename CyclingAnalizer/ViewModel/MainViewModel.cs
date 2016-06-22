using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CyclingAnalizer.Commands;
using CyclingAnalizer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using MKCoolsoft.GPXLib;
using System.Xml;
using System.IO;
using System.Globalization;

namespace CyclingAnalizer.ViewModel
{
    class MainViewModel : VMB, IContent
    {
        private object _content;

        public MainViewModel()
        {
            OpenSettingsCommand = new OpenViewCommand<SettingsViewModel>(this);
            OpenOvertimeLineChartCommand = new SimpleCommand(OpenOvertimeLineChart);
        }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand OpenOvertimeLineChartCommand { get; private set; }

        void OpenOvertimeLineChart()
        {
            IEnumerable<IDateData> data = GetData();
            OvertimeLineChartViewModel vm = new OvertimeLineChartViewModel(data);
            Content = vm;
        }

        private IEnumerable<IDateData> GetData()
        {




            var folder = AppSettings.Default.GPXFolder;
            var files = Directory.GetFiles(folder, "*.gpx");
            TaskFactory<IDateData> tf = new TaskFactory<IDateData>();
            List<Task<IDateData>> l = new List<Task<IDateData>>();
            foreach (var item in files)
            {
                // GetAvgCadFromGPX(item);
                var t = tf.StartNew(() => GetAvgCadFromGPX(item));
                l.Add(t);
            }
          
  Task.WaitAll(l.ToArray());
          




            // string fn = @"C:\Users\storm\Dropbox\garmin\activities\20150816-021219-Ride.gpx";
      
            //  double avg = GetAvgCadFromGPX(fn);

            var d = l.Select(x => x.Result).Where(x=>x.Num>0);


            //List<IDateData> l = new List<IDateData>();
            //AvgCadence c=new AvgCadence();
            //c.Cadence = 60;
            //c.Date = DateTime.Today.AddMonths(-2);
            //l.Add(c);
            //c=new AvgCadence();
            //c.Cadence = 65;
            //c.Date = DateTime.Today.AddMonths(-1);
            //l.Add(c);

            //return l;
         




            return d.OrderBy(x => x.Date);
        }

        private static IDateData GetAvgCadFromGPX(string fn)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                XmlDocument doc = new XmlDocument();
                string sd = Path.GetFileName(fn).Substring(0, 8);
                DateTime dt = DateTime.ParseExact(sd, "yyyyMMdd", provider);
                doc.Load(fn);


                var cads = new List<ushort>();

                foreach (XmlNode item in doc["gpx"]["trk"]["trkseg"].ChildNodes)
                {
                    var point = item;

                    var ex = point["extensions"].FirstChild;
                    var cad = ex["gpxtpx:cad"];
                    var rcad = ushort.Parse(cad.FirstChild.Value);
                    cads.Add(rcad);
                }
                var avg = cads.Where(x => x > 0).Average(x => x);
                return new AvgCadence() { Cadence = (int)avg, Date = dt };
            }
            catch (Exception)
            {

                return new AvgCadence();
            }

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

    [Serializable]
    public class NoCadenceException : Exception
    {
        public NoCadenceException() { }
        public NoCadenceException(string message) : base(message) { }
        public NoCadenceException(string message, Exception inner) : base(message, inner) { }
        protected NoCadenceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
