namespace EvalPro.Database.Repository;

public class ItemRepository
{
    private BaseRepo repo;

    public ItemRepository()
    {
        repo = new BaseRepo("../../../jsons/items.json");
    }
}