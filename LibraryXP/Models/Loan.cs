using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
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
