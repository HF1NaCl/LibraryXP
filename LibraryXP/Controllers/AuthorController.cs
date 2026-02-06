using LibraryXP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP.Controllers
{
    internal class AuthorController
    {
        public static void CreateAuthor(Author newAuthor)
        {
            var db = JsonHelper.ReadDB();

            newAuthor.IdAuthor = JsonHelper.GetNextID(db.Authors);

            db.Authors.Add(newAuthor);
            JsonHelper.SaveDB(db);
        }
        public static List<Author> GetAuthors()
        {
            var db = JsonHelper.ReadDB();
            return db.Authors;
        }
        public static Author GetAuthorByID(int id)
        {
            var db = JsonHelper.ReadDB();
            return db.Authors.FirstOrDefault(u => u.IdAuthor == id);
        }
        public static bool UpdateAuthor(int id, string nameAuthor)
        {
            var db = JsonHelper.ReadDB();
            var author = db.Authors.FirstOrDefault(u => u.IdAuthor == id);

            if (author == null)
            {
                return false;
            }

            author.NameAuthor = nameAuthor;
            
            JsonHelper.SaveDB(db);

            return true;
        }
        /// <summary>
        /// Eliminación del Autor. El metodo puede revisar si tiene libros con préstamos activos para evitar borrar su existencia y crear sinsentidos.
        /// En caso de que ningún libro tenga préstamo activo, este se elimina, y también eliminando sus libros en el proceso.
        /// </summary>
        /// <param name="id">El ID a borrar</param>
        /// <returns>Un bool para indicar si todo ha ido bien o no.</returns>
        public static bool DeleteAuthor(int id)
        {
            var db = JsonHelper.ReadDB();
             
            var author = db.Authors.FirstOrDefault(u => u.IdAuthor == id);

            if (author == null)
            {
                return false;
            }
            //Revisa si tiene libros con préstamos activos
            bool hasActiveLoans = db.Books
            .Where(book => book.IdAuthor == id)
            .Any(book =>
                db.Loans.Any(loan =>
                    loan.IdBook == book.IdBook &&
                    loan.IsActive == true // préstamo activo
                )
            );


            if (hasActiveLoans)
            {
                Console.WriteLine("Uno de sus libros tiene préstamos.");
                Console.ReadLine();
                return false;
            }

            // Eliminar libros del autor
            db.Books.RemoveAll(b => b.IdAuthor == id);

            //Eliminar autor
            db.Authors.Remove(author);
            JsonHelper.SaveDB(db);

            return true;
        }
    }
}
