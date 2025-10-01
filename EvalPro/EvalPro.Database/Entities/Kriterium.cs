namespace EvalPro.Database.Entities;

public class Kriterium
{
    public int Id { get; set; }
    
    public int BewertungId { get; set; }
    
    public string Bezeichnung { get; set; }
    
    public int Punkte {get; set;}
    
    public string Kommentar { get; set; }
}