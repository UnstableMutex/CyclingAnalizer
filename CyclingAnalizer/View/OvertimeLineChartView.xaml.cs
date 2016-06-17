using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyclingAnalizer.View
{
    /// <summary>
    /// Логика взаимодействия для OvertimeLineChartView.xaml
    /// </summary>
    public partial class OvertimeLineChartView : UserControl
    {
        public OvertimeLineChartView()
        {
            InitializeComponent();
            Addsample();
        }

        private void Addsample()
        {
            LineSeries ls = new LineSeries();

        ls.ItemsSource = new Dictionary<DateTime,int>()
        {
            {DateTime.Now ,100 },
            {DateTime.Now.AddMonths(1), 130 },
            {DateTime.Now.AddMonths(2), 150 },
            {DateTime.Now.AddMonths(3), 125 },
            {DateTime.Now.AddMonths(4),155 }

        };
          ls.IndependentValueBinding = new Binding("Key");
            ls.DependentValueBinding = new Binding("Value");
            ch.Series.Add(ls);

        }
    }
}
