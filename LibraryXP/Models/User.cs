using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryXP
{
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
