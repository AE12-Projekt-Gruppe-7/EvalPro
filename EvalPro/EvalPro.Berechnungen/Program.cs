using System;

namespace Berechnung
{
    class Berechnung
    {

        static void Main()
        {
            Console.WriteLine("Bewertung der Abschlussprüfung für Fachinformatiker Anwendungsentwicklung\n");

            // Eingabe der Noten für die Prüfungsbereiche
            double praesentation = EingabeNote("Präsentation und Fachgespräch");
            double softwarePlanung = EingabeNote("Planen eines Softwareprodukts");
            double Programmierung = EingabeNote("Anwendungsentwicklung");
            double projektDoku = EingabeNote("Projektdokumentation");

            // Gewichtung gemäß IHK Bayern
            double projektarbeit = (projektDoku + praesentation) / 2;
            double gesamtNote = projektarbeit * 0.5 + softwarePlanung * 0.25 + Programmierung * 0.25;

            Console.WriteLine($"\nGesamtnote: {gesamtNote:F2}");

            // Bestehensregeln
            bool bestanden = gesamtNote < 4.0 &&
                            projektarbeit < 4.0 &&
                            softwarePlanung < 4.0 &&
                            Programmierung < 4.0;

            Console.WriteLine(bestanden ? "Prüfung bestanden" : "Prüfung nicht bestanden");
            Thread.Sleep(3000);
        }

        static double EingabeNote(string bereich)
        {
            double note;
            do
            {
                Console.Write($"Note für {bereich} (1.0 - 6.0): ");
            }
            while (!double.TryParse(Console.ReadLine(), out note) || note < 1.0 || note > 6.0);
            return note;
        }
    }
}