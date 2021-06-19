using System;
using System.Collections.Generic;
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
            return imeGrupe +" " + ispisiReprezentacije();
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

    }
       
}
