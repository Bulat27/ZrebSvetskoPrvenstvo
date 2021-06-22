using System;
using System.Collections.Generic;
using System.Linq;
using BibliotekaKlasa;

namespace ZrebSvetskoPrvenstvo
{
    class Program
    {
        
        public static List<Reprezentacija> listaReprezentacija = new List<Reprezentacija>();
        public static List<Reprezentacija> prviSesir = new List<Reprezentacija>();
        public static List<Reprezentacija> drugiSesir = new List<Reprezentacija>();
        public static List<Reprezentacija> treciSesir = new List<Reprezentacija>();
        public static List<Reprezentacija> cetvrtiSesir = new List<Reprezentacija>();
        public static Grupa grupaA;
        public static Grupa grupaB;
        public static Grupa grupaC;
        public static Grupa grupaD;
        public static Grupa grupaE;
        public static Grupa grupaF;
        public static Grupa grupaG;
        public static Grupa grupaH;

        public static List<Grupa> iteracionaLista;

        static void Main(string[] args)
        {
            //bolja je preksa praviti konstante nego hard-kodovati ove putanje i nazive fajlova, ali mali je program tako da nije veliki problem
            ucitajPodatke("D:\\Users\\Dragon\\source\\repos\\ZrebSvetskoPrvenstvo\\ZrebSvetskoPrvenstvo\\bin\\Debug\\netcoreapp3.1\\ulaz.csv");

            if (!validniPodaci())
            {
                Console.WriteLine("NISU VALIDNI PODACI!");//ovde moze i Exception da se baci, nije bas naglaseno, ali svakako program ne nastavlja sa radom
                return;
            }

            napraviSesire();
            inicijalizujGrupe();
            napraviGrupe();
            dodeliPozicije();
            upisiUCSVFajl("grupe.csv", grupeZaUpis());
            upisiUCSVFajl("rezultatiUtakmica.csv", utakmiceZaUpis());
            sortirajSvePoRezultatu();
            upisiUCSVFajl("sledecaFaza.csv", krajnjiIzgledGrupaZaUpis());

        }
        

        private static void sortirajSvePoRezultatu()
        {
            foreach (Grupa grupa in iteracionaLista) grupa.sortirajPremaRezultatu();
        }

        //Generise string koji ce se upistati u sledecaFaza.csv

        private static string krajnjiIzgledGrupaZaUpis()
        {
            String s = "";
            foreach(Grupa grupa in iteracionaLista)
            {
                s += grupa.ImeGrupe + "," + grupa.toStringSaProlazom().Trim(',').Trim('-')+"\n";
            }
            return s;
        }

        private static string utakmiceZaUpis()
        {
            string s = "";
            foreach(Grupa grupa in iteracionaLista)
            {
                s += grupa.izgenerisiUtakmice();
            }
            return s;
        }

