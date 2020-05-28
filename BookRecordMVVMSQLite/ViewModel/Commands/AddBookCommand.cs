using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookRecordMVVMSQLite.ViewModel.Commands
{
    public class AddBookCommand : ICommand
    {
        public NewBookVM VM { get; set; }


        public event EventHandler CanExecuteChanged;

        public AddBookCommand(NewBookVM vm)
        {
            VM = vm;
     
        }


        public bool CanExecute(object parameter)
        {
            //TODO: require all fields be filled
            return true;
        }

        public void Execute(object parameter)
        {
            VM.AddBook();
     
        }
    }
}
