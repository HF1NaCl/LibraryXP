using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    /// <summary>
    /// El libro es una clase más específica en miembros. Referencia con código para su búsqueda manual, así como la asignación de un Autor, género, y año
    /// para una idea a lo que se refiere en caso de repetición o similitudes en títulos.
    /// El stock es un límite para realizar préstamos posteriores, evitando la molestia de prestar un libro que no está disponible.
    /// </summary>
    internal class Book
    {
        private int idBook;
        private string codeBook;
        private string nameBook;
        private int idAuthor;
        private string genre;
        private int year;
        private int totalStockBook;

        public int IdBook { get => idBook; set => idBook = value;  }
        public string CodeBook { get => codeBook; set => codeBook = value; }
        public string NameBook { get => nameBook; set => nameBook = value; }
        public int IdAuthor { get => idAuthor; set => idAuthor = value; }
        public string Genre { get => genre; set => genre = value; }
        public int Year { get => year; set => year = value; }
        public int TotalStockBook { get => totalStockBook; set => totalStockBook = value; }
    }
}
