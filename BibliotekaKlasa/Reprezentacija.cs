using System;
using System.Collections.Generic;

namespace BibliotekaKlasa
{
    public class Reprezentacija
    {
        //sve su i po defaultu private ovde svejedno, ali moze i da se naglasi
        private string ime;
        private string kvZona;
        private int rang;
        private int pozicijaUGrupi;
        private int brojPoena;
        private int koeficijentGolova;
        private string status;//dal je prosla dalje ili nije
        // nema potrebe da ih stavljam u konstruktor, po defaultu ce biti nula, a posle cu svakako dodeliti vrednosti

        public Reprezentacija(string ime, string kvZona, int rang)
        {
            this.ime = ime;
            this.kvZona = kvZona;
            this.rang = rang;
            status = "prosao";//stavicu ga ovako po default-u, a promenicu samo onoj koja nije prosla
        }

        public Reprezentacija() { }

        public string Ime {
            get=>ime; 
            set=>ime=value; 
        }

        public string KvZona
        {
            get => kvZona;
            set => kvZona = value;
        }

        public int Rang
        {
            get => rang;
            set => rang = value;
        }

        public int PozicijaUGrupi
        {
            get => pozicijaUGrupi;
            set => pozicijaUGrupi= value;
        }

        public int BrojPoena
        {
            get => brojPoena;
            set => brojPoena = value;
        }
        public int KoeficijentGolova
        {
            get => koeficijentGolova;
            set => koeficijentGolova = value;
        }

        public string Status
        {
            get => status;
            set => status = value;
        }

        public override string ToString()
        {
            return pozicijaUGrupi + "." + ime + ",";
        }

        //ovo posle izmeni tako da iskoristis i tamo za grupe!!!
        public static int brojPojavljivanjaKontitenta(List<Reprezentacija> lista, string kontinent)
        {
            int brojac = 0;
            foreach (Reprezentacija reprezentacija in lista)
            {
                if (reprezentacija.KvZona.Equals(kontinent)) brojac++;
            }
            return brojac;

        }

       

    }
   
}
