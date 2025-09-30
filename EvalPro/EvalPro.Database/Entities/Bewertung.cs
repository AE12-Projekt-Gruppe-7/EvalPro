namespace EvalPro.Database.Entities;

public class Bewertung
{
    int Id { get; set; }
    
    string Gesammtkommentar { get; set; }
    
    IEnumerable<int> KriterienIds { get; set; }
    
    bool istPraesi { get; set; }

    bool istDoku { get; set; }
    
    
}