using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class FachgespraechRepository(IIdRepository idRepository) : IFachgespraechRepository
{
    private readonly BaseRepo _repo = new("fachgespraech.json");

    /// <summary>
    /// Gets all Fachgespraeche in the Database
    /// Usage not Recommended
    /// </summary>
    public IEnumerable<Fachgespraech> GetAll()
    {
        return (_repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader) ?? []).ToList();
    }

    /// <summary>
    /// Gets a Fachgespraech by its ID
    /// </summary>
    public Fachgespraech? GetById(int id)
    {
        return _repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader)!.FirstOrDefault(x => x.Id == id);
    }
    
    
    /// <summary>
    /// Gets all Fachgespreache by their Associated PrueflingID
    /// </summary>
    public IEnumerable<Fachgespraech> GetByPrueflingId(int prueflingId)
    {
        return (_repo.Serializer.Deserialize<List<Fachgespraech>>(_repo.Reader) ?? []).Where(x => x.PrueflingId == prueflingId);
    }

    /// <summary>
    /// Uppdates a Fachgespraech by its ID
    /// </summary>
    public void Update(Fachgespraech fachgespraech)
    { 
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == fachgespraech.Id);
        
        all[index] = fachgespraech;
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }
    
    /// <summary>
    /// Overrides the Entire Fachgespraeche Table
    /// Usage not Recommended
    /// </summary>
    public void Override(IEnumerable<Fachgespraech> fachgespraeche)
    {
        foreach (var f in fachgespraeche)
        {
            var newId = idRepository.CreateNewId();
            f.Id = newId;
        }
        _repo.Serializer.Serialize(_repo.Writer, fachgespraeche);
    }
    
    /// <summary>
    /// Adds a Fachgespraech and Returns its Provided ID
    /// </summary>
    public int Add(Fachgespraech fachgespraech)
    {
        var all = GetAll().ToList();
        var newId = idRepository.CreateNewId();
        fachgespraech.Id = newId;
        _repo.Serializer.Serialize(_repo.Writer, all.Append(fachgespraech));
        return newId;
    }
    
    /// <summary>
    /// Deletes a Fachgespraech by its ID
    /// </summary>
    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        _repo.Serializer.Serialize(_repo.Writer, all);
    }
}