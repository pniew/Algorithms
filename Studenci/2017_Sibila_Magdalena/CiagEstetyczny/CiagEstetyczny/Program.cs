using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiagEstetyczny
{
    class Program
    {
        private static int LiczMax = 100000000;

        public static long[] StworzCiag(int dlugosc)
        {
            long[] ciag = new long[dlugosc];

            //wypełnienie pseudolosowymi
            Random gen = new Random();
            for (int a = 0; a < ciag.Length; a++)
            {
                ciag[a] = gen.Next(0, LiczMax);
            }
            return ciag;
        }

        private static Dictionary<long, long> ZliczDictionary(long[] ciag) {
            Dictionary<long, long> slownik = new Dictionary<long, long>();
            foreach (int i in ciag) {
                if (slownik.ContainsKey(i))
                {
                    slownik[i]= slownik[i]+1;
                }
                else {
                    slownik.Add(i, 1);
                }
            }
            return slownik;
        }

        public static void PosortujDictionary(long[] ciag)
        {
            //Stopwatch stoper = new Stopwatch();
            //stoper.Start();
            //zliczenie elementów
            Dictionary < long, long> slownik = ZliczDictionary(ciag);
            //stoper.Stop();
            //Console.WriteLine("zliczanie :" + stoper.Elapsed.TotalSeconds);
            // zmiana wartości w tabeli ciąg na posortowane

            //stoper.Start();
            int c = 0;
            while (c < ciag.Length)
            {
                long nmnl = LiczMax, ile = 0;

                foreach (long klucz in slownik.Keys) {
                    if (klucz < nmnl) {
                        nmnl = klucz;
                    }
                }
                ile = 0;
                //if (slownik.ContainsKey(nmnl)) {
                    ile = slownik[nmnl];
                    //usun najmniejszy element ze slownika
                    slownik.Remove(nmnl);
               // }

                do
                {
                    ciag[c] = nmnl;
                    ile--;
                    c++;
                } while (ile > 0);
            }
            //stoper.Stop();
            //Console.WriteLine("sortowanie :" + stoper.Elapsed.TotalSeconds);
        }

        //sortowanie przez zliczanie
        public static long[,] Zlicz(long[] ciag)
        {

            long[,] tabPom = new long[ciag.Length, 2];
            for (int a = 0; a < tabPom.GetLength(0); a++)
            {
                tabPom[a, 0] = -1;
                tabPom[a, 1] = 0;
            }
            ////Pomiar czasu tego elementu
            //Stopwatch stoper = new Stopwatch();
            //stoper.Start();
            for (int a = 0; a < ciag.Length; a++)
            {
                for (int b = 0; b < tabPom.GetLength(0); b++)
                {
                    if (tabPom[b, 0] == ciag[a])
                    {
                        tabPom[b, 1] += 1;
                        break;
                    }
                    else if (tabPom[b, 0] == -1)
                    {
                        tabPom[b, 0] = ciag[a];
                        tabPom[b, 1] += 1;
                        break;
                    }
                }
            }
            //stoper.Stop();
            //Console.WriteLine("zlicz:" + stoper.Elapsed.Milliseconds);
            ////Pakiecik testowy :)
            //Console.WriteLine("Zliczenie:");
            //foreach (long i in ciag)
            //{ Console.Write("{0,3},      ", i); }
            //Console.WriteLine();
            //foreach (long i in tabPom)
            //{ Console.Write("{0,3}, ", i); }
            //Console.WriteLine();
            return tabPom;

        }

        public static void Posortuj(long[] ciag)
        {
            //zliczenie elementów
            long[,] tabPom = Zlicz(ciag);

            // zmiana wartości w tabeli ciąg na posortowane
            //Stopwatch stoper = new Stopwatch();
            //stoper.Start();
            int c = 0;
            while (c < ciag.Length)
            {
                long poz = 0, nmnl = LiczMax, ile = 0;
                for (int b = 0; b < tabPom.GetLength(0); b++)
                {
                    if (tabPom[b, 0] >= 0 && tabPom[b, 0] < nmnl)
                    {
                        poz = b;
                        nmnl = tabPom[b, 0];
                        ile = tabPom[b, 1];
                    }
                }
                do
                {
                    ciag[c] = nmnl;
                    ile--;
                    c++;
                } while (ile > 0);

                tabPom[poz, 0] = -1;
            }
            //stoper.Stop();
            //Console.WriteLine("wpisz:" + stoper.Elapsed.Milliseconds);
        }

        //przeliczenie elementów ciągu estetycznego
        public static int ZnajdzEst(long[] ciag)
        {
            int dlTym = 1, dlKon = 1;
            for (long i = 1; i < ciag.Length; i++)
            {
                if ((ciag[i] - ciag[i - 1]) <= 1)
                {
                    dlTym++;
                }

                else
                {
                    if (dlTym > dlKon)
                    {
                        dlKon = dlTym;
                        dlTym = 1;
                    }
                    else
                        dlTym = 1;
                }
            }
            if (dlTym > dlKon)
            {
                dlKon = dlTym;
            }
            return dlKon;
        }


        static void Main(string[] args)
        {
            //stworzenie ciągu liczb
            Console.WriteLine("Podaj długość ciągu: ");
            int dlugosc = Convert.ToInt32(Console.ReadLine());
            long[] ciag = StworzCiag(dlugosc);

            Stopwatch stoper = new Stopwatch();

            stoper.Start();
            PosortujDictionary(ciag);
            stoper.Stop();
            Console.WriteLine("Moje sortowanie Dict: " + stoper.ElapsedMilliseconds);
            Console.WriteLine("Długość ciągu to: " + ZnajdzEst(ciag));

            stoper.Restart();
            Posortuj(ciag);
            stoper.Stop();
            Console.WriteLine("Moje sortowanie tablica:" + stoper.ElapsedMilliseconds);
            Console.WriteLine("Długość ciągu to: " + ZnajdzEst(ciag));

            stoper.Restart();
            Array.Sort(ciag);
            stoper.Stop();
            Console.WriteLine("Sortowanie Visual: " + stoper.ElapsedMilliseconds);
            Console.WriteLine("Długość ciągu to: " + ZnajdzEst(ciag));

            //Test sortowania -wydruk posortowanego ciągu
            //Console.WriteLine("Posortowany:");
            //foreach (long i in ciag)
            //{
            //    Console.Write("{0,3},      ", i);
            //}
            //Console.WriteLine();
            //Console.WriteLine("Długość ciągu to: " + ZnajdzEst(ciag));
            Console.ReadKey();
        }

    }
}


