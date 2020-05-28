using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecordMVVMSQLite.Model
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Genre { get; set; }

        public string YearRead { get; set; }


        public override string ToString()
        {
            return $"{Title} - {Author} - {Genre} - {YearRead}";
        }
    }
}
