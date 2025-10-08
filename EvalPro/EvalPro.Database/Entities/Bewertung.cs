namespace EvalPro.Database.Entities;

public class Bewertung
{
    public int Id { get; set; }
    
    public int PrueflingId { get; set; }
    
    public string Gesammtkommentar { get; set; }
    
    public IEnumerable<int> KriterienIds { get; set; }
    
    public bool istPraesi { get; set; }

    public bool istDoku { get; set; }
}