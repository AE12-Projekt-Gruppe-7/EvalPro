using EvalPro.Database.Entities;

namespace EvalPro.Database.Interfaces.Repository;

public interface IPruefungsausschussRepository
{
    public IEnumerable<Pruefungsausschuss> GetAll();
    
    public Pruefungsausschuss? GetById(int id);
    
    public void Add(Pruefungsausschuss f);
    
    public void Update(Pruefungsausschuss f);
    
    public void Delete(int id);

    public void Override(IEnumerable<Pruefungsausschuss> fs);
}