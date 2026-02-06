using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryXP.Data
{
    internal class JsonHelper
    {
        private static string filePath = "Data/Data.json";

        public static DataBase ReadDB()
        {
            if (!File.Exists(filePath))
                return new DataBase();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<DataBase>(json) ?? new DataBase();
        }

        public static void SaveDB(DataBase dataBase)
        {
            string json = JsonConvert.SerializeObject(dataBase, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static int GetNextID<T>(List<T> list) where T : class
        {
            if (typeof(T) == typeof(User))
            {
                var users = list.Cast<User>().ToList();
                return users.Count == 0 ? 1 : users.Max(u => u.IdUser) + 1;
            }
            else if (typeof(T) == typeof(Loan))
            {
                var loans = list.Cast<Loan>().ToList();
                return loans.Count == 0 ? 1 : loans.Max(u => u.IdLoan) + 1;
            }
            else if (typeof(T) == typeof(Book))
            {
                var books = list.Cast<Book>().ToList();
                return books.Count == 0 ? 1 : books.Max(u => u.IdBook) + 1;
            }
            else if (typeof(T) == typeof(Author))
            {
                var authors = list.Cast<Author>().ToList();
                return authors.Count == 0 ? 1 : authors.Max(u => u.IdAuthor) + 1;
            }

            throw new Exception("Tipo no soportado para auto-increment de ID.");
        }
    }
}
