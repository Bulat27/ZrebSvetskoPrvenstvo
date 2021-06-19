﻿using System;
using System.Collections.Generic;

namespace BibliotekaKlasa
{
    public class Reprezentacija
    {
        private string ime;
        private string kvZona;
        private int rang;
        int pozicijaUGrupi;
        // nema potrebe da je stavljam u konstruktor, po defaultu ce biti nula, a posle cu svakako dodeliti vrednosti

        public Reprezentacija(string ime, string kvZona, int rang)
        {
            this.ime = ime;
            this.kvZona = kvZona;
            this.rang = rang;
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
