using BookRecordMVVMSQLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookRecordMVVMSQLite.View.UserControls
{
    /// <summary>
    /// Interaction logic for BookControl.xaml
    /// </summary>
    public partial class BookControl : UserControl
    {



        public Book DisplayBook
        {
            get { return (Book)GetValue(DisplayBookProperty); }
            set { SetValue(DisplayBookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayBook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayBookProperty =
            DependencyProperty.Register("DisplayBook", typeof(Book), typeof(BookControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BookControl book = d as BookControl;

            if (book != null)
            {
                book.titleTextBlock.Text = (e.NewValue as Book).Title;
                book.authorTextBlock.Text = (e.NewValue as Book).Author;
                book.genreTextBlock.Text = (e.NewValue as Book).Genre;
                book.yearReadTextBlock.Text = (e.NewValue as Book).YearRead;
            }
        }

        public BookControl()
        {
            InitializeComponent();
        }
    }
}
