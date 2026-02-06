using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    /// <summary>
    /// El préstamo es bastante condicional si se analiza bien el código. Utiliza clases como el Usuario y el Libro para poder generar el préstamo.
    /// Existen miembros en los que definen realmente como un préstamo, como lo son: isActive, dateLoan y returnLoan. Estos datos determinan si está
    /// activo el préstamo y estas fechas condicionan el puntaje (O historial) del Usuario, afectando su reputación positivamente o negativamente, dependiendo
    /// de su responsabilidad al regresar el libro.
    /// </summary>
    internal class Loan
    {   
        private int idLoan;
        private int idUser;
        private int idBook;
        private bool isActive = true;
        private DateTime dateLoan = DateTime.Now;
        private DateTime? returnLoan = DateTime.Now.AddDays(14);

        public int IdLoan { get => idLoan; set => idLoan = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int IdBook { get => idBook; set => idBook = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public DateTime DateLoan { get => dateLoan; set => dateLoan = value; }
        public DateTime? ReturnLoan { get => returnLoan; set => returnLoan = value; }
    }
}
