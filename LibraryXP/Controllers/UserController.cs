using LibraryXP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP.Controllers
{
    internal class UserController
    {
        public static void CreateUser(User newUser)
        {
            var db = JsonHelper.ReadDB();

            newUser.IdUser = JsonHelper.GetNextID(db.Users);

            db.Users.Add(newUser);
            JsonHelper.SaveDB(db);
        }
        public static List<User> GetUsers()
        {
            var db = JsonHelper.ReadDB();
            return db.Users;
        }
        public static User GetUserByID(int id)
        {
            var db = JsonHelper.ReadDB();
            return db.Users.FirstOrDefault(u => u.IdUser == id);
        }
        /// <summary>
        /// El crédito de préstamos se mantiene intacto al cambiar elementos de la identidad del usuario. (Sigue siendo la misma persona)
        /// </summary>
        /// <param name="id">El ID para actualizar el Usuario</param>
        /// <param name="nameUser">Nuevo Nombre</param>
        /// <param name="lastNameUser">Nuevo Apellido</param>
        /// <returns>Un bool para indicar si todo ha ido bien o no.</returns>
        public static bool UpdateUser(int id, string nameUser, string lastNameUser)
        {
            var db = JsonHelper.ReadDB();
            var user = db.Users.FirstOrDefault(u => u.IdUser == id);

            if (user == null)
            {
                return false;
            }

            user.NameUser = nameUser;
            user.LastNameUser = lastNameUser;

            JsonHelper.SaveDB(db);
            
            return true;
        }
        /// <summary>
        /// Método para hacerlo en base a un DB ya existente. No hay necesidad de utilizar sin DB debido que solo exclusivamente lo hace dentro de los Préstamos.
        /// </summary>
        /// <param name="db">La Base de datos abierta anteriormente para poder aplicar cambio</param>
        /// <param name="id">El ID del Usuario para cambiar su puntaje (O el historial crediticio)</param>
        /// <param name="newScore">El puntaje nuevo que se le suma o reste.</param>
        /// <returns>Un bool para indicar si todo ha ido bien o no.</returns>
        public static bool UpdateScoreByUser(DataBase db, int id, int newScore)
        {
            var user = db.Users.FirstOrDefault(u => u.IdUser == id);
            if (user == null)
            {
                Console.WriteLine("No se ha encontrado el usuario.");
                return false;
            }

            user.CreditScoreUser += newScore;

            return true;
        }
        /// <summary>
        /// Elimina todos los préstamos relacionados y el usuario seleccionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un bool para indicar si todo ha ido bien o no.</returns>
        public static bool DeleteUser(int id)
        {
            var db = JsonHelper.ReadDB();
            var user = db.Users.FirstOrDefault(u => u.IdUser == id);

            if (user == null)
            {
                return false;
            }

            //Eliminar todos los préstamos relacionados.
            db.Loans.RemoveAll(l => l.IdUser == id);
            //Eliminar solo el usuario
            db.Users.Remove(user);

            JsonHelper.SaveDB(db);

            return true;
        }
    }
}
