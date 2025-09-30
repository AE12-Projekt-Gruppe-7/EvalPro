using Newtonsoft.Json;

namespace EvalPro.Database.Repository;

public class BaseRepo
{
    public JsonWriter Writer;

    public BaseRepo(string path)
    {
        var sw = new StreamWriter(path);
        Writer = new JsonTextWriter(sw);
    }
}