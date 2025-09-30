namespace EvalPro.Database.Entities;

public class Pruefling
{
    int Id { get; set; }
    
    string Name { get; set; }
    
    string Ausbildungsbetrieb { get; set; }

    string Ansprechpartner { get; set; }
    
    string ProjektThema { get; set; }

    string AusschussId { get; set; }
    
    int DokuBewertungsId  { get; set; }
    
    int PraesiBewertungsId { get; set; }

    int FachgesprächId { get; set; }
}