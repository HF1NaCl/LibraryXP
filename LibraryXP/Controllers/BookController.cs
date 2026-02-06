using LibraryXP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP.Controllers
{
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
