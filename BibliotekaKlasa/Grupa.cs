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
                    int brojGolova1 = r.Next(0, 51);
                    int brojGolova2 = r.Next(0, 51);
                    utakmice += imeGrupe + " ," + listaReprezentacija[i].Ime + "," + listaReprezentacija[j].Ime + "," + brojGolova1 + "," + brojGolova2 +"\n";
                    upisiPoeneIKoeficijente(listaReprezentacija[i],listaReprezentacija[j],brojGolova1,brojGolova2);
                }
            }
            return utakmice;
        }

        private void upisiPoeneIKoeficijente(Reprezentacija reprezentacija1, Reprezentacija reprezentacija2, int brojGolova1, int brojGolova2)
        {
            switch (Math.Sign(brojGolova1 - brojGolova2))
            {
                case 1:reprezentacija1.BrojPoena += 3;
                    break;
                case -1:reprezentacija2.BrojPoena += 3;
                    break;
                case 0: reprezentacija1.BrojPoena += 1;
                        reprezentacija2.BrojPoena += 1;
                    break;
            }
            reprezentacija1.KoeficijentGolova = brojGolova1 - brojGolova2;
            reprezentacija2.KoeficijentGolova = brojGolova2 - brojGolova1;


        }

        public void sortirajPremaRezultatu()
        {
            //Na ovaj nacin mnogo vecu tezinu dajem broju poena. Samim tim, ako reprezentacije imaju isti broj poena, odlucice koeficijent golova.
            //Sa druge strane, ako reprezentacija sa manjim brojem poena ima bolji koeficijent, i dalje ce presuditi broj poena jer ima mnogo vecu tezinu.
            listaReprezentacija = listaReprezentacija.OrderBy(Reprezentacija => Reprezentacija.BrojPoena*10000 +  Reprezentacija.KoeficijentGolova).ToList();
            promeniPozicijeUGrupi();
        }

        private void promeniPozicijeUGrupi()
        {
            int pozicija = 1;
            foreach(Reprezentacija reprezentacija in listaReprezentacija)
            {
                reprezentacija.PozicijaUGrupi = pozicija;
                if (pozicija == 4) reprezentacija.Status = "";//ova jedina nije prosla
                pozicija++;
            }
        }
        //PROMENI IME ONOM GORE!LOGIKA JE DOBRA, IMENOVANJE NIJE!
        public string toStringSaProlazom()
        {
            string s = "";
            foreach(Reprezentacija reprezentacija in listaReprezentacija)
            {
                s += reprezentacija.PozicijaUGrupi + "." + reprezentacija.Ime +"-"+reprezentacija.Status + ",";
            }
            return s;
        }



    }
       
}
