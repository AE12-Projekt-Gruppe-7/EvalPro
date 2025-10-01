namespace EvalPro.Database.Entities;

public class Frage
{
    public int Id {get; set;}
    
    public string FrageInhalt {get; set;}
    
    public string Kommentar {get; set;}
    
    public int Punkte  {get; set;}

    public int GespraechId { get; set; }
    
}