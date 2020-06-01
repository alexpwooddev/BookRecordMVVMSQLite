using BookRecordMVVMSQLite.Model;
using BookRecordMVVMSQLite.View;
using BookRecordMVVMSQLite.ViewModel.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecordMVVMSQLite.ViewModel
{
    public class NewBookVM : INotifyPropertyChanged
    {
        


        private string inputTitle;

        public string InputTitle
        {
            get { return inputTitle; }
            set 
            { 
                inputTitle = value;
                OnPropertyChanged("InputTitle");
            }
        }

        private string inputAuthor;

        public string InputAuthor
        {
            get { return inputAuthor; }
            set 
            {
                inputAuthor = value;
                OnPropertyChanged("InputAuthor");
            }
        }

        private string inputGenre;

        public string InputGenre
        {
            get { return inputGenre; }
            set 
            { 
                inputGenre = value;
                OnPropertyChanged("InputGenre");
            }
        }

        private string inputYearRead;

        public string InputYearRead
        {
            get { return inputYearRead; }
            set 
            { 
                inputYearRead = value;
                OnPropertyChanged("InputYearRead");
            }
        }








        public AddBookCommand AddBookCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler HasAddedBook; 


        public NewBookVM()
        {
            AddBookCommand = new AddBookCommand(this);

        }




        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddBook()
        {
         
            //take input from the 4 fields
            //assign those values to an instance of a book
            //AND create a blank comment 
           
            
            Book book = new Book()
            {
                
                Title = InputTitle,
                Author = InputAuthor,
                Genre = InputGenre,
                YearRead = InputYearRead
            };

            Comment newComment = new Comment()
            {
                BookId = book.Id,
                CommentContent = ""
            };
            

            //Update the database Table with this book and comment
            DatabaseHelper.Insert(book);
            DatabaseHelper.Insert(newComment);

            InputTitle = string.Empty;
            InputAuthor = string.Empty;
            InputGenre = string.Empty;
            InputYearRead = string.Empty;

            //TODO: close this window            
            //TODO: Maybe an event handler that fires now - then main window can subscribe to that and refresh?
            if (HasAddedBook != null)
            {
                HasAddedBook(this, new EventArgs());
            }
        }
    }
}
