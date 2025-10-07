using EvalPro.Database.Entities;
using Newtonsoft.Json;

namespace EvalPro.Debug;

internal class Program
{
    private static List<ListItem> _items =
    [
        new()
        {
            Id = 1,
            Name = "Item Name 1"
        },

        new()
        {
            Id = 2,
            Name = "New Name 2"
        },

        new()
        {
            Id = 3,
            Name = "Item Name 3"
        },

        new()
        {
            Id = 4,
            Name = "Item Name 4"
        }
    ];

    private static readonly DebugModel _model = new()
    {
        Id = 1,
        Name = "Item Name 1",
        ItemIds = [1,2,3,4]
    };

    private static void Main(string[] args)
    {
        Console.WriteLine(JsonConvert.SerializeObject(_model));
        Console.WriteLine();

        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        var serializer = JsonSerializer.Create(settings);
        
        using (StreamWriter sw = new StreamWriter(@"../../../jsons/model.json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, _model);
        }
        
        using (StreamWriter sw = new StreamWriter(@"../../../jsons/items.json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, _items);
        }

        IEnumerable<ListItem> deItems;
        using (JsonReader reader = new JsonTextReader(new StreamReader(@"../../../jsons/items.json")))
        {
            deItems = serializer.Deserialize<List<ListItem>>(reader);
        }

        Console.WriteLine(deItems);

    }
}