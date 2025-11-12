using EvalPro.Database.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
namespace EvalPro.Web;

[Controller]
public class KriteriumController(IKriteriumRepository kriteriumRepository) : Controller
{
    [HttpGet("")]
    public IActionResult GetAllKriterium()
    {
        return Ok(kriteriumRepository.GetAll());
    }
}