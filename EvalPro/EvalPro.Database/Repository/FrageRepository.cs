using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class FrageRepository(IIdRepository _idRepository) : IFrageRepository
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
        return (_repo.Serializer.Deserialize<List<Frage>>(_repo.Reader) ?? []).Where(x => x.GespraechId == gespraechId);
    }

    public int Add(Frage f)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        f.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(f));
        return newId;
    }

    public void Update(Frage f)
    {
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == f.Id);
        
        all[index] = f;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    public void Override(IEnumerable<Frage> fs)
    {
        foreach (var f in fs)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, fs);
    }
}