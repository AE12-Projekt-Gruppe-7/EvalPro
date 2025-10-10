using EvalPro.Database.Entities;
using EvalPro.Database.Repository;
using Newtonsoft.Json;

namespace EvalPro.Debug;

internal class Program
{

    private static KriteriumRepository _repo = new ();
    
        private static void Main()
        {
            System.IO.Directory.CreateDirectory("jsons");
            _repo.Main();
        }
}