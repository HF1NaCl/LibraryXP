using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    internal class Author
    {
        private int idAuthor;
        private string nameAuthor;

        public int IdAuthor { get => idAuthor; set => idAuthor = value; }
        public string NameAuthor { get => nameAuthor; set => nameAuthor = value; }
    }
}
