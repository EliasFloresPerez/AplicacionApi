using AplicacionApi.Data;
using AplicacionApi.DTO;



namespace AplicacionApi.Servicios
{
    public class ClientesService
    {
        private readonly  AseguradoraContext _context;

        public ClientesService(AseguradoraContext context) {
            this._context = context;
        }


        public void IngresarCliente(ClienteDto cliente ,List<SeguroDto> seguros)
        {
            //Verificamos que la cedula del cliente no se encuentre en la base de datos
            //Si no se encuentra, se ingresa el cliente y sus seguros



        }
    }
}
