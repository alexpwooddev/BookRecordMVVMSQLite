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
    /// Interaction logic for CommentControl.xaml
    /// </summary>
    public partial class CommentControl : UserControl
    {


        public Comment DisplayComment
        {
            get { return (Comment)GetValue(DisplayCommentProperty); }
            set { SetValue(DisplayCommentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayComment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayCommentProperty =
            DependencyProperty.Register("DisplayComment", typeof(Comment), typeof(CommentControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommentControl commentControl = d as CommentControl;

            if(commentControl != null)
            {
                commentControl.commentContentTextBlock.Text = (e.NewValue as Comment).CommentContent;
            }
        }

        public CommentControl()
        {
            
            InitializeComponent();
        }
    }
}
