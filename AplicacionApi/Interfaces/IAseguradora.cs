

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
        
    }
}
