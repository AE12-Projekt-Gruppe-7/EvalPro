# EvalPro.Debug - JSON Struktur

## Dateien und deren Struktur

### pruefling.json
Liste aller Prüflinge mit ihren Basisdaten.
- **Id**: Eindeutige Prüflings-ID (1-4)
- **Name**: Vollständiger Name
- **Vorname/Nachname**: Separat gespeichert
- **Matrikelnummer**: Eindeutige Matrikelnummer

**Aktuell**: 4 Prüflinge

### kriterium.json
Alle Bewertungskriterien mit ihren Punktwerten.
- **Id**: Eindeutige Kriterium-ID (1-8)
- **BewertungId**: Zuordnung zur Bewertungskategorie (1-4)
- **Bezeichnung**: Aussagekräftige Bezeichnung der Kategorie
- **Punkte**: Erreichbare Punkte für dieses Kriterium (18-30)
- **Kommentar**: Beschreibung/Hinweise

**Aktuell**: 8 Kriteriums für 4 Kategorien
- Präsentation und Fachgespräch (IDs 1-2)
- Projektdokumentation (IDs 3-4)
- Planen eines Softwareprodukts (IDs 5-6)
- Anwendungsentwicklung (IDs 7-8)

### model.json
Bewertungen pro Prüfling und Kategorie.
- **Id**: Eindeutige Bewertungs-ID (1-16)
- **PrueflingId**: Referenz zur Prüflings-ID (1-4)
- **Name**: Beschreibung der Bewertung
- **ItemIds**: Array von Kriterium-IDs (z.B. [1,2])
- **istPraesi**: boolean - Markiert Präsentation/Fachgespräch
- **istDoku**: boolean - Markiert Projektdokumentation

**Aktuell**: 20 Bewertungen
- 5 Prüflinge × 4 Kategorien
- Jeder Prüfling hat 4 Bewertungen (eine pro Kategorie)

## Regeln für die Struktur

1. **Eindeutige IDs**: Alle IDs müssen eindeutig sein
2. **Referentielle Integrität**: ItemIds müssen existierende Kriterium-IDs sein
3. **Flags istPraesi/istDoku**: 
   - Präsentation: istPraesi=true, istDoku=false
   - Projektdokumentation: istPraesi=false, istDoku=true
   - Andere: istPraesi=false, istDoku=false
4. **Bezeichnungen**: Müssen aussagekräftig und eindeutig sein

## Validierungsergebnisse

✓ pruefling.json: 5 Prüflinge
✓ kriterium.json: 8 Kriteriums mit korrekten Bezeichnungen
✓ model.json: 20 Bewertungen mit richtiger Struktur
