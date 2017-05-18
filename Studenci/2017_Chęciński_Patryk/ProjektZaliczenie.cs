using System;

namespace Shaker {
    class Program {

        static T [] ArrayReverse<T>(T [] a) {
            for(int i = 0; i < a.Length / 2; i++) {
                T tmp = a[i];
                a[i] = a[a.Length - i - 1];
                a[a.Length - i - 1] = tmp;
            }
            return a;
        }

        static int Reverse(int n) {
            char[] array = n.ToString().ToCharArray();
            ArrayReverse(array);
            return int.Parse(new string(array));
        }

        static void Main(string[] args) {

            bool debug = !Console.IsInputRedirected;

            int size = 0;
            int[] numbers = null;

            try {
                size = int.Parse(Console.ReadLine());

                if (size < 1 || size > 1000000) {
                    throw new Exception("Nieprawidłowy zakres liczb (n)");
                 }
                numbers = new int[size];

                for (int i = 0; i < size; i++) {
                    numbers[i] = int.Parse(Console.ReadLine());

                    if(numbers[i] < 0 || numbers[i] > 1000000000) {
                        string exceptionString = string.Format("Nieprawidłowy zakres liczb dla (a[{0}])", i);
                        throw new Exception(exceptionString);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Wystąpił błąd: " + e.Message);
                Console.WriteLine(numbers[0]);

                if (debug) {
                    Console.ReadKey();
                }

                return;
            }

            while(size > 1) {
                int largest = 0, largestIndex = 0;


                for(int i = 0; i < size; i++) {
                    if(numbers[i] > largest) {
                        largest = numbers[i];
                        largestIndex = i;
                    }
                    if(debug) Console.Write(numbers[i] + ", ");
                }

                if(debug) Console.Write("wyrzuciłem: " + numbers[largestIndex]);
                numbers[largestIndex] = numbers[size - 1];
 
                for (int i = 0; i < size; i++) {
                    numbers[i] = Reverse(numbers[i]);
                }

                if(debug) Console.WriteLine();

                size--;
            }

            Console.WriteLine(numbers[0]);
        }
    }
}

/*
http://smurf.mimuw.edu.pl/node/451

Zadanie WIR (Wirówka liczbowa), r. akad. 2008/2009
Dostępna pamięć: 32MB.
Profesor Makary postanowił zbudować odwracającą wirówkę liczbową. Program wirówki działa w następujący sposób.

Do wirówki wsypuje się liczby całkowite.
Jeśli w wirówce pozostała dokładnie jedna liczba, to wirówka kończy pracę.
Wirówka odwirowuje (czyli wyrzuca) największą z liczb, która w niej się znajduje (jeśli takich liczb jest kilka, to wyrzuca tylko jedną).
Wszystkie liczby, które pozostały w wirówce, są odwracane wspak (czyli 123123 staje się 321321, a 20202020 staje się 202202 ).
Wirówka skacze do drugiego kroku programu.
Napisz dla profesora symulator, który będzie przewidywał, jaka liczba pozostanie w wirówce po zakończeniu jej pracy.

Wejście

W pierwszym wierszu standardowego wejścia znajduje się jedna liczba całkowita nn ( 1≤n≤10000001≤n≤1000000 ), oznaczająca ilość liczb wrzuconych do wirówki. W następych nn wierszach znajdują się kolejne liczby całkowite aiai ( 0≤ai≤10000000000≤ai≤1000000000 ), które zostaną wrzucone do wirówki.

Wyjście

W jedynym wierszu standardowego wyjścia należy wypisać liczbę, która pozostanie w wirówce po zakończeniu pracy.

Przykład

Dla danych wejściowych:
3
10
21
13
poprawnym wynikiem jest:
1
*/
