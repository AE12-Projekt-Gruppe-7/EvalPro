using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IPrueflingRepository
{
    public IEnumerable<Pruefling> GetAll();
    
    public Pruefling? GetById(int id);
    
    public IEnumerable<Pruefling> GetByAusschussId(int aId);
    
    public int Add(Pruefling f);
    
    public void Update(Pruefling f);
    
    public void Delete(int id);

    public void Override(IEnumerable<Pruefling> fs);
}