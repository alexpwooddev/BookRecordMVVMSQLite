using BookRecordMVVMSQLite.Model;
using BookRecordMVVMSQLite.View;
using BookRecordMVVMSQLite.ViewModel.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecordMVVMSQLite.ViewModel
{
    public class BookRecordVM : INotifyPropertyChanged
    {

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set 
            {
                selectedBook = value;

                if (selectedBook != null)
                {
                    OnPropertyChanged("SelectedBook");
                }
            }
        }



        public ObservableCollection<Book> Books { get; set; }

        public OpenAddWindowCommand OpenAddWindowCommand { get; set; }

        public RemoveCommand RemoveCommand { get; set; }



        public BookRecordVM()
        {
            Books = new ObservableCollection<Book>();
            OpenAddWindowCommand = new OpenAddWindowCommand(this);
            RemoveCommand = new RemoveCommand(this);

            ReadBooks();
        }



        public void ReadBooks()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                var books = conn.Table<Book>().ToList();

                Books.Clear();

                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
        }


        public void RemoveBook(Book book)
        {
            if (book != null)
            {
                DatabaseHelper.Delete(book);
            }

            ReadBooks();
        }


        public void OpenNewBookWindow()
        {
            NewBookWindow newBookWindow = new NewBookWindow();
            newBookWindow.Closed += (s, eventarg) =>
            {
                ReadBooks();
            };
            newBookWindow.ShowDialog();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
