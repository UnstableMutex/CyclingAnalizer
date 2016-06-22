using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CyclingAnalizer.Commands
{
    class OpenViewCommand<T>:ICommand where T: new()
    {
        private readonly IContent _main;

        public OpenViewCommand(IContent main)
        {
            _main = main;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
          var form=new T();
            _main.Content = form;
        }

        public event EventHandler CanExecuteChanged;
    }
}
