using EvalPro.Database.Interfaces.Repository;
namespace EvalPro.Database.Repository;

public class IdRepository : IIdRepository
{
    private readonly BaseRepo _repo = new("ids.json");
    
    public int CreateNewId()
    {
        int? counter;
        try
        { 
            counter = _repo.Serializer.Deserialize<int?>(_repo.Reader);
        }
        catch
        {
            Console.WriteLine("No ID Database found, restarting from 0");
            _repo.Serializer.Serialize(_repo.Writer, 1);
            return 0;
        }

        if (counter != null)
        {
            _repo.Serializer.Serialize(_repo.Writer, ++counter);
            return (int)counter;
        }

        _repo.Serializer.Serialize(_repo.Writer, 0);
            return 0;
    }
}
