namespace EvalPro.Database.Entities;

public class Pruefling
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Ausbildungsbetrieb { get; set; }

    public string Ansprechpartner { get; set; }
    
    public string ProjektThema { get; set; }

    public int AusschussId { get; set; }
    
    public int DokuBewertungsId  { get; set; }
    
    public int PraesiBewertungsId { get; set; }

    public int FachgesprächId { get; set; }

}