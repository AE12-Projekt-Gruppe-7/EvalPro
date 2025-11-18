using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class FachgespraechRepository(IIdRepository _idRepository) : IFachgespraechRepository
{
    private readonly BaseRepo _repo = new("fachgespraech.json");

    public IEnumerable<Fachgespraech> GetAll()
    {
        return (_repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader) ?? []).ToList();
    }

    public Fachgespraech? GetById(int id)
    {
        return _repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader)!.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Fachgespraech> GetByPrueflingId(int prueflingId)
    {
        return (_repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader) ?? []).Where(x => x.PrueflingId == prueflingId);
    }

    public void Update(Fachgespraech fachgespraech)
    { 
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == fachgespraech.Id);
        
        all[index] = fachgespraech;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    public void Override(IEnumerable<Fachgespraech> fachgespraeche)
    {
        foreach (var f in fachgespraeche)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, fachgespraeche);
    }

    public void Add(Fachgespraech fachgespraech)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        fachgespraech.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(fachgespraech));
    }

    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }
}