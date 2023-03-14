using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteApiController : ControllerBase
    {
        [HttpGet("listarID/{id}")]

        public IActionResult Get(int id)
        {
            var cliente = new List<Cliente>
            {
                new Cliente {
                    Identificacion="1", Nombres="Harry", Apellidos="Potter", Edad=25, Pais="London"},
                  new Cliente {
                    Identificacion="2", Nombres="James", Apellidos="Raj", Edad=20, Pais="Ecuador"}
            };
            if (id==0)
            {
                return NotFound();
            }
            else
            {
                return Ok(cliente.ElementAt(id));
            }
        }
    }
}
