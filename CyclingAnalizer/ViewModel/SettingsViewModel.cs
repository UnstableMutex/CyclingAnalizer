using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingAnalizer.ViewModel
{
    class SettingsViewModel:VMB
    {
        protected override void RealSave()
        {
            AppSettings.Default.Save();
        }

        public string GPXFolder
        {
            get { return AppSettings.Default.GPXFolder; }
            set
            {
                AppSettings.Default.GPXFolder = value;
              RaisePropertyChanged(nameof(GPXFolder));
            }
        }

    }
}
