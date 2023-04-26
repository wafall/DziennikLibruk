using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    public class Ocena
    {
        public string przedmiot { get; set; }
        public string ocena { get; set; }
        public string kategoria { get; set; }
        public string nauczyciel { get; set; }
        public string data_uzyskania { get; set; }
        public bool do_sredniej { get; set; }
        public int waga { get; set; }

        public Ocena(string przedmiot, string ocena, string kategoria, string nauczyciel, string data_uzyskania, bool do_sredniej, int waga)
        {
            this.przedmiot = przedmiot;
            this.ocena = ocena;
            this.kategoria = kategoria;
            this.nauczyciel = nauczyciel;
            this.data_uzyskania = data_uzyskania;
            this.do_sredniej = do_sredniej;
            this.waga = waga;
        }
    }

}