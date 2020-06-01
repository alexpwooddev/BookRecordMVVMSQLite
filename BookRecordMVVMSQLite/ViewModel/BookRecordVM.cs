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
using System.Windows.Controls;

namespace BookRecordMVVMSQLite.ViewModel
{
    public class BookRecordVM : INotifyPropertyChanged
    {

        private string searchQuery;

        public string SearchQuery
        {
            get { return searchQuery; }
            set 
            { 
                if (value == "")
                {
                    ReadBooks();
                }
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged("SearchQuery");
                    SearchBooks();
                }
                
            }
        }


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



        public void SearchBooks()
        {
            var filteredList = Books.Where(b => b.Author.ToLower().Contains(SearchQuery.ToLower())
            | b.Title.ToLower().Contains(SearchQuery.ToLower()) | b.Genre.ToLower().Contains(SearchQuery.ToLower())
            | b.YearRead.ToLower().Contains(SearchQuery.ToLower())).ToList();

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                
                Books.Clear();

                foreach (var book in filteredList)
                {
                    Books.Add(book);
                }
            }


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
