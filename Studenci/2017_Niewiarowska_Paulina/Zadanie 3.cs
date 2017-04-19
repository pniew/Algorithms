using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Element
    {
        public int wartosc = 0;
        public Element nastepny = null;
    }


    class Program
    {
        static void Main()
        {
            Element lista = null;
            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {
                lista = dodajNaKoniec(lista, i);
            }

            /* usuńNaIndeksie jeszcze nie działa  */
            Console.WriteLine("Aktualny stan listy: ");
            wypiszListe(lista);

            lista = usuńNaIndeksie(lista, 3);
            Console.WriteLine("Aktualny stan listy: ");
            wypiszListe(lista);

            lista = usuńNaIndeksie(lista, 0);
            Console.WriteLine("Aktualny stan listy: ");
            wypiszListe(lista);

            lista = usuńNaIndeksie(lista, 7);
            Console.WriteLine("Aktualny stan listy: ");
            wypiszListe(lista);

            Console.ReadKey();
        }

        #region funkcje
        static Element usuńNaIndeksie(Element poczatek, int indeksDelete)
        {
            if(poczatek == null){
                Console.WriteLine("Lista całkowicie pusta!");
                return null;
            }

            int indeksAktualny = 0;
            Element tymczasowy = poczatek.nastepny;

            while (tymczasowy != null && indeksAktualny != indeksDelete)
            {
                indeksAktualny++;
                tymczasowy = poczatek.nastepny;
            }

            // usuńDrugi(tymczasowy); // <-- możemy skorzystać :) 
            return poczatek;
        }

        static Element usuńDrugi(Element jakiśTam)
        {
            if (jakiśTam.nastepny == null)
            {
                Console.WriteLine("Lista ma tylko 1 element");
            }
            else
            {
                Element kolejny = jakiśTam.nastepny;
                jakiśTam.nastepny = kolejny.nastepny;

                // z uwagi, że to C# to sam usuwa 'kolejny' (zbieracz śmieci)
            }
            return jakiśTam;
        }

        static Element usuńOstatni(Element innaNazwa)
        {
            if (innaNazwa == null)
            {
                Console.WriteLine("Ta lista jest już pusta!");
                return null;
            }

            if(innaNazwa.nastepny == null)
            {
                return null; // nie muszę robić elfa bo jest return (zakończenie funkcji)
            }

            if (innaNazwa.nastepny.nastepny == null)
            {
                innaNazwa.nastepny = null;
            }
            else
            {
                usuńOstatni(innaNazwa.nastepny);
            }
            return innaNazwa;
        }

        static Element dodajNaKoniec(Element jakiś, int losowa)
        {
            if (jakiś == null) // wyjątkowa sytuacja, pusta lista aka. 1szy element
            {
                Element tymczasowy = new Element();

                tymczasowy.wartosc = losowa;

                return tymczasowy; // też bez elfów bo return
            }


            if (jakiś.nastepny == null)
            {
                Element tymczasowy = new Element();

                tymczasowy.wartosc = losowa;

                jakiś.nastepny = tymczasowy;
            }
            else
            {
                Element nastepny = jakiś.nastepny;
                dodajNaKoniec(nastepny, losowa);
            }

            return jakiś;
        }

        // NIE DZIAŁA, NIE UŻYWAC PLZ
        static Element dodajNaPoczatek(Element lista, int wartosc)
        {
            Element tymczasowy = new Element();

            lista.wartosc = wartosc;

            tymczasowy.nastepny = lista;

            return tymczasowy;
        }

        static void wypiszListe(Element lista)
        {
            if (lista == null)
            {
                Console.WriteLine("Lista pusta");
            }
            else
            {
                Console.WriteLine(lista.wartosc);

                if (lista.nastepny != null)
                {
                    wypiszListe(lista.nastepny);
                }
            }
        }
        #endregion
    }
}
