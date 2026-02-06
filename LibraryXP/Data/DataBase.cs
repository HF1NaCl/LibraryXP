using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    internal class DataBase
    {
        public List<Author> Authors { get; set; } =  new List<Author>();
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
        public List<User> Users { get; set; } = new List<User>();
    }

}
