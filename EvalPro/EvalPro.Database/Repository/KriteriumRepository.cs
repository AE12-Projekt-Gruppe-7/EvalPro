using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class KriteriumRepository(IIdRepository _idRepository) : IKriteriumRepository
{
    private readonly BaseRepo _repo = new("kriterium.json");

    public IEnumerable<Kriterium> GetAll()
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Kriterium>>(_repo.Reader) ?? []).ToList();
    }

    public Kriterium? GetById(int id)
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Kriterium>>(_repo.Reader) ?? []).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Kriterium> GetByBewertungId(int bId)
    {
        return (_repo.Serializer.Deserialize<List<Kriterium>>(_repo.Reader) ?? []).Where(x => x.BewertungId == bId);
    }

    public int Add(Kriterium k)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        k.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(k));
        return newId;
    }

    public void Update(Kriterium k)
    {
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == k.Id);
        
        all[index] = k;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
        
    }

    public void Override(IEnumerable<Kriterium> ks)
    {
        foreach (var f in ks)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, ks);
    }
}