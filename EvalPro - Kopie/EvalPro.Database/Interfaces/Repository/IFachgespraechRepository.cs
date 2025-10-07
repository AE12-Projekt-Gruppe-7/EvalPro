using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IFachgespraechRepository
{
    public IEnumerable<Fachgespraech> GetAll();
    
    public Fachgespraech? GetById(int id);
    
    public IEnumerable<Fachgespraech> GetByPrueflingId(int prueflingId);
    
    public void Update(Fachgespraech fachgespraech);
    
    public void Override(IEnumerable<Fachgespraech> fachgespraeche);
    
    public void Add(Fachgespraech fachgespraech);
    
    public void Delete(int id);

}