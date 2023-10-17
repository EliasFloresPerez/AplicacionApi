using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AplicacionApi.Data;

namespace AplicacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        private AseguradoraContext _context;

        public AseguradoraController(AseguradoraContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Seguros);
        }

    }
}
