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

        private bool isEditingComment;

        public bool IsEditingComment
        {
            get { return isEditingComment; }
            set 
            {
                isEditingComment = value;
                OnPropertyChanged("IsEditingComment");
            }
        }

        


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
                    ReadComments();
                }
            }
        }

        private Comment selectedComment;

        public Comment SelectedComment
        {
            get { return selectedComment; }
            set 
            { 
                selectedComment = value;
                //is this the key to losing focus and the trigger?
                //SelectedCommentChanged(this, new EventArgs());
            }
        }




        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public OpenAddWindowCommand OpenAddWindowCommand { get; set; }

        public RemoveCommand RemoveCommand { get; set; }
        public BeginCommentEditCommand BeginCommentEditCommand { get; set; }

        public HasEditedCommentCommand HasEditedCommentCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SelectedCommentChanged;



        public BookRecordVM()
        {
            
            Books = new ObservableCollection<Book>();
            Comments = new ObservableCollection<Comment>();

            OpenAddWindowCommand = new OpenAddWindowCommand(this);
            RemoveCommand = new RemoveCommand(this);
            BeginCommentEditCommand = new BeginCommentEditCommand(this);
            HasEditedCommentCommand = new HasEditedCommentCommand(this);

            ReadBooks();
            ReadComments();
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
                ReadComments();
            };
            newBookWindow.ShowDialog();
        }


        public void ReadComments()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                //conn.CreateTable<Comment>();

                //filter for selectedBook
                if (selectedBook != null)
                {
                    var comments = conn.Table<Comment>().Where(c => c.BookId == SelectedBook.Id).ToList();
                    
                    //clear everytime to avoid duplicates from previous calls
                    Comments.Clear();

                    foreach (var comment in comments)
                    {
                        Comments.Add(comment);
                    }
                }
                
            }
        }


        public void StartCommentEditing()
        {
            IsEditingComment = true;
        }


        public void HasCommented(Comment comment)
        {
            if (comment != null)
            {
                DatabaseHelper.Update(comment);
                IsEditingComment = false;
                ReadComments();
            }
        }



        

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
