using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryXP.Data
{
    /// <summary>
    /// El pilar fundamental de la conexión de Base de Datos con la conexión de archivo .json para poder desarrollar el proceso.
    /// En él, se almacenan todo lo necesario como para abrir para su lectura y guardar para su escritura. Así como la detección de las 
    /// clases necesarias ya hechas en la DataBase.cs
    /// </summary>
    internal class JsonHelper
    {
        /// <summary>
        /// El directorio en donde se almacenará el .json preestablecido.
        /// </summary>
        private static string filePath = "Data/Data.json";

        /// <summary>
        /// Abre el archivo .json y lo lee.
        /// </summary>
        /// <returns>Devuelve toda la base de datos. (Solo para proyectos pequeños es válido)</returns>
        public static DataBase ReadDB()
        {
            if (!File.Exists(filePath))
                return new DataBase();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<DataBase>(json) ?? new DataBase();
        }
        /// <summary>
        /// Guarda todo lo modificado en la base de datos. Es recomendable abrir una sola vez para que haga sentido el guardado de la Base de datos.
        /// </summary>
        /// <param name="dataBase">La base de datos abierta anteriormente con ReadDB()</param>
        public static void SaveDB(DataBase dataBase)
        {
            string json = JsonConvert.SerializeObject(dataBase, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        /// <summary>
        /// Este método captura el siguiente ID dentro de la clase. Solo verifica lo que es el último número y le agrega un 1.
        /// </summary>
        /// <typeparam name="T">La Clase a detectar.</typeparam>
        /// <param name="list">La lista que incluye la clase verificando el último número ID existente</param>
        /// <returns>Un int que encuentra el último ID + 1</returns>
        /// <exception cref="Exception">Lanza si la clase no existe.</exception>
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
