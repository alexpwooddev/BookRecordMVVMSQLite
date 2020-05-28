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

        NewBookWindow newBookWindow = new NewBookWindow();



        public AddBookCommand AddBookCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddBook()
        {
            //take input from the 4 fields
            //assign those values to an instance of a book
            Book book = new Book()
            {
                Title = newBookWindow.titleTextBox2.Text,
                Author = newBookWindow.authorTextBox2.Text,
                Genre = newBookWindow.genreTextBox2.Text,
                YearRead = newBookWindow.yearReadTextBox2.Text
            };

            //Update the database Table with this book
            DatabaseHelper.Insert(book);

            //TODO: close this window


            
            //Maybe an event handler that fires now - then main window can subscribe to that and refresh?
        }
    }
}
