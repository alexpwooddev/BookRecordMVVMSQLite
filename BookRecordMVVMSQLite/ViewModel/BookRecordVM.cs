using BookRecordMVVMSQLite.Model;
using BookRecordMVVMSQLite.View;
using BookRecordMVVMSQLite.ViewModel.Commands;
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
        }




        public void RemoveBook(Book book)
        {
            if (book != null)
            {
                DatabaseHelper.Delete(book);
            }
        }


        public void OpenNewBookWindow()
        {
            NewBookWindow newBookWindow = new NewBookWindow();
            newBookWindow.ShowDialog();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
