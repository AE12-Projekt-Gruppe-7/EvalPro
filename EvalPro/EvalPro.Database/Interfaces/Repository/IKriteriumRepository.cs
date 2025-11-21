using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IKriteriumRepository
{
    public IEnumerable<Kriterium> GetAll();
    
    public Kriterium? GetById(int id);
    
    public IEnumerable<Kriterium> GetByBewertungId(int bId);
    
    public int Add(Kriterium k);
    
    public void Update(Kriterium k);
    
    public void Delete(int id);

    public void Override(IEnumerable<Kriterium> ks);
}