using Newtonsoft.Json;

namespace EvalPro.Database.Repository;

public class BaseRepo
{
    public readonly JsonWriter Writer;
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings();
    public readonly JsonSerializer Serializer;
    public readonly JsonReader Reader;
    public readonly FileStream Str;

    public BaseRepo(string path)
    {
        Str = new FileStream("./jsons/" + path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        var sw = new StreamWriter(Str);
        sw.AutoFlush = true;
        Writer = new JsonTextWriter(sw);
        
        var sr = new StreamReader(Str);
        Reader = new JsonTextReader(sr);
        
        _settings.Formatting = Formatting.Indented;
        Serializer = JsonSerializer.Create(_settings);
    }
}