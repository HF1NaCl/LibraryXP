using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    /// <summary>
    /// Esta es la "Base de Datos" (Si es que se le puede llamar de esa manera), para reutilizar las clases hechas en la carpeta Models.
    /// Como es reutilizable, entonces es fácil de acceder a través de los llamados de los controladores y con el JsonHelper.
    /// </summary>
    internal class DataBase
    {
        public List<Author> Authors { get; set; } =  new List<Author>();
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
        public List<User> Users { get; set; } = new List<User>();
    }

}
