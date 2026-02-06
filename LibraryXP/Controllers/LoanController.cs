using LibraryXP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP.Controllers
{
    internal class LoanController
    {
        public static void CreateLoan(Loan newLoan)
        {
            var db = JsonHelper.ReadDB();

            newLoan.IdLoan = JsonHelper.GetNextID(db.Loans);

            db.Loans.Add(newLoan);
            JsonHelper.SaveDB(db);
        }
        public static List<Loan> GetLoans()
        {
            var db = JsonHelper.ReadDB();
            return db.Loans;
        }
        public static Loan GetLoanByID(int id)
        {
            var db = JsonHelper.ReadDB();
            return db.Loans.FirstOrDefault(u => u.IdLoan == id);
        }
        /// <summary>
        /// Método para contar cuantos préstamos tiene el libro. Esto evita sobrepasarse en el stock.
        /// </summary>
        /// <param name="idBook">El ID del libro.</param>
        /// <returns>Un Int en el que muestra cuántos préstamos tiene activos actualmente.</returns>
        public static int CountActiveLoans(int idBook)
        {
            var db = JsonHelper.ReadDB();

            return db.Loans.Count(l =>
                l.IdBook == idBook &&
                l.IsActive == true // activo
            );
        }
        /// <summary>
        /// Revisa si el usuario en cuestión tiene Préstamos activos.
        /// </summary>
        /// <param name="id">El ID del usuario para revisar préstmos</param>
        /// <returns>Si tiene préstmos, mostrará true.</returns>
        public static bool HasActiveLoanByUser(int id) {
            var db = JsonHelper.ReadDB();

            return db.Loans.Any(l =>
                l.IdUser == id &&
                l.IsActive == true // activo
            );
        }
        /// <summary>
        /// Si el sistema de puntaje de créditos es funcional, entonces podríamos hacer que suba y baje puntaje para el historial de préstamos.
        /// Lógica sería así: Si aún está a la fecha, al devolver se le sube puntaje, pudiendo dar retroceso en caso de error.
        /// Si ya se pasó la fecha, solo se puede cerrar el caso y tener puntaje negativo.
        /// </summary>
        /// <param name="id">Un ID para poder actualizar el Préstamo</param>
        /// <returns>Un bool para indicar si todo ha ido bien o no.</returns>
        public static bool UpdateLoan(int id)
        {
            var db = JsonHelper.ReadDB();
            var loan = db.Loans.FirstOrDefault(u => u.IdLoan == id);

            if (loan == null)
            {
                return false;
            }

            int scoreMeter = 15;
            int idUser = loan.IdUser;
            if (loan.IsActive == false && loan.ReturnLoan >= DateTime.Now)
            {
                //Si el préstamo no está activo y la fecha de regreso todavía no ha sucedido hoy.
                if (HasActiveLoanByUser(idUser) == true)
                {
                    Console.WriteLine("El usuario ya tiene un préstamo activo.");
                    Console.ReadLine();
                    return false;
                }
                UserController.UpdateScoreByUser(db, idUser, -(scoreMeter));
                Console.WriteLine("Se le ha bajado {0} puntos al Usuario", scoreMeter);
                Console.ReadLine();
                loan.IsActive = true;
            }
            else {
                //Caso de que esté activo
                if (loan.ReturnLoan >= DateTime.Now)
                {
                    UserController.UpdateScoreByUser(db, idUser, scoreMeter);
                    Console.WriteLine("Se le ha subido {0} puntos al Usuario", scoreMeter);
                    Console.ReadLine();
                }
                else {
                    UserController.UpdateScoreByUser(db, idUser, -(scoreMeter));
                    Console.WriteLine("Se le ha bajado {0} puntos al Usuario", scoreMeter);
                    Console.ReadLine();
                }
                loan.IsActive = false;
            }

            //Si el sistema de puntaje de créditos es funcional, entonces podríamos hacer que suba y baje puntaje para
            //El historial de préstamos.
            //Lógica sería así: Si aún está a la fecha, al devolver se le sube puntaje, pudiendo dar retroceso en caso de error.
            //Si ya se pasó la fecha, solo se puede cerrar el caso y tener puntaje negativo.

            JsonHelper.SaveDB(db);

            return true;
        }
        public static bool DeleteLoan(int id)
        {
            var db = JsonHelper.ReadDB();
            var loan = db.Loans.FirstOrDefault(u => u.IdLoan == id);

            if (loan == null)
            {
                return false;
            }

            db.Loans.Remove(loan);

            JsonHelper.SaveDB(db);

            return true;
        }
    }
}
