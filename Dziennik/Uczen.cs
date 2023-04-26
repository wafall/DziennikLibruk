using Dziennik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    internal class Uczen : Uzytkownik
    {
        public List<Ocena> Oceny { get; set; }
        public List<Uwaga> Uwagi { get; set; }

        public Uczen(string imie, string nazwisko, List<Ocena> oceny, List<Uwaga> uwagi) : base(imie, nazwisko)
        {
            Oceny = oceny;
            Uwagi = uwagi;
        }

        public void WyswietlOceny()
        {
            string przedmiot = "";
            foreach (Ocena o in Oceny.OrderBy(x => x.przedmiot))
            {
                if (o.przedmiot != przedmiot)
                {
                    przedmiot = o.przedmiot;
                    Console.WriteLine(przedmiot);
                }
                string sredniej = o.do_sredniej ? "Tak" : "Nie";
                Console.WriteLine($"Ocena: {o.ocena}\nKategoria: {o.kategoria}\nNauczyciel: {o.nauczyciel}\nData uzyskania: {o.data_uzyskania}\nDo średniej: {sredniej}\nWaga: {o.waga}");
            }
        }

        public void WyswietlUwagi()
        {
            foreach (Uwaga u in Uwagi)
            {
                string rodzaj = u.RodzajUwagi ? "pozytywna" : "negatywna";
                Console.WriteLine($"Uwaga\n{u.Tresc}\nNauczyciel: {u.Nauczyciel}\nData uzyskania: {u.DataUzyskania}\nRodzaj uwagi: {rodzaj}\nKategoria: {u.Kategoria}");
            }
        }
    }
}
