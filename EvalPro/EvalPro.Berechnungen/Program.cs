using System;
using EvalPro.Database.Repository;
using EvalPro.Database.Entities;

namespace Berechnung
{
    class Berechnung
    {

        static void Main()
        {
            System.IO.Directory.CreateDirectory("jsons"); // Sicherstellen, dass das Verzeichnis existiert;
            Console.WriteLine("Bewertung der Abschlussprüfung für Fachinformatiker Anwendungsentwicklung\n");

            // Beispiel: PrueflingId = 1 (anpassen je nach gewünschtem Prüfling)
            int prueflingId = 1;
            var bewertungRepo = new BewertungRepository();
            var kriteriumRepo = new KriteriumRepository();

            // Hole alle Bewertungen für den Prüfling
            var bewertungen = bewertungRepo.GetByPrueflingId(prueflingId).ToList();

            // Hole die Punkte für die einzelnen Kategorien
            double praesentation = GetLeistungspunkte(bewertungen, kriteriumRepo, true, false);
            Console.WriteLine();
            double projektDoku = GetLeistungspunkte(bewertungen, kriteriumRepo, false, true);
            Console.WriteLine();
            double softwarePlanung = GetLeistungspunkte(bewertungen, kriteriumRepo, false, false, "Planen eines Softwareprodukts");
            Console.WriteLine();
            double Programmierung = GetLeistungspunkte(bewertungen, kriteriumRepo, false, false, "Anwendungsentwicklung");
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

        static double GetLeistungspunkte(List<Bewertung> bewertungen, KriteriumRepository kriteriumRepo, bool istPraesi, bool istDoku, string bezeichnung = null)
        {
            // Finde die Bewertung für die Kategorie
            var bewertung = bewertungen.FirstOrDefault(b => b.istPraesi == istPraesi && b.istDoku == istDoku);
            if (bewertung == null) return 0;
            var punkte = 0;
            foreach (var kriteriumId in bewertung.KriterienIds)
            {
                var kriterium = kriteriumRepo.Repo.Serializer.Deserialize<List<Kriterium>>(kriteriumRepo.Repo.Reader)?.FirstOrDefault(k => k.Id == kriteriumId);
                if (kriterium != null)
                {
                    if (bezeichnung == null || kriterium.Bezeichnung == bezeichnung)
                        punkte += kriterium.Punkte;
                }
            }
            double note = EvalPro.Berechnungen.IHKNotenschluessel.BerechneNote(punkte);
            Console.WriteLine($"Note für {punkte} Punkte: {note:F1}");
            return note;
        }
    }
}