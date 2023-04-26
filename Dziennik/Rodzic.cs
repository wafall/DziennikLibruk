using Dziennik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    internal class Rodzic : Uzytkownik
    {
        public Uczen Uczen { get; set; }
        public Rodzic(string imie, string nazwisko, Uczen uczen) : base(imie, nazwisko)
        {
            Uczen = uczen;
        }
    }
}
