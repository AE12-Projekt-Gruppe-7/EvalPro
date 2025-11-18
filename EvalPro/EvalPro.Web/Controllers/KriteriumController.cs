using EvalPro.Database.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
namespace EvalPro.Web;

[ApiController]
[Route("api/[controller]")]
public class KriteriumController(IKriteriumRepository kriteriumRepository) : Controller
{
    [HttpGet("")]
    public IActionResult GetAllKriterium()
    {
        return Ok(kriteriumRepository.GetAll());
    }
}