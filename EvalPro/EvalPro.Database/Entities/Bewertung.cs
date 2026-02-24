using System.ComponentModel.DataAnnotations;

namespace EvalPro.Database.Entities;

public class Bewertung
{
    public int Id { get; set; }
    
    public int PrueflingId { get; set; }
    
    public Pruefling? Pruefling { get; set; }
    
    [MaxLength(255)]
    public string Gesammtkommentar { get; set; }
    
    public IEnumerable<int> KriterienIds { get; set; }
    
    public bool istPraesi { get; set; }

    public bool istDoku { get; set; }
}