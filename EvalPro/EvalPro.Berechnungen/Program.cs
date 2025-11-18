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
            int prueflingId = 1; // wird in den jeweiligen Modi gesetzt

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
                prueflingId = SelectPrueflingFromDebug();
                bewertungen = LoadBewertungenFromDebug(prueflingId);
            }
            else
            {
                var bewertungRepo = new BewertungRepository();
                var kriteriumRepo = new KriteriumRepository();
                
                Console.WriteLine("=== Datenbank-Modus ===");
                
                // Diagnose durchführen
                DiagnoseDatabaseConnection(bewertungRepo, kriteriumRepo);
                
                Console.WriteLine("Verfügbare Prüflinge werden geladen...\n");
                
                // Hole alle Bewertungen um verfügbare Prüflinge zu ermitteln
                var alleBewertungen = bewertungRepo.GetAll().ToList();
                if (alleBewertungen == null || alleBewertungen.Count == 0)
                {
                    Console.WriteLine("Keine Bewertungen in der Datenbank gefunden!");
                    Thread.Sleep(3000);
                    return;
                }

                // Gruppiere nach Prüfling-ID um eindeutige Prüflinge zu zeigen
                var uniquePrueflinge = alleBewertungen.GroupBy(b => b.PrueflingId).Select(g => g.First()).ToList();
                
                Console.WriteLine("=== Verfügbare Prüflinge ===");
                for (int i = 0; i < uniquePrueflinge.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Prüflings-ID: {uniquePrueflinge[i].PrueflingId}");
                }

                // Benutzer wählt einen Prüfling
                Console.WriteLine("\nBitte wählen Sie einen Prüfling (Nummer eingeben):");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > uniquePrueflinge.Count)
                {
                    Console.WriteLine($"Ungültige Eingabe! Bitte Nummer zwischen 1 und {uniquePrueflinge.Count} eingeben:");
                }

                prueflingId = uniquePrueflinge[choice - 1].PrueflingId;
                Console.WriteLine($"\n[Prüfling-ID: {prueflingId} ausgewählt]\n");

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

                Console.WriteLine($"[Geladen: {bewertungen.Count} Bewertung(en) für Prüfling-ID {prueflingId}, {kriteriums.Count} Kriterium(s) insgesamt]\n");
            }

            if (kriteriums == null || bewertungen == null || bewertungen.Count == 0)
            {
                Console.WriteLine("Fehler beim Laden der Daten!");
                Thread.Sleep(3000);
                return;
            }

            // Hole die Punkte für die einzelnen Kategorien (jeweils Note und Rohpunkte)
            var (praesentation, praesentationPunkte) = GetLeistungspunkte(bewertungen, kriteriums, true, false, "Präsentation und Fachgespräch");
            Console.WriteLine();
            var (projektDoku, projektDokuPunkte) = GetLeistungspunkte(bewertungen, kriteriums, false, true, "Projektdokumentation");
            Console.WriteLine();
            var (softwarePlanung, softwarePlanungPunkte) = GetLeistungspunkte(bewertungen, kriteriums, false, false, "Planen eines Softwareprodukts");
            Console.WriteLine();
            var (Programmierung, ProgrammierungPunkte) = GetLeistungspunkte(bewertungen, kriteriums, false, false, "Anwendungsentwicklung");
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

            // Speichere berechnete Noten im Debug-Ordner (nur im Debug-Modus)
            try
            {
                var notes = new List<object>
                {
                    new { Kategorie = "Präsentation und Fachgespräch", Punkte = praesentationPunkte, Note = praesentation },
                    new { Kategorie = "Projektdokumentation", Punkte = projektDokuPunkte, Note = projektDoku },
                    new { Kategorie = "Planen eines Softwareprodukts", Punkte = softwarePlanungPunkte, Note = softwarePlanung },
                    new { Kategorie = "Anwendungsentwicklung", Punkte = ProgrammierungPunkte, Note = Programmierung },
                    new { Kategorie = "Projektarbeit (Doku+Präsi)", Punkte = (projektDokuPunkte + praesentationPunkte)/2, Note = projektarbeit }
                };

                string outPath = Path.Combine(DebugPath, $"notes_pruefling_{prueflingId}.json");
                File.WriteAllText(outPath, JsonConvert.SerializeObject(notes, Formatting.Indented));
                Console.WriteLine($"Noten in {outPath} gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Noten: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
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
                var allKriteriums = kriteriumRepo.GetAll();
                if (allKriteriums == null)
                {
                    Console.WriteLine("GetAll() hat null zurückgegeben!");
                    return new List<Kriterium>();
                }

                var kriteriumsList = allKriteriums.ToList();
                if (kriteriumsList.Count == 0)
                {
                    Console.WriteLine("Keine Kriteriums in der Datenbank vorhanden!");
                    return new List<Kriterium>();
                }

                // Debug-Info: Zeige ein Beispiel
                if (kriteriumsList.Count > 0)
                {
                    var first = kriteriumsList[0];
                    Console.WriteLine($"[Debug: Erstes Kriterium - ID: {first.Id}, Bezeichnung: {first.Bezeichnung}, Punkte: {first.Punkte}]");
                }

                return kriteriumsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Kriteriums aus Datenbank: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new List<Kriterium>();
            }
        }

        static void DiagnoseDatabaseConnection(BewertungRepository bewertungRepo, KriteriumRepository kriteriumRepo)
        {
            Console.WriteLine("\n=== Datenbankverbindungs-Diagnose ===");
            
            // Teste Bewertungen
            try
            {
                var allBewertungen = bewertungRepo.GetAll().ToList();
                Console.WriteLine($"✓ Bewertungen: {allBewertungen.Count} Einträge gefunden");
                if (allBewertungen.Count > 0)
                {
                    var grouped = allBewertungen.GroupBy(b => b.PrueflingId);
                    Console.WriteLine($"  - {grouped.Count()} unterschiedliche Prüflinge");
                    foreach (var group in grouped.Take(3))
                    {
                        Console.WriteLine($"    - Prüfling {group.Key}: {group.Count()} Bewertung(en)");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Bewertungen-Fehler: {ex.Message}");
            }

            // Teste Kriteriums
            try
            {
                var allKriteriums = kriteriumRepo.GetAll().ToList();
                Console.WriteLine($"✓ Kriteriums: {allKriteriums.Count} Einträge gefunden");
                if (allKriteriums.Count > 0)
                {
                    Console.WriteLine($"  - Beispiele:");
                    foreach (var k in allKriteriums.Take(3))
                    {
                        Console.WriteLine($"    - ID {k.Id}: {k.Bezeichnung} ({k.Punkte} Punkte)");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Kriteriums-Fehler: {ex.Message}");
            }

            Console.WriteLine("=================================\n");
        }

        static (double note, int punkte) GetLeistungspunkte(List<Bewertung> bewertungen, List<Kriterium> kriteriums, bool istPraesi, bool istDoku, string bezeichnung = null)
        {
            if (bewertungen == null || bewertungen.Count == 0)
            {
                Console.WriteLine($"Keine Bewertungen verfügbar für {bezeichnung ?? "unbekannt"}!");
                return (0, 0);
            }

            // 1) Versuche, eine Bewertung mit passenden Flags und der Bezeichnung in den Kriteriums zu finden
            Bewertung bewertung = null;
            if (bezeichnung != null && kriteriums != null)
            {
                bewertung = bewertungen.FirstOrDefault(b => b.istPraesi == istPraesi && b.istDoku == istDoku && b.KriterienIds != null && b.KriterienIds.Any(id => kriteriums.Any(k => k.Id == id && k.Bezeichnung != null && k.Bezeichnung.Contains(bezeichnung))));
            }

            // 2) Falls nicht gefunden: versuche nur über Bezeichnung (unabhängig von Flags)
            if (bewertung == null && bezeichnung != null && kriteriums != null)
            {
                bewertung = bewertungen.FirstOrDefault(b => b.KriterienIds != null && b.KriterienIds.Any(id => kriteriums.Any(k => k.Id == id && k.Bezeichnung != null && k.Bezeichnung.Contains(bezeichnung))));
            }

            // 3) Falls immer noch nicht gefunden: versuche nur über Flags
            if (bewertung == null)
            {
                bewertung = bewertungen.FirstOrDefault(b => b.istPraesi == istPraesi && b.istDoku == istDoku);
            }

            // 4) Letzter Fallback: erste vorhandene Bewertung
            if (bewertung == null && bewertungen.Count > 0)
            {
                bewertung = bewertungen[0];
                Console.WriteLine($"[Warnung: Nutze Fallback-Bewertung für {bezeichnung}]");
            }

            if (bewertung == null || bewertung.KriterienIds == null || !bewertung.KriterienIds.Any())
            {
                Console.WriteLine($"Keine gültigen Kriteriums für {bezeichnung ?? "unbekannt"} gefunden!");
                return (0, 0);
            }

            // Summiere die Punkte für alle Kriteriums der gewählten Bewertung
            var punkte = 0;
            foreach (var kriteriumId in bewertung.KriterienIds)
            {
                var kriterium = kriteriums?.FirstOrDefault(k => k.Id == kriteriumId);
                if (kriterium != null)
                {
                    // Wenn eine Bezeichnung vorgegeben ist, berücksichtige Kriteriums, die die Bezeichnung enthalten;
                    // ansonsten alle Kriteriums der Bewertung.
                    if (bezeichnung == null || (kriterium.Bezeichnung != null && kriterium.Bezeichnung.Contains(bezeichnung)))
                    {
                        punkte += kriterium.Punkte;
                    }
                    else if (bezeichnung != null)
                    {
                        // Wenn das Kriterium nicht zur gewünschten Kategorie passt, überspringen
                        continue;
                    }
                }
            }

            // Berechne die Note aus den gesammelten Punkten
            double note = punkte > 0 ? EvalPro.Berechnungen.IHKNotenschluessel.BerechneNote(punkte) : 0;
            string noteString = note > 0 ? note.ToString("F1") : "Keine (0 Punkte)";
            Console.WriteLine($"Kategorie: {bezeichnung ?? "Unbekannt"} - Punkte: {punkte} - Note: {noteString}");

            return (note > 0 ? note : 0, punkte);
        }
    }
}