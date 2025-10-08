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
            Console.WriteLine();
            double softwarePlanung = EingabeNote("Planen eines Softwareprodukts");
            Console.WriteLine();
            double Programmierung = EingabeNote("Anwendungsentwicklung");
            Console.WriteLine();
            double projektDoku = EingabeNote("Projektdokumentation");
            Console.WriteLine();

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
            int punkte;
            do
            {
                Console.Write($"Punktzahl für {bereich} (0 - 100): ");
            }
            while (!int.TryParse(Console.ReadLine(), out punkte) || punkte < 0 || punkte > 100);
            double note = EvalPro.Berechnungen.IHKNotenschluessel.BerechneNote(punkte);
            Console.WriteLine($"Note für {punkte} Punkte: {note:F1}");
            return note;
        }
    }
}