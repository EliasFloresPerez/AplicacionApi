using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AplicacionApi.Data;
using AplicacionApi.DTO;
using AplicacionApi.Servicios;
using AplicacionApi.Interfaces;

namespace AplicacionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        private  IAseguradora iaseguradora;

        public AseguradoraController(IAseguradora _iaseguradora)
        {
            this.iaseguradora = _iaseguradora;
        }


        //Insertar un cliente con su(s) seguro con una ruta llamada "api/IngresarCliente"
        [HttpPost("IngresarCliente")]
        public IActionResult Post(ClienteDto cliente)
        {
            try
            {
                var retorno = this.iaseguradora.IngresarCliente(cliente);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Obtener todos los clientes con sus seguros o un cliente con sus seguros por su cedula
        [HttpGet("ObtenerClientes")]
        public IActionResult Get(string? cedula = null)
        {
            try
            {
                var retorno = this.iaseguradora.RetornarClientes(cedula);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Actualizar cliente
        [HttpPut("ActualizarCliente")]
        public IActionResult Put(ClienteDto cliente)
        {
            try
            {
                var retorno = this.iaseguradora.ActualizarCliente(cliente);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Eliminar Cliente
        [HttpDelete("EliminarCliente")]
        public IActionResult Delete(string cedula)
        {
            try
            {
                var retorno = this.iaseguradora.EliminarCliente(cedula);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Crear un seguro
        [HttpPost("IngresarSeguro")]

        public IActionResult Post(SeguroDto seguro)
        {
            try
            {
                var retorno = this.iaseguradora.IngresarSeguro(seguro);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Obtener todos los seguros o un seguro por su codigo
        [HttpGet("ObtenerSeguros")]
        public IActionResult Get(int codigo = 0)
        {
            try
            {
                var retorno = this.iaseguradora.RetornarSeguros(codigo);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Actualizar seguro

        [HttpPut("ActualizarSeguro")]

        public IActionResult Put(int codigo, SeguroDto seguro)
        {
            try
            {
                var retorno = this.iaseguradora.ActualizarSeguro(codigo, seguro);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Eliminar seguro

        [HttpDelete("EliminarSeguro")]

        public IActionResult Delete(int codigo)
        {
            try
            {
                var retorno = this.iaseguradora.EliminarSeguro(codigo);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Asignar un seguro a un cliente

        [HttpPost("AsociarSeguro")]

        public IActionResult Post(string cedula, int codigo)
        {
            try
            {
                var retorno = this.iaseguradora.AsociarSeguro(cedula, codigo);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Ingresar clientes via csv

        [HttpPost("IngresarClientesCsv")]

        public IActionResult Post(IFormFile file)
        {
            try
            {
                var retorno = this.iaseguradora.IngresarClientesCsv(file);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }   
        }


        //Ingresar seguros via csv

        [HttpPost("IngresarSegurosCsv")]

        public IActionResult PostSeguros(IFormFile file)
        {
            try
            {
                var retorno = this.iaseguradora.IngresarSegurosCsv(file);

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
