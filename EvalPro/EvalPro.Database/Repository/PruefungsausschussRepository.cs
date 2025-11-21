using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class PruefungsausschussRepository(IIdRepository _idRepository) : IPruefungsausschussRepository
{
    private readonly BaseRepo _repo = new("pruefungsausschuss.json");

    public IEnumerable<Pruefungsausschuss> GetAll()
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Pruefungsausschuss>>(_repo.Reader) ?? []).ToList();
    }

    public Pruefungsausschuss? GetById(int id)
    {
        return (_repo.Serializer.Deserialize<IEnumerable<Pruefungsausschuss>>(_repo.Reader) ?? []).FirstOrDefault(x => x.Id == id);
    }

    public int Add(Pruefungsausschuss f)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        f.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(f));
        return newId;
    }

    public void Update(Pruefungsausschuss f)
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

    public void Override(IEnumerable<Pruefungsausschuss> fs)
    {
        foreach (var f in fs)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, fs);
    }
}