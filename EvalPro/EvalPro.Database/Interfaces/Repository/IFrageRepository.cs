using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IFrageRepository
{
    public IEnumerable<Frage> GetAll();
    
    public Frage? GetById(int id);
    
    public IEnumerable<Frage> GetByGespraechId(int gespraechId);
    
    public int Add(Frage f);
    
    public void Update(Frage f);
    
    public void Delete(int id);

    public void Override(IEnumerable<Frage> fs);
}