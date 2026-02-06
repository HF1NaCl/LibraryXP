using LibraryXP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP.Controllers

    internal class BookController
    {
        public static void CreateBook(Book newBook)
        {
            var db = JsonHelper.ReadDB();

            newBook.IdBook = JsonHelper.GetNextID(db.Books);

            db.Books.Add(newBook);
            JsonHelper.SaveDB(db);
        }
        public static List<Book> GetBooks()
        {
            var db = JsonHelper.ReadDB();
            return db.Books;
        }
        public static Book GetBookByID(int id)
        {
            var db = JsonHelper.ReadDB();
            return db.Books.FirstOrDefault(u => u.IdBook == id);
        }
        /// <summary>
        /// Solo una actualización de Libro. Si bien, este método no está condicionado, es recomendable condicionarlo externamente para utilizar y verificar bien los préstamos activos.
        /// </summary>
        /// <param name="id">ID del Libro a editar.</param>
        /// <param name="codeBook">Nuevo Código para el Libro</param>
        /// <param name="nameBook">Nuevo Nombre para el Libro</param>
        /// <param name="idAuthor">Asignación de otro ID de Autor para el Libro</param>
        /// <param name="genre">Nuevo Género para el Libro</param>
        /// <param name="year">Nuevo Año para el Libro</param>
        /// <param name="totalStockBook">Nuevo Stock para el Libro. Condicionado por la cantidad de préstamos vigentes, y debe ser igual o mayor que los préstamos existentes.</param>
        /// <returns>Un bool para indicar si ha sido exitoso la ejecución del método.</returns>
        public static bool UpdateBook(int id, string codeBook, string nameBook, int idAuthor, string genre, int year, int totalStockBook)
        {
            var db = JsonHelper.ReadDB();
            var book = db.Books.FirstOrDefault(u => u.IdBook == id);

            if (book == null)
            {
                return false;
            }

            book.CodeBook = codeBook;
            book.NameBook = nameBook;
            book.IdAuthor = idAuthor;
            book.Genre = genre;
            book.Year = year;
            book.TotalStockBook = totalStockBook;

            JsonHelper.SaveDB(db);

            return true;
        }
        /// <summary>
        /// Solo una eliminación del Libro. En él, la diferencia que se observa, es que primero, verifica si quedan préstamos activos, para evitar crear sinsentido de datos
        /// en los préstamos. Si no queda ningún préstamo activo (Todos han sido cerrados), se borran primero los préstamos (Sin afectar el puntaje de responsabilidad al usuario)
        /// y posteriormente elimina el Libro.
        /// </summary>
        /// <param name="id">ID para eliminar el Libro</param>
        /// <returns>Un bool para indicar si el método ha sido ejecutado exitosamente.</returns>
        public static bool DeleteBook(int id)
        {
            var db = JsonHelper.ReadDB();
            var book = db.Books.FirstOrDefault(u => u.IdBook == id);

            if (book == null)
            {
                return false;
            }

            int activeLoans = LoanController.CountActiveLoans(id);

            if (activeLoans > 0)
            {
                Console.WriteLine("Quedan préstamos pendientes");
                Console.ReadLine();
                return false;
            }
            //Eliminar todos los préstamos relacionados.
            db.Loans.RemoveAll(l => l.IdBook == id);
            //Eliminar el libro en específico
            db.Books.Remove(book);

            JsonHelper.SaveDB(db);

            return true;
        }
    }
}
