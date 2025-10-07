using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IBewertungRepository
{
    public IEnumerable<Bewertung> GetAll();
    
    public Bewertung? GetById(int id);
    
    public IEnumerable<Bewertung> GetByPrueflingId(int prueflingId);
    
    public void Update(Bewertung bewertung);
    
    public void Override(IEnumerable<Bewertung> bewertungen);
    
    public void Add(Bewertung bewertung);
    
    public void Delete(int id);
    
}