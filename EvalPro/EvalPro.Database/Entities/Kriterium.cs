namespace EvalPro.Database.Entities;

public class Kriterium
{
    int Id { get; set; }
    
    string Bezeichnung { get; set; }
    
    int Punkte {get; set;}
    
    double Wertung { get; set; }
    
    string Kommentar { get; set; }
}