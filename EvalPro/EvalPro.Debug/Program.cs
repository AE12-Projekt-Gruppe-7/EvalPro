using EvalPro.Database.Entities;
using EvalPro.Database.Repository;
using Newtonsoft.Json;

namespace EvalPro.Debug;

internal static class Program
{
    class ListItem()
    {
        public int Id {get;set;}
        public string Name {get;set;} = "";
    }
    
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

    private static readonly DebugModel Model = new()
    {
        Id = 1,
        Name = "Item Name 1",
        ItemIds = [1,2,3,4]
    };

    private static List<Kriterium> _kriteriums =
    [
        new()
        {
            Id = 1,
            BewertungId = 2,
            Bezeichnung = "Test 1",
            Kommentar = "Kommentar1",
            Punkte = 34,
        },new(){
        Id = 13563,
        BewertungId = 2,
        Bezeichnung = "Test 1",
        Kommentar = "Kommentar1",
        Punkte = 34,
        },new(){
        Id = 135783657,
        BewertungId = 2357837,
        Bezeichnung = "3578378 1",
        Kommentar = "65784678877",
        Punkte = 34,
        },new(){
        Id = 125432525,
        BewertungId = 2,
        Bezeichnung = "Test 1",
        Kommentar = "Kommentar1",
        Punkte = 34,
        },new(){
        Id = 7678854,
        BewertungId = 2,
        Bezeichnung = "3425 1",
        Kommentar = "Kommentar1",
        Punkte = 34,
        }
    ];
    
    private static KriteriumRepository _repo = new ();

    private static void Main()
    {
        _repo.Main();
    }
}