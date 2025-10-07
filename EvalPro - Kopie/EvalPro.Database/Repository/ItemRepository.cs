using EvalPro.Database.Interfaces.Repository;

namespace EvalPro.Database.Repository;

public class ItemRepository : IItemRepository
{
    private BaseRepo repo;

    public ItemRepository()
    {
        repo = new BaseRepo("items.json");
    }
}