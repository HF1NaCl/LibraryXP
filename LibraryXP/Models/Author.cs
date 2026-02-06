using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    /// <summary>
    /// La clase autor es lo que tiene. Almacena el Nombre del autor solamente para referencias.
    /// </summary>
    internal class Author
    {
        private int idAuthor;
        private string nameAuthor;

        public int IdAuthor { get => idAuthor; set => idAuthor = value; }
        public string NameAuthor { get => nameAuthor; set => nameAuthor = value; }
    }
}
