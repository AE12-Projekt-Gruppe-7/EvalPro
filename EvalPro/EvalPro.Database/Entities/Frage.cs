using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvalPro.Database.Entities;

public class Frage
{
    public int Id {get; set;}
    
    public string FrageInhalt {get; set;}
    
    public string Kommentar {get; set;}
    
    [Range(0, 100)]
    public int Punkte  {get; set;}

    public int GespraechId { get; set; }
    
}
