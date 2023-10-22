

using AplicacionApi.DTO;
using AplicacionApi.Modelos;

namespace AplicacionApi.Interfaces
{
    public interface IAseguradora
    {
        //Funciones para el CRUD de clientes
        public Cliente IngresarCliente(ClienteDto cliente);
        public object RetornarClientes(string cedula);

        public Cliente ActualizarCliente(ClienteDto cliente);

        public Cliente EliminarCliente(string cedula);

        //Funciones para el CRUD de seguros
        public Seguro IngresarSeguro(SeguroDto seguro);
        public object RetornarSeguros(int codigo);

        public Seguro ActualizarSeguro(int codigo, SeguroDto seguro);

        public Seguro EliminarSeguro( int codigo);
        public Cliente AsociarSeguro(string cedula, int codigo);

        //Ingreso de archivos via csv

        public object IngresarClientesCsv(IFormFile file);

        public object IngresarSegurosCsv(IFormFile file);
        
    }
}
