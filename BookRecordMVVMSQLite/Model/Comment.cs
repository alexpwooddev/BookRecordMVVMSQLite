using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecordMVVMSQLite.Model
{
    class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int BookId { get; set; }

        public string CommentContent { get; set; }
    }
}
