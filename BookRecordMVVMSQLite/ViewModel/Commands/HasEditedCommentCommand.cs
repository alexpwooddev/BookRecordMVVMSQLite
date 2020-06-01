using BookRecordMVVMSQLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookRecordMVVMSQLite.ViewModel.Commands
{
    public class HasEditedCommentCommand : ICommand
    {
        public BookRecordVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public HasEditedCommentCommand(BookRecordVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Comment comment = parameter as Comment;

            VM.HasCommented(comment);
        }
    }
}
