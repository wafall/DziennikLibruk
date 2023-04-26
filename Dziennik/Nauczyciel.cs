using Dziennik;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    public class Nauczyciel : Uzytkownik
    {
        public Nauczyciel(string imie, string nazwisko) : base(imie, nazwisko)
        {
        }

        public void DodajOcene(int id_ucznia)
        {
            Ocena ocena;
            string query;

            Console.Write("Podaj ocenę: ");
            string ocena_str = Console.ReadLine();

            Console.Write("Podaj przedmiot: ");
            string przedmiot = Console.ReadLine();

            Console.Write("Podaj kategorie: ");
            string kategoria = Console.ReadLine();

            Console.Write("Podaj wage: ");
            int waga;
            while (!int.TryParse(Console.ReadLine(), out waga))
            {
                Console.WriteLine("Nieprawidłowe dane wejściowe. Podaj poprawną wagę:");
            }

            Console.Write("Czy dodać do średniej? (true/false): ");
            bool do_sredniej;
            while (!bool.TryParse(Console.ReadLine(), out do_sredniej))
            {
                Console.WriteLine("Nieprawidłowe dane wejściowe. Podaj true lub false:");
            }

            string nauczyciel = $"{Imie} {Nazwisko}";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            ocena = new Ocena(przedmiot, ocena_str, kategoria, nauczyciel, currentDate, do_sredniej, waga);

            query = $"INSERT INTO ocena (uczen_id, przedmiot, ocena, kategoria, nauczyciel, data_uzyskania, do_sredniej, waga) " +
                $"VALUES ('{id_ucznia}', '{ocena.przedmiot}', '{ocena.ocena}', '{ocena.kategoria}', '{ocena.nauczyciel}', '{ocena.data_uzyskania}', '{ocena.do_sredniej}', '{ocena.waga}')";
            using MySqlCommand ocena_command = new MySqlCommand(query, DBConnection.Connection);
            ocena_command.ExecuteNonQuery();
            Console.WriteLine("Dodano ocenę");

            ocena_command.Dispose();
        }


        public void DodajUwage(int id_ucznia)
        {
            Uwaga uwaga;
            string query;

            Console.Write("Podaj treść uwagi: ");
            string tresc = Console.ReadLine();

            Console.Write("Podaj kategorię: ");
            string kategoria = Console.ReadLine();

            Console.Write("Czy jest pozytywna czy negatywna? (true/false): ");
            bool rodzaj_uwagi;
            while (!bool.TryParse(Console.ReadLine(), out rodzaj_uwagi))
            {
                Console.WriteLine("Nieprawidłowe dane wejściowe. Podaj true lub false:");
            }

            string nauczyciel = $"{Imie} {Nazwisko}";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            uwaga = new Uwaga(tresc, currentDate, nauczyciel, rodzaj_uwagi, kategoria);

            query = $"INSERT INTO uwaga (uczen_id, tresc, data_uzyskania, nauczyciel, rodzaj_uwagi, kategoria) " +
                $"VALUES ('{id_ucznia}', '{uwaga.Tresc}', '{uwaga.DataUzyskania}', '{uwaga.Nauczyciel}', '{uwaga.RodzajUwagi}', '{uwaga.Kategoria}')";
            using MySqlCommand uwaga_command = new MySqlCommand(query, DBConnection.Connection);
            uwaga_command.ExecuteNonQuery();
            Console.WriteLine("Dodano uwagę");

            uwaga_command.Dispose();

        }

    }
}
