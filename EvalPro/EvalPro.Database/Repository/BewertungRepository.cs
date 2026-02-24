using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class BewertungRepository(IIdRepository idRepository) : IBewertungRepository
{
    private readonly BaseRepo _repo = new("bewertung.json");
    
    /// <summary>
    /// Gets all Bewertungen in the Database
    /// Use not Recommended
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Bewertung> GetAll()
    {
        return (_repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader) ?? []).ToList();
    }

    /// <summary>
    /// Tries to get a Bewertung from the Database by its ID
    /// </summary>
    public Bewertung? GetById(int id)
    {
        return _repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader)!.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Gets all Bewertungen associated with the provided PrueflingId
    /// </summary>
    public IEnumerable<Bewertung> GetByPrueflingId(int prueflingId)
    {
        return (_repo.Serializer.Deserialize<List<Bewertung>>(_repo.Reader) ?? []).Where(x => x.PrueflingId == prueflingId);
    }

    /// <summary>
    /// Updates a Bewertung by its provided ID
    /// </summary>
    public void Update(Bewertung bewertung)
    { 
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == bewertung.Id);
        
        all[index] = bewertung;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }

    /// <summary>
    /// Overrides all Entries
    /// IDs will not be Provided by ID Provider
    /// </summary>
    public void Override(IEnumerable<Bewertung> bewertungen)
    {
        _repo.Serializer.Serialize(_repo.Writer, bewertungen);
    }

    /// <summary>
    /// Adds a new Bewertung and returns Its ID
    /// </summary>
    public int Add(Bewertung bewertung)
    {
        var all = GetAll().ToList();
        var newId = idRepository.CreateNewId();
        bewertung.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(bewertung));
        return bewertung.Id;
    }

    /// <summary>
    /// Deletes a Bewertung by Its ID
    /// </summary>
    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }
}