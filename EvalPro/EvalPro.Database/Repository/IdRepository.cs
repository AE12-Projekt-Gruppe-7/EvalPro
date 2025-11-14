using EvalPro.Database.Exceptions;
using EvalPro.Database.Interfaces.Repository;
namespace EvalPro.Database.Repository;

public class IdRepository : IIdRepository
{
    private readonly BaseRepo _repo = new("ids.json");
    
    public int CreateNewId()
    {
        int? counter = null;
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

        _repo.Serializer.Serialize(_repo.Writer, counter + 1);
        return counter??0;
    }

    private bool RebuildIdCounter()
    {
        return true;
    }
}
