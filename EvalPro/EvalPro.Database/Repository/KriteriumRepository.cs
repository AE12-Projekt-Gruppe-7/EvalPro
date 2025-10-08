using EvalPro.Database.Entities;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class KriteriumRepository : IKriteriumRepository
{
    public readonly BaseRepo Repo = new("kriterium.json");

    public void Main()
    {
        List<Kriterium> _kriteriums =
    [
        new()
        {
            Id = 1,
            BewertungId = 2,
            Bezeichnung = "Test 1",
            Kommentar = "Kommentar1",
            Punkte = 34,
        },new(){
            Id = 13563,
            BewertungId = 2,
            Bezeichnung = "Test 1",
            Kommentar = "Kommentar1",
            Punkte = 34,
        },new(){
            Id = 135783657,
            BewertungId = 2357837,
            Bezeichnung = "3578378 1",
            Kommentar = "65784678877",
            Punkte = 34,
        },new(){
            Id = 125432525,
            BewertungId = 2,
            Bezeichnung = "Test 1",
            Kommentar = "Kommentar1",
            Punkte = 34,
        },new(){
            Id = 7678854,
            BewertungId = 2,
            Bezeichnung = "3425 1",
            Kommentar = "Kommentar1",
            Punkte = 34,
        }
    ];
        Override(_kriteriums);
    }

    public IEnumerable<Kriterium> GetAll()
    {
        return (Repo.Serializer.Deserialize<IEnumerable<Kriterium>>(Repo.Reader) ?? []).ToList();
    }

    public Kriterium? GetById(int id)
    {
        return (Repo.Serializer.Deserialize<IEnumerable<Kriterium>>(Repo.Reader) ?? []).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Kriterium> GetByBewertungId(int bId)
    {
        return (Repo.Serializer.Deserialize<List<Kriterium>>(Repo.Reader) ?? []).Where(x => x.BewertungId == bId);
    }

    public void Add(Kriterium k)
    {
        var all = GetAll().ToList();
        
        Repo.Serializer.Serialize(Repo.Writer, all.Append(k));
    }

    public void Update(Kriterium k)
    {
        var all = GetAll().ToList();
        var index = all.FindIndex(x => x.Id == k.Id);
        
        all[index] = k;
        
        Repo.Serializer.Serialize(Repo.Writer, all);
    }

    public void Delete(int id)
    {
        var all = GetAll().ToList();
        all.Remove(all.Find(x => x.Id == id)!);
        
        Repo.Serializer.Serialize(Repo.Writer, all);
        
    }

    public void Override(IEnumerable<Kriterium> ks)
    {
        Repo.Serializer.Serialize(Repo.Writer, ks);
    }
}