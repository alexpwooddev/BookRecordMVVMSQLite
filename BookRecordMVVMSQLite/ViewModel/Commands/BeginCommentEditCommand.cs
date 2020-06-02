using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookRecordMVVMSQLite.ViewModel.Commands
{
    public class BeginCommentEditCommand : ICommand
    {
        public BookRecordVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public BeginCommentEditCommand(BookRecordVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.StartCommentEditing();
        }
    }
}
