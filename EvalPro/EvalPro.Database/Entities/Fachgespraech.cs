namespace EvalPro.Database.Entities;

public class Fachgespraech
{
    public int Id { get; set; }

    public string Gesammtkommentar { get; set; }
    
    public IEnumerable<int> FragenIds { get; set; }
    
    public int PrueflingId { get; set; }
}