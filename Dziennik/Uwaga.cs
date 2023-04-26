using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    public class Uwaga
    {
        public string Tresc { get; set; }
        public string DataUzyskania { get; set; }
        public string Nauczyciel { get; set; }
        public bool RodzajUwagi { get; set; }
        public string Kategoria { get; set; }

        public Uwaga(string tresc, string dataUzyskania, string nauczyciel, bool rodzajUwagi, string kategoria)
        {
            Tresc = tresc;
            DataUzyskania = dataUzyskania;
            Nauczyciel = nauczyciel;
            RodzajUwagi = rodzajUwagi;
            Kategoria = kategoria;
        }
    }

}
