namespace EvalPro.Database.Entities;

public class Pruefungsausschuss
{
    public int Id  { get; set; }
    
    public string Bezeichnung { get; set; }

    public string Ausbildungsberuf { get; set; }
    
    public IEnumerable<DateOnly> Pruefungstage {get; set;}
    
    public IEnumerable<int> PrueflingeIds { get; set; }
}
