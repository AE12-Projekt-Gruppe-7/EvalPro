using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class PrueflingRepository(IIdRepository _idRepository) : IPrueflingRepository
{
    private readonly BaseRepo _repo = new("pruefling.json");

    public IEnumerable<Pruefling> GetAll()
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Pruefling>>(_repo.Reader) ?? []).ToList();
    }

    public Pruefling? GetById(int id)
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Pruefling>>(_repo.Reader) ?? []).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Pruefling> GetByAusschussId(int aId)
    {
        return (_repo.Serializer.Deserialize<List<Pruefling>>(_repo.Reader) ?? []).Where(x => x.AusschussId == aId);
    }

    public int Add(Pruefling f)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        f.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(f));
        return newId;
    }

    public void Update(Pruefling f)
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

    public void Override(IEnumerable<Pruefling> fs)
    {
        foreach (var f in fs)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, fs);
    }
}