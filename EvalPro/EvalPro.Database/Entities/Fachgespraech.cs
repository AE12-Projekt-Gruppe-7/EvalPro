namespace EvalPro.Database.Entities;

public class Fachgespraech
{
    int Id { get; set; }

    string Gesammtkommentar { get; set; }
    
    IEnumerable<int> FragenIds { get; set; }
}