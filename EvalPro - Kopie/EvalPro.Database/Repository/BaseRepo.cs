using Newtonsoft.Json;

namespace EvalPro.Database.Repository;

public class BaseRepo
{
    public readonly JsonWriter Writer;
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings();
    public readonly JsonSerializer Serializer;
    public readonly JsonReader Reader;

    public BaseRepo(string path)
    {
        var sw = new StreamWriter("../../../jsons/" + path);
        Writer = new JsonTextWriter(sw);
        
        var sr = new StreamReader("../../../jsons/" + path);
        Reader = new JsonTextReader(sr);
        
        _settings.Formatting = Formatting.Indented;
        Serializer = JsonSerializer.Create(_settings);
    }
}