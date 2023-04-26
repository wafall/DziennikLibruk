using MySql.Data.MySqlClient;

namespace Dziennik
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Witaj w aplikacji Libruk!");

            while (true)
            {
                Console.WriteLine("Wybierz panel:");
                Console.WriteLine("1. Panel ucznia");
                Console.WriteLine("2. Panel rodzica");
                Console.WriteLine("3. Panel nauczyciela");
                Console.Write("Wybierz numer panelu (lub wpisz '0' aby wyjść z aplikacji): ");

                string userInput = Console.ReadLine();

                if (userInput == "0") break;

                switch (userInput)
                {
                    case "1":
                        if (!PanelUcznia()) continue;
                        break;
                    case "2":
                        if (!PanelRodzica()) continue;
                        break;
                    case "3":
                        if (!PanelNauczyciela()) continue;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór panelu.");
                        break;
                }
            }

        }

        static bool PanelUcznia()
        {
            Uczen uczen;
            List<Ocena> oceny = new List<Ocena>();
            List<Uwaga> uwagi = new List<Uwaga>();
            int uczen_id;
            string imie, nazwisko, query;

            //logowanie
            Console.WriteLine("Witaj w panelu ucznia!");
            Console.Write("Podaj adres e-mail: ");
            string email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();
            query = $"SELECT ID, imie, nazwisko FROM uzytkownik WHERE email='{email}' AND haslo='{password}' AND typ_uzytkownika = 'uczen'";

            using MySqlCommand uzytkownik_command = new MySqlCommand(query, DBConnection.Connection);
            using MySqlDataReader uzytkownik_reader = uzytkownik_command.ExecuteReader();

            if (uzytkownik_reader.Read()) //sprawdza czy jest taki rekord w tabeli
            {
                uczen_id = uzytkownik_reader.GetInt32("ID");
                imie = uzytkownik_reader.GetString("imie");
                nazwisko = uzytkownik_reader.GetString("nazwisko");
                uzytkownik_reader.Close();
                uzytkownik_command.Dispose();

                // zaladowywanie ocen
                query = $"SELECT przedmiot, ocena, kategoria, nauczyciel, data_uzyskania, do_sredniej, waga FROM ocena WHERE uczen_id = {uczen_id}";
                using MySqlCommand oceny_command = new MySqlCommand(query, DBConnection.Connection);
                using MySqlDataReader oceny_reader = oceny_command.ExecuteReader();

                while (oceny_reader.Read())
                {
                    string przedmiot = oceny_reader.GetString("przedmiot");
                    string ocena_string = oceny_reader.GetString("ocena");
                    string kategoria = oceny_reader.GetString("kategoria");
                    string nauczyciel = oceny_reader.GetString("nauczyciel");
                    string data_uzyskania = oceny_reader.GetString("data_uzyskania");
                    bool do_sredniej = oceny_reader.GetBoolean("do_sredniej");
                    int waga = oceny_reader.GetInt32("waga");
                    Ocena ocena = new Ocena(przedmiot, ocena_string, kategoria, nauczyciel, data_uzyskania, do_sredniej, waga);
                    oceny.Add(ocena);
                }
                oceny_reader.Close();

                //zaladowywanie uwag
                query = $"SELECT tresc, nauczyciel, data_uzyskania, rodzaj_uwagi, kategoria FROM uwaga WHERE uczen_id = {uczen_id}";
                using MySqlCommand uwagi_command = new MySqlCommand(query, DBConnection.Connection);
                using MySqlDataReader uwagi_reader = uwagi_command.ExecuteReader();

                while (uwagi_reader.Read())
                {
                    string tresc = uwagi_reader.GetString("tresc");
                    string nauczyciel = uwagi_reader.GetString("nauczyciel");
                    string data_uzyskania = uwagi_reader.GetString("data_uzyskania");
                    bool rodzaj_uwagi = uwagi_reader.GetBoolean("rodzaj_uwagi");
                    string kategoria = uwagi_reader.GetString("kategoria");
                    Uwaga uwaga = new Uwaga(tresc, data_uzyskania, nauczyciel, rodzaj_uwagi, kategoria);
                    uwagi.Add(uwaga);
                }
                uwagi_reader.Close();
                uwagi_command.Dispose();

                uczen = new Uczen(imie, nazwisko, oceny, uwagi); //zaladowywanie wszystkich danych do obiektu uczen

                while (true)
                {
                    Console.WriteLine($"Uczen: {uczen.Imie} {uczen.Nazwisko}");
                    Console.WriteLine("Co chcesz wyświetlić?");
                    Console.WriteLine("1. Oceny");
                    Console.WriteLine("2. Uwagi");
                    Console.WriteLine("0. Wyloguj się");

                    string userInput = Console.ReadLine();

                    if (userInput == "0") return false;

                    switch (userInput)
                    {
                        case "1":
                            //TODO dodac sortowanie po przedmiocie
                            uczen.WyswietlOceny();
                            break;
                        case "2":
                            uczen.WyswietlUwagi();
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór panelu.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Błędny email lub hasło");
                return false;
            }
        }

        static bool PanelRodzica()
        {
            Rodzic rodzic;
            List<Ocena> oceny = new List<Ocena>();
            List<Uwaga> uwagi = new List<Uwaga>();
            int rodzic_id, uczen_id;
            string imie, nazwisko, imie_uczenia, nazwisko_ucznia, query;

            //logowanie
            Console.WriteLine("Witaj w panelu rodzica!");
            Console.Write("Podaj adres e-mail: ");
            string email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();
            query = $"SELECT ID, imie, nazwisko FROM uzytkownik WHERE email='{email}' AND haslo='{password}' AND typ_uzytkownika = 'rodzic'";
            using MySqlCommand uzytkownik_command = new MySqlCommand(query, DBConnection.Connection);
            using MySqlDataReader uzytkownik_reader = uzytkownik_command.ExecuteReader();

            if (uzytkownik_reader.Read()) //sprawdza czy jest taki rekord w tabeli
            {
                rodzic_id = uzytkownik_reader.GetInt32("ID");
                imie = uzytkownik_reader.GetString("imie");
                nazwisko = uzytkownik_reader.GetString("nazwisko");
                uzytkownik_reader.Close();
                uzytkownik_command.Dispose();

                query = $"SELECT uczen_id FROM rodzic WHERE uzytkownik_id = {rodzic_id}";
                using MySqlCommand uczen_command = new MySqlCommand(query, DBConnection.Connection);
                using MySqlDataReader uczen_reader = uczen_command.ExecuteReader();
                if (uczen_reader.Read()) // czy jest uczen przypisany do tego rodzica
                {
                    uczen_id = uczen_reader.GetInt32("uczen_id");
                    uczen_reader.Close();
                    uczen_command.Dispose();
                    query = $"SELECT imie, nazwisko FROM uzytkownik WHERE ID = {uczen_id}";
                    using MySqlCommand uczen2_command = new MySqlCommand(query, DBConnection.Connection);
                    using MySqlDataReader uczen2_reader = uczen2_command.ExecuteReader();
                    if (uczen2_reader.Read())
                    {
                        imie_uczenia = uczen2_reader.GetString("imie");
                        nazwisko_ucznia = uczen2_reader.GetString("nazwisko");
                        uczen2_reader.Close();
                        uczen2_command.Dispose();
                        // zaladowywanie ocen
                        query = $"SELECT przedmiot, ocena, kategoria, nauczyciel, data_uzyskania, do_sredniej, waga FROM ocena WHERE uczen_id = {uczen_id}";
                        using MySqlCommand oceny_command = new MySqlCommand(query, DBConnection.Connection);
                        using MySqlDataReader oceny_reader = oceny_command.ExecuteReader();

                        while (oceny_reader.Read())
                        {
                            string przedmiot = oceny_reader.GetString("przedmiot");
                            string ocena_string = oceny_reader.GetString("ocena");
                            string kategoria = oceny_reader.GetString("kategoria");
                            string nauczyciel = oceny_reader.GetString("nauczyciel");
                            string data_uzyskania = oceny_reader.GetString("data_uzyskania");
                            bool do_sredniej = oceny_reader.GetBoolean("do_sredniej");
                            int waga = oceny_reader.GetInt32("waga");
                            Ocena ocena = new Ocena(przedmiot, ocena_string, kategoria, nauczyciel, data_uzyskania, do_sredniej, waga);
                            oceny.Add(ocena);
                        }
                        oceny_reader.Close();
                        oceny_command.Dispose();

                        //zaladowywanie uwag
                        query = $"SELECT tresc, nauczyciel, data_uzyskania, rodzaj_uwagi, kategoria FROM uwaga WHERE uczen_id = {uczen_id}";
                        using MySqlCommand uwagi_command = new MySqlCommand(query, DBConnection.Connection);
                        using MySqlDataReader uwagi_reader = uwagi_command.ExecuteReader();

                        while (uwagi_reader.Read())
                        {
                            string tresc = uwagi_reader.GetString("tresc");
                            string nauczyciel = uwagi_reader.GetString("nauczyciel");
                            string data_uzyskania = uwagi_reader.GetString("data_uzyskania");
                            bool rodzaj_uwagi = uwagi_reader.GetBoolean("rodzaj_uwagi");
                            string kategoria = uwagi_reader.GetString("kategoria");
                            Uwaga uwaga = new Uwaga(tresc, data_uzyskania, nauczyciel, rodzaj_uwagi, kategoria);
                            uwagi.Add(uwaga);
                        }
                        uwagi_reader.Close();
                        uwagi_command.Dispose();

                        rodzic = new Rodzic(imie, nazwisko, new Uczen(imie_uczenia, nazwisko_ucznia, oceny, uwagi)); //new Uczen(imie, nazwisko, oceny, uwagi); //zaladowywanie wszystkich danych do obiektu uczen

                        while (true)
                        {
                            Console.WriteLine($"Uczen: {rodzic.Uczen.Imie} {rodzic.Uczen.Nazwisko}");
                            Console.WriteLine($"Rodzic: {rodzic.Imie} {rodzic.Nazwisko}");
                            Console.WriteLine("Co chcesz wyświetlić?");
                            Console.WriteLine("1. Oceny");
                            Console.WriteLine("2. Uwagi");
                            Console.WriteLine("0. Wyloguj się");

                            string userInput = Console.ReadLine();

                            if (userInput == "0") return false;

                            switch (userInput)
                            {
                                case "1":
                                    rodzic.Uczen.WyswietlOceny();
                                    break;
                                case "2":
                                    rodzic.Uczen.WyswietlUwagi();
                                    break;
                                default:
                                    Console.WriteLine("Nieprawidłowy wybór panelu.");
                                    break;
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine("Blad: nie mozna bylo znalezc ucznia");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Błędny email lub hasło");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Ten rodzic nie ma przypisanego ucznia");
                return false;
            }


        }

        static bool PanelNauczyciela()
        {
            Nauczyciel nauczyciel;
            int id_ucznia;
            string imie, nazwisko, imieucznia, nazwiskoucznia, query;

            Console.WriteLine("Witaj w panelu nauczyciela!");
            Console.Write("Podaj adres e-mail: ");
            string email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            query = $"SELECT ID, imie, nazwisko FROM uzytkownik WHERE email='{email}' AND haslo='{password}' AND typ_uzytkownika = 'nauczyciel'";
            using MySqlCommand nauczyciel_command = new MySqlCommand(query, DBConnection.Connection);
            using MySqlDataReader nauczyciel_reader = nauczyciel_command.ExecuteReader();

            if(nauczyciel_reader.Read())
            {
                imie = nauczyciel_reader.GetString("imie");
                nazwisko = nauczyciel_reader.GetString("nazwisko");
                nauczyciel = new Nauczyciel(imie, nazwisko);
                nauczyciel_reader.Close();
                nauczyciel_command.Dispose();

                while(true)
                {
                    query = $"SELECT ID, imie, nazwisko FROM uzytkownik WHERE typ_uzytkownika = 'uczen'";
                    using MySqlCommand uczniowie_command = new MySqlCommand(query, DBConnection.Connection);
                    using MySqlDataReader uczniowie_reader = uczniowie_command.ExecuteReader();
                    while (uczniowie_reader.Read())
                    {
                        id_ucznia = uczniowie_reader.GetInt32("ID");
                        imieucznia = uczniowie_reader.GetString("imie");
                        nazwiskoucznia = uczniowie_reader.GetString("nazwisko");

                        Console.WriteLine($"{id_ucznia}. {imieucznia} {nazwiskoucznia}");
                    }
                    uczniowie_reader.Close();
                    uczniowie_command.Dispose();

                    Console.Write("ID ucznia: ");

                    string userInput2 = Console.ReadLine();

                    Console.WriteLine($"Nauczyciel: {nauczyciel.Imie} {nauczyciel.Nazwisko}");
                    Console.WriteLine("1. Dodaj ocenę");
                    Console.WriteLine("2. Dodaj uwagę");
                    Console.WriteLine("0. Wyloguj");

                    string userInput1 = Console.ReadLine();

                    if (userInput1 == "0") return false;

                    switch (userInput1)
                    {
                        case "1":
                            nauczyciel.DodajOcene(int.Parse(userInput2));
                            break;
                        case "2":
                            nauczyciel.DodajUwage(int.Parse(userInput2));
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór panelu.");
                            break;
                    }

                }
                return true;
            }
            else
            {
                Console.WriteLine("Zły email lub haslo");
                return false;
            }
        }
    }
}