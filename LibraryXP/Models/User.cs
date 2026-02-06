using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
    /// <summary>
    /// El usuario es la clase clave para determinar si el Usuario realmente es responsable con devolver libros y registrar aquellos usuarios frecuentes o nuevos
    /// quienes solicitan libros para ser prestados. Tienen un sistema de puntaje para determinar y priorizar aquellos que sean responsables y determinar si
    /// es apto para el préstamo.
    /// </summary>
    internal class User
    {
        private int idUser;
        private string nameUser;
        private string lastNameUser;
        private int creditScoreUser = 15;

        public int IdUser { get => idUser; set => idUser = value; }
        public string NameUser { get => nameUser; set => nameUser = value; }
        public string LastNameUser { get => lastNameUser; set => lastNameUser = value; }
        public int CreditScoreUser { get => creditScoreUser; set => creditScoreUser = value; }
    }
}
