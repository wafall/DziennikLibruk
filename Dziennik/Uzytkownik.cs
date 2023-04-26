using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    public class Uzytkownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public Uzytkownik(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }
    }
}
