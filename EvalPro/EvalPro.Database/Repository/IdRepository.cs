using EvalPro.Database.Exceptions;
using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class IdRepository : IIdRepository
{
    private readonly BaseRepo _repo = new("ids.json");
    
    public int CreateNewId()
    {
        var counter = _repo.Serializer.Deserialize<int?>(_repo.Reader);

        if (counter == null)
        {
            throw new MissingDbException("Id could not be retrieved from Database, rebuilding Id Database.");
        }
        
        
        return 0;
    }

    private bool RebuildIdCounter()
    {
        return true;
    }
}
