using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class FrageRepository : IFrageRepository
{
    private readonly BaseRepo _repo = new("frage.json");
    
    public IEnumerable<Frage> GetAll()
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Frage>>(_repo.Reader) ?? []).ToList();
    }

    public Frage? GetById(int id)
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Frage>>(_repo.Reader) ?? []).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Frage> GetByGespraechId(int gespraechId)
    {
        throw new NotImplementedException();
    }

    public void Add(Frage f)
    {
        throw new NotImplementedException();
    }

    public void Update(Frage f)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Override(IEnumerable<Frage> fs)
    {
        throw new NotImplementedException();
    }
}