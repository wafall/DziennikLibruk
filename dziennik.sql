-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 27 Kwi 2023, 08:58
-- Wersja serwera: 10.4.27-MariaDB
-- Wersja PHP: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `dziennik`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ocena`
--

CREATE TABLE `ocena` (
  `ID` int(11) NOT NULL,
  `uczen_id` int(11) NOT NULL,
  `przedmiot` text NOT NULL,
  `ocena` text NOT NULL,
  `kategoria` text NOT NULL,
  `nauczyciel` text NOT NULL,
  `data_uzyskania` text NOT NULL,
  `do_sredniej` tinyint(1) NOT NULL,
  `waga` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `ocena`
--

INSERT INTO `ocena` (`ID`, `uczen_id`, `przedmiot`, `ocena`, `kategoria`, `nauczyciel`, `data_uzyskania`, `do_sredniej`, `waga`) VALUES
(1, 1, 'gera', '1+', 'sprawdzian', 'pani gera', '19:23:00 26.04.2023', 1, 5),
(2, 1, 'biola', '1', 'sprawdzian', 'Anna Maria Wesolowska', '2023-04-26 23:13:25', 0, 5),
(3, 1, 'matematyka', '5', 'sprawdzian', 'Jan Kowalski', '2023-04-25', 1, 2),
(4, 7, 'język polski', '4', 'kartkówka', 'Anna Nowak', '2023-04-26', 1, 1),
(5, 7, 'historia', '3', 'sprawdzian', 'Adam Wiśniewski', '2023-04-24', 1, 1),
(6, 1, 'fizyka', '5', 'odpowiedź', 'Anna Kaczmarek', '2023-04-23', 1, 3),
(7, 7, 'chemia', '2', 'kartkówka', 'Marek Nowicki', '2023-04-22', 1, 1),
(8, 1, 'język angielski', '4', 'sprawdzian', 'Jan Kowalski', '2023-04-21', 1, 2),
(9, 7, 'historia', '4', 'kartkówka', 'Adam Wiśniewski', '2023-04-20', 1, 1),
(10, 1, 'matematyka', '3', 'sprawdzian', 'Anna Nowak', '2023-04-19', 1, 1),
(11, 1, 'język angielski', '5', 'odpowiedź', 'Marek Nowicki', '2023-04-18', 1, 3),
(12, 7, 'geografia', '2', 'kartkówka', 'Jan Kowalski', '2023-04-17', 1, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rodzic`
--

CREATE TABLE `rodzic` (
  `ID` int(11) NOT NULL,
  `uzytkownik_id` int(11) NOT NULL,
  `uczen_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `rodzic`
--

INSERT INTO `rodzic` (`ID`, `uzytkownik_id`, `uczen_id`) VALUES
(1, 2, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `uwaga`
--

CREATE TABLE `uwaga` (
  `ID` int(11) NOT NULL,
  `uczen_id` int(11) NOT NULL,
  `tresc` text NOT NULL,
  `nauczyciel` text NOT NULL,
  `data_uzyskania` text NOT NULL,
  `rodzaj_uwagi` tinyint(1) NOT NULL,
  `kategoria` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `uwaga`
--

INSERT INTO `uwaga` (`ID`, `uczen_id`, `tresc`, `nauczyciel`, `data_uzyskania`, `rodzaj_uwagi`, `kategoria`) VALUES
(1, 1, 'chujem jest', 'Anna Maria Wesolowska', '2023-04-26 23:18:50', 0, '-'),
(2, 1, 'pomoc kolezance ', 'Anna Maria Wesolowska', '2023-04-27 08:26:30', 0, 'zachowanie');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `uzytkownik`
--

CREATE TABLE `uzytkownik` (
  `ID` int(11) NOT NULL,
  `email` text NOT NULL,
  `haslo` text NOT NULL,
  `typ_uzytkownika` text NOT NULL,
  `imie` text NOT NULL,
  `nazwisko` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `uzytkownik`
--

INSERT INTO `uzytkownik` (`ID`, `email`, `haslo`, `typ_uzytkownika`, `imie`, `nazwisko`) VALUES
(1, 'bielik@gmail.com', 'haslo', 'uczen', 'bie', 'lik'),
(2, 'rodzic@gmail.com', 'haslo', 'rodzic', 'Marek', 'Mostowiak'),
(3, 'nauczyciel@gmail.com', 'haslo', 'nauczyciel', 'Anna', 'Maria Wesolowska'),
(4, 'jan.kowalski@szkola.pl', 'haslo123', 'nauczyciel', 'Jan', 'Kowalski'),
(5, 'anna.nowak@szkola.pl', 'haslo456', 'nauczyciel', 'Anna', 'Nowak'),
(6, 'adam.wisniewski@szkola.pl', 'haslo789', 'nauczyciel', 'Adam', 'Wiśniewski'),
(7, 'marek.nowicki@szkola.pl', 'hasloabc', 'uczen', 'Marek', 'Nowicki'),
(8, 'katarzyna.kowalczyk@szkola.pl', 'hasloxyz', 'rodzic', 'Katarzyna', 'Kowalczyk');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `ocena`
--
ALTER TABLE `ocena`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `rodzic`
--
ALTER TABLE `rodzic`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `uwaga`
--
ALTER TABLE `uwaga`
  ADD PRIMARY KEY (`ID`);

--
-- Indeksy dla tabeli `uzytkownik`
--
ALTER TABLE `uzytkownik`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `ocena`
--
ALTER TABLE `ocena`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT dla tabeli `rodzic`
--
ALTER TABLE `rodzic`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT dla tabeli `uwaga`
--
ALTER TABLE `uwaga`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `uzytkownik`
--
ALTER TABLE `uzytkownik`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
