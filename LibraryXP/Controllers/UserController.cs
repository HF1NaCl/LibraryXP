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
            //El crédito de préstamos se mantiene intacto al cambiar elementos de la identidad del usuario. (Sigue siendo la misma persona)

            JsonHelper.SaveDB(db);
            
            return true;
        }
        //Método para hacerlo en base a un DB ya existente. No hay necesidad de utilizar sin DB debido que solo exclusivamente lo hace dentro de los Préstamos.
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