        private static void upisiUCSVFajl(string nazivFajla,string upis)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(nazivFajla, false))
            {
                sw.Write(upis);
            }
        }

        private static string grupeZaUpis()
        {
            string s = "";
            foreach(Grupa grupa in iteracionaLista)
            {
                s += grupa.ToString().Trim(',') + "\n";
            }
            return s;
        }

        private static void dodeliPozicije()
        {
            foreach (Grupa grupa in iteracionaLista) dodeliPozicijeGrupi(grupa);
        }

        //Ekipa iz prvog sesira ima rang  1-8, ona dobija poziciju jedan, a ostale dobijaju random pozicije
        private static void dodeliPozicijeGrupi(Grupa grupa)
        {
            for(int i =0;i<grupa.ListaReprezentacija.Count;i++)
            {
                if (grupa.ListaReprezentacija[i].Rang <= 8)
                {
                    grupa.ListaReprezentacija[i].PozicijaUGrupi = 1;
                    Reprezentacija pomocna = grupa.ListaReprezentacija[i];
                    grupa.ListaReprezentacija.Remove(pomocna);//sklanjam je pa je ubacujem na prvo mesto kako bih znao gde je i onda mogu nasumicno da prodjem kroz ostale
                    grupa.ListaReprezentacija.Insert(0, pomocna);//Verovatno bi bilo efikasnije samo pomeriti element, ali male su liste, tako da ovde nije toliko bitno
                }
            }
            //sad kad znam da je ovaj sa brojem jedan na prvoj poziciji, dodelicu nasumicno ovim ostalim
            int brojac = 2;
            Random r = new Random();
            foreach (int i in Enumerable.Range(1, 3).OrderBy(x => r.Next()))
            {
                grupa.ListaReprezentacija[i].PozicijaUGrupi = brojac;
                brojac++;
            }
            grupa.sortirajPremaPoziciji();
        }

        public static bool validniPodaci()
        {
             return (
                Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Afrika") == 5 && Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Azija") == 5 &&
                Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Evropa") == 14 && Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Severna i Centralna Amerika") == 3 &&
                (Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Okeanija") == 0 || Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Okeanija") == 1) &&
                (Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Juzna Amerika") == 4 || Reprezentacija.brojPojavljivanjaKontitenta(listaReprezentacija, "Juzna Amerika") == 5)
             );
            
        }


        private static void inicijalizujGrupe()
        {
            grupaA = new Grupa("A");
            grupaB=  new Grupa("B");
            grupaC = new Grupa("C");
            grupaD = new Grupa("D");
            grupaE = new Grupa("E");
            grupaF = new Grupa("F");
            grupaG = new Grupa("G");
            grupaH = new Grupa("H");

            iteracionaLista = new List<Grupa>();
            iteracionaLista.Add(grupaA);
            iteracionaLista.Add(grupaB);
            iteracionaLista.Add(grupaC);
            iteracionaLista.Add(grupaD);
            iteracionaLista.Add(grupaE);
            iteracionaLista.Add(grupaF);
            iteracionaLista.Add(grupaG);
            iteracionaLista.Add(grupaH);
        }

        private static void ucitajPodatke(string putanja)
        {
            string path = putanja;

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                //Bolja je praksa koristiti TryParse, ali mi ovde nije potrebno jer znam da su dobri podaci
                listaReprezentacija.Add(new Reprezentacija(columns[0], columns[1], Int32.Parse(columns[2])));
               
            }
            listaReprezentacija = listaReprezentacija.OrderBy(Reprezentacija => Reprezentacija.Rang).ToList();
        }
        private static void napraviSesire()
        {
            prviSesir = listaReprezentacija.Take(8).ToList();
            drugiSesir = listaReprezentacija.Skip(8).Take(8).ToList();
            treciSesir = listaReprezentacija.Skip(16).Take(8).ToList();
            cetvrtiSesir = listaReprezentacija.Skip(24).Take(8).ToList();


        }
        private static void napraviGrupe()
        {
            bool nasaoKombinaciju;

            //Napravicu shallow kopije kako bih resio situaciju u kojoj mora ponovo da se izvlaci jer ne mogu biti zadovoljenja ogranicenja. Shallow kopije su ovde dovoljne
            //jer svakako necu da menjam sama polja objekta tipa Reprezentacija
            //radniSesir je u stvari taj rezervni sesir jer ne zelim da menjam stanje pravog sesira kako bih mogao posle ponovo da pocnem ispocetka ako je potrebno.
            List<Reprezentacija> radniPrviSesir = new List<Reprezentacija>(prviSesir);
            List<Reprezentacija> radniDrugiSesir = new List<Reprezentacija>(drugiSesir);
            List<Reprezentacija> radniTreciSesir = new List<Reprezentacija>(treciSesir);
            List<Reprezentacija> radniCetvrtiSesir = new List<Reprezentacija>(cetvrtiSesir);

            //Ove ovde provere su potrebne jer bi nemoguca kombinacija potencijalno mogla da nastane u nakon obrade bilo kog sesira (osim prvog).
            obradiSesir(radniPrviSesir);
            nasaoKombinaciju= obradiSesir(radniDrugiSesir);
            if (nasaoKombinaciju)nasaoKombinaciju= obradiSesir(radniTreciSesir);
            else return;
            if(nasaoKombinaciju) obradiSesir(radniCetvrtiSesir);

        }

        private static bool obradiSesir(List<Reprezentacija> sesir)
        {
            int brojac = 0;
            int sekundarniBrojac = 0;
            while (sesir.Any())
            {
                
                int pozicija = generisiRandomPoziciju(sesir.Count);

                if(iteracionaLista[brojac % 8].ListaReprezentacija.Count == 4)
                {
                    brojac++;
                    continue;
                }

                if (((iteracionaLista[brojac % 8].brojPojavljivanjaKvalifikacioneZone(sesir[pozicija].KvZona)==0) || 
                    (sesir[pozicija].KvZona.Equals("Evropa") && iteracionaLista[brojac % 8].brojPojavljivanjaKvalifikacioneZone(sesir[pozicija].KvZona) <= 1)))
                {
                    iteracionaLista[brojac % 8].ListaReprezentacija.Add(sesir[pozicija]);
                    sesir.RemoveAt(pozicija);
                    brojac++;
                    
                  
                }
                sekundarniBrojac++;


                // Ovo je situacija u kojoj ogranicenja ne dozvoljavaju da se ekipa iz trenutnog sesira ubaci u trenutnu grupu. Cak i da preskocimo taj sesir,
                //pa da se posle vratimo, nikad nece biti ispunjeno da svaka grupa ima tacno jednu ekipu iz svakog sesira. Zato cemo ovde samo ispocetka ponoviti izvlacenje.
                if (sekundarniBrojac > 1000)
                {
                    inicijalizujGrupe();//ponovna inicijalizacija kako bismo ocistili grupe
                    napraviGrupe();
                    return false;
                }
            }
            return true;
        }

        private static int generisiRandomPoziciju(int count)
        {
            var random = new Random();
            return random.Next(0,count);
        }
    }
}
