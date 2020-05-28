using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookRecordMVVMSQLite.ViewModel.Commands
{
    public class OpenAddWindowCommand : ICommand
    {
        public BookRecordVM VM { get; set; }

        public event EventHandler CanExecuteChanged;


        public OpenAddWindowCommand(BookRecordVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.OpenNewBookWindow();
        }
    }
}
