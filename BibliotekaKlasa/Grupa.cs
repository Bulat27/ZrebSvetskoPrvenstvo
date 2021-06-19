using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotekaKlasa
{
    public class Grupa
    {
        private string imeGrupe;
        private List<Reprezentacija> listaReprezentacija;

        public Grupa(string imeGrupe)
        {
            this.imeGrupe = imeGrupe;
            listaReprezentacija = new List<Reprezentacija>();
        }

        public string ImeGrupe {
            get=>imeGrupe;
            set=>imeGrupe=value; }


        public List<Reprezentacija> ListaReprezentacija
        {
            get => listaReprezentacija;
        }

        public override string ToString()
        {
            return imeGrupe +"," + ispisiReprezentacije();
        }

        private string ispisiReprezentacije()
        {
            string reprezentacije = "";
            foreach(Reprezentacija reprezentacija in listaReprezentacija)
            {
                reprezentacije += reprezentacija;
            }
            return reprezentacije;
        }

        public int brojPojavljivanjaKvalifikacioneZone(String kvalifikacionaZona)
        {
            int brojac = 0;
            foreach(Reprezentacija reprezentacija in ListaReprezentacija)
            {
                if (reprezentacija.KvZona.Equals(kvalifikacionaZona)) brojac++;
            }
            return brojac;
        }
        public void sortirajPremaPoziciji()
        {
            listaReprezentacija = listaReprezentacija.OrderBy(Reprezentacija => Reprezentacija.PozicijaUGrupi).ToList();
        }
        //metoda koja generise utakmice za grupu nad kojom je pozvana i vraca kao string spreman za upis u .csv fajl
        public string izgenerisiUtakmice()
        {
            Random r = new Random();
            string utakmice = "";
            for(int i = 0; i < listaReprezentacija.Count - 1; i++)
            {
                for(int j = i+1; j < listaReprezentacija.Count; j++)
                {
                    utakmice += imeGrupe + " ," + listaReprezentacija[i].Ime + "," + listaReprezentacija[j].Ime + "," + r.Next(0, 51) + "," + r.Next(0, 51)+"\n";
                }
            }
            return utakmice;
        }



    }
       
}
