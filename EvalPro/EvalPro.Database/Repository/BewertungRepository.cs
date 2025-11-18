using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class BewertungRepository(IIdRepository _idRepository) : IBewertungRepository
{
    private readonly BaseRepo _repo = new("bewertung.json");

    public IEnumerable<Bewertung> GetAll()
    {
        return (_repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader) ?? []).ToList();
    }

    public Bewertung? GetById(int id)
    {
        return _repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader)!.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Bewertung> GetByPrueflingId(int prueflingId)
    {
        return (_repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader) ?? []).Where(x => x.PrueflingId == prueflingId);
    }

    public void Update(Bewertung bewertung)
    { 
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == bewertung.Id);
        
        all[index] = bewertung;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    public void Override(IEnumerable<Bewertung> bewertungen)
    {
        foreach (var f in bewertungen)
        {
            var newId = _idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, bewertungen);
    }

    public void Add(Bewertung bewertung)
    {
        var all = GetAll().ToList();
        var newId = _idRepository.CreateNewId();
        bewertung.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(bewertung));
    }

    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }
}