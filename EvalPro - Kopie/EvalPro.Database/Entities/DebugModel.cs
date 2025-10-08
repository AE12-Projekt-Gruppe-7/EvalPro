namespace EvalPro.Database.Entities;

public class DebugModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<int> ItemIds { get; set; }
}