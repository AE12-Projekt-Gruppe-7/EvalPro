using EvalPro.Database.Entities;

namespace EvalPro.Database.DTOs;

public class BewertungDTO
{
    public int Id { get; set; }
    
    public int PrueflingId { get; set; }
    
    public Pruefling Pruefling { get; set; }
    
    public string Gesammtkommentar { get; set; }
    
    public IEnumerable<int> KriterienIds { get; set; }
    
    public IEnumerable<Kriterium> Kriterien { get; set; }
    
    public bool istPraesi { get; set; }

    public bool istDoku { get; set; }
}