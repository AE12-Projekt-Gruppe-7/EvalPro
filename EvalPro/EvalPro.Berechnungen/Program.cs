using System;
using EvalPro.Database.Repository;
using EvalPro.Database.Entities;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Berechnung
{
    class Berechnung
    {
        // ====== UMSCHALTUNG: Kommentiere eine der beiden Zeilen aus, um zwischen EvalPro.Debug und EvalPro.Database zu wechseln ======
        private static readonly bool USE_DEBUG = true;    // true = EvalPro.Debug, false = EvalPro.Database
        //private static readonly bool USE_DEBUG = false;

        private static readonly string DebugPath = @"..\..\..\..\EvalPro.Debug\jsons";

        static void Main()
        {
            System.IO.Directory.CreateDirectory("jsons");
            Console.WriteLine("Bewertung der Abschlussprüfung für Fachinformatiker Anwendungsentwicklung\n");
            Console.WriteLine(USE_DEBUG ? "[DEBUG-Modus aktiv]\n" : "[Datenbank-Modus aktiv]\n");

            // Lade Daten je nach Konfiguration
            List<Bewertung> bewertungen;
            List<Kriterium> kriteriums;

            if (USE_DEBUG)
            {
                kriteriums = LoadKriteriumsFromDebug();
                if (kriteriums == null)
                {
                    Console.WriteLine("Fehler beim Laden der Kriteriums!");
                    Thread.Sleep(3000);
                    return;
                }

                // Zeige verfügbare Prüflinge und lasse den Benutzer einen auswählen
                int prueflingId = SelectPrueflingFromDebug();
                bewertungen = LoadBewertungenFromDebug(prueflingId);
            }
            else
            {
                var bewertungRepo = new BewertungRepository();
                var kriteriumRepo = new KriteriumRepository();
                
                Console.WriteLine("=== Datenbank-Modus ===");
                Console.WriteLine("Verfügbare Prüflinge werden geladen...\n");
                
                // Hole alle Bewertungen um verfügbare Prüflinge zu ermitteln
                var alleBewertungen = bewertungRepo.GetAll().ToList();
                if (alleBewertungen == null || alleBewertungen.Count == 0)
                {
                    Console.WriteLine("Keine Bewertungen in der Datenbank gefunden!");
                    Thread.Sleep(3000);
                    return;
                }

                // Zeige verfügbare Prüflinge
                Console.WriteLine("=== Verfügbare Prüflinge ===");
                for (int i = 0; i < alleBewertungen.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Prüflings-ID: {alleBewertungen[i].PrueflingId} - Bewertungs-ID: {alleBewertungen[i].Id}");
                }

                // Benutzer wählt einen Prüfling
                Console.WriteLine("\nBitte wählen Sie einen Prüfling (Nummer eingeben):");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > alleBewertungen.Count)
                {
                    Console.WriteLine($"Ungültige Eingabe! Bitte Nummer zwischen 1 und {alleBewertungen.Count} eingeben:");
                }

                int prueflingId = alleBewertungen[choice - 1].PrueflingId;
                Console.WriteLine($"Prüfling ausgewählt: {prueflingId}\n");

                bewertungen = bewertungRepo.GetByPrueflingId(prueflingId).ToList();
                if (bewertungen == null || bewertungen.Count == 0)
                {
                    Console.WriteLine($"Keine Bewertungen für Prüflings-ID {prueflingId} gefunden!");
                    Thread.Sleep(3000);
                    return;
                }

                kriteriums = LoadKriteriumsFromDatabase(kriteriumRepo);
                if (kriteriums == null || kriteriums.Count == 0)
                {
                    Console.WriteLine("Keine Kriteriums in der Datenbank gefunden!");
                    Thread.Sleep(3000);
                    return;
                }

                Console.WriteLine($"Geladen: {bewertungen.Count} Bewertung(en), {kriteriums.Count} Kriterium(s)\n");
            }

            if (kriteriums == null || bewertungen == null || bewertungen.Count == 0)
            {
                Console.WriteLine("Fehler beim Laden der Daten!");
                Thread.Sleep(3000);
                return;
            }

            // Hole die Punkte für die einzelnen Kategorien
            double praesentation = GetLeistungspunkte(bewertungen, kriteriums, true, false, "Präsentation und Fachgespräch");
            Console.WriteLine();
            double projektDoku = GetLeistungspunkte(bewertungen, kriteriums, false, true, "Projektdokumentation");
            Console.WriteLine();
            double softwarePlanung = GetLeistungspunkte(bewertungen, kriteriums, false, false, "Planen eines Softwareprodukts");
            Console.WriteLine();
            double Programmierung = GetLeistungspunkte(bewertungen, kriteriums, false, false, "Anwendungsentwicklung");
            Console.WriteLine();

            // Gewichtung gemäß IHK Bayern
            double projektarbeit = (projektDoku + praesentation) / 2;
            double gesamtNote = projektarbeit * 0.5 + softwarePlanung * 0.25 + Programmierung * 0.25;

            Console.WriteLine($"\nGesamtnote: {gesamtNote:F2}");

            // Bestehensregeln
            bool bestanden = gesamtNote < 4.0 && gesamtNote > 0 &&
                            projektarbeit < 4.0 && projektarbeit > 0 &&
                            softwarePlanung < 4.0 && softwarePlanung > 0 &&
                            Programmierung < 4.0 && Programmierung > 0;

            Console.WriteLine(bestanden ? "Prüfung bestanden" : "Prüfung nicht bestanden");
            Thread.Sleep(3000);
        }

        static int SelectPrueflingFromDebug()
        {
            try
            {
                string filePath = Path.Combine(DebugPath, "pruefling.json");
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Warnung: {filePath} nicht gefunden! Nutze Standard-Prüfling ID 1");
                    return 1;
                }

                string json = File.ReadAllText(filePath);
                var prueflinge = JsonConvert.DeserializeObject<List<dynamic>>(json);

                if (prueflinge == null || prueflinge.Count == 0)
                {
                    Console.WriteLine("Keine Prüflinge gefunden!");
                    return 1;
                }

                Console.WriteLine("=== Verfügbare Prüflinge ===");
                for (int i = 0; i < prueflinge.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. ID: {prueflinge[i].Id} - Name: {prueflinge[i].Name}");
                }

                Console.WriteLine("\nBitte wählen Sie einen Prüfling (Nummer eingeben):");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > prueflinge.Count)
                {
                    Console.WriteLine($"Ungültige Eingabe! Bitte Nummer zwischen 1 und {prueflinge.Count} eingeben:");
                }

                int selectedId = (int)prueflinge[choice - 1].Id;
                Console.WriteLine($"Prüfling ausgewählt: {prueflinge[choice - 1].Name}\n");
                return selectedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Auswählen des Prüflings: {ex.Message}");
                return 1;
            }
        }

        static List<Bewertung> LoadBewertungenFromDebug(int prueflingId)
        {
            try
            {
                string filePath = Path.Combine(DebugPath, "model.json");
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Warnung: {filePath} nicht gefunden!");
                    return null;
                }
                string json = File.ReadAllText(filePath);
                var models = JsonConvert.DeserializeObject<List<dynamic>>(json);

                // Suche alle Bewertungen für den Prüfling (nicht nur eine!)
                var selectedModels = new List<dynamic>();
                foreach (var model in models)
                {
                    if ((int)model.PrueflingId == prueflingId)
                    {
                        selectedModels.Add(model);
                    }
                }

                if (selectedModels == null || selectedModels.Count == 0)
                {
                    Console.WriteLine($"Keine Bewertungen für Prüfling-ID {prueflingId} gefunden!");
                    return null;
                }

                Console.WriteLine($"Geladen: {selectedModels.Count} Bewertung(en) für Prüfling-ID {prueflingId}\n");

                // Konvertiere alle Modelle zu Bewertungen
                List<Bewertung> bewertungen = new List<Bewertung>();
                foreach (var selectedModel in selectedModels)
                {
                    // Konvertiere ItemIds zu List<int>
                    List<int> itemIds = new List<int>();
                    if (selectedModel.ItemIds != null)
                    {
                        foreach (var item in selectedModel.ItemIds)
                        {
                            itemIds.Add((int)item);
                        }
                    }

                    // Erstelle eine Bewertung aus den Debug-Daten
                    var bewertung = new Bewertung
                    {
                        Id = (int)selectedModel.Id,
                        PrueflingId = (int)selectedModel.PrueflingId,
                        Gesammtkommentar = "",
                        KriterienIds = itemIds,
                        istPraesi = (bool)selectedModel.istPraesi,
                        istDoku = (bool)selectedModel.istDoku
                    };
                    bewertungen.Add(bewertung);
                }

                return bewertungen;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Bewertungen: {ex.Message}");
                return null;
            }
        }

        static List<Kriterium> LoadKriteriumsFromDebug()
        {
            try
            {
                string filePath = Path.Combine(DebugPath, "kriterium.json");
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Warnung: {filePath} nicht gefunden!");
                    return null;
                }
                string json = File.ReadAllText(filePath);
                var kriteriums = JsonConvert.DeserializeObject<List<Kriterium>>(json);
                return kriteriums;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Kriteriums: {ex.Message}");
                return null;
            }
        }

        static List<Kriterium> LoadKriteriumsFromDatabase(KriteriumRepository kriteriumRepo)
        {
            try
            {
                var allKriteriums = kriteriumRepo.GetAll().ToList();
                return allKriteriums;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Kriteriums aus Datenbank: {ex.Message}");
                return new List<Kriterium>();
            }
        }

        static double GetLeistungspunkte(List<Bewertung> bewertungen, List<Kriterium> kriteriums, bool istPraesi, bool istDoku, string bezeichnung = null)
        {
            // Suche nach Bewertung mit Kategoriefilter
            var bewertung = bewertungen.FirstOrDefault(b => b.istPraesi == istPraesi && b.istDoku == istDoku);
            
            // Fallback: Wenn Präsentation/Doku nicht funktioniert, suche nach Bezeichnung
            if (bewertung == null && bezeichnung != null)
            {
                // Versuche über die Kriterienbezeichnungen zu filtern
                foreach (var bew in bewertungen)
                {
                    if (bew.KriterienIds != null && bew.KriterienIds.Count() > 0)
                    {
                        var firstKriterium = kriteriums?.FirstOrDefault(k => k.Id == bew.KriterienIds.First());
                        if (firstKriterium != null && firstKriterium.Bezeichnung.Contains(bezeichnung))
                        {
                            bewertung = bew;
                            break;
                        }
                    }
                }
            }
            
            // Fallback: Nimm die nächste verfügbare Bewertung
            if (bewertung == null && bewertungen.Count > 0)
            {
                bewertung = bewertungen[0];
            }
            
            if (bewertung == null || bewertung.KriterienIds == null || bewertung.KriterienIds.Count() == 0)
            {
                Console.WriteLine($"Keine Bewertung für {bezeichnung ?? "unbekannt"} gefunden!");
                return 0;
            }

            var punkte = 0;
            foreach (var kriteriumId in bewertung.KriterienIds)
            {
                var kriterium = kriteriums?.FirstOrDefault(k => k.Id == kriteriumId);
                if (kriterium != null)
                {
                    if (bezeichnung == null || kriterium.Bezeichnung.Contains(bezeichnung))
                    {
                        punkte += kriterium.Punkte;
                    }
                }
            }

            double note = punkte > 0 ? EvalPro.Berechnungen.IHKNotenschluessel.BerechneNote(punkte) : 0;
            Console.WriteLine($"Kategorie: {bezeichnung ?? "Unbekannt"} - Punkte: {punkte} - Note: {(note > 0 ? note.ToString("F1") : "Keine")}");
            return note;
        }
    }
}