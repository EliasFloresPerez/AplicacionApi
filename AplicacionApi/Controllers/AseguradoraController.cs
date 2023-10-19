using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AplicacionApi.Data;
using AplicacionApi.DTO;
using AplicacionApi.Servicios;

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


        //Insertar un cliente con su(s) seguro con una ruta llamada "api/IngresarCliente"
        [HttpPost("IngresarCliente")]
        public IActionResult Post(ClienteDto cliente)
        {
            try
            {

                //ClientesService clientesService = new ClientesService(_context);
                //clientesService.IngresarCliente(cliente, seguros);
                
                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error al ingresar el cliente");
            }
        }
        

    }
}
