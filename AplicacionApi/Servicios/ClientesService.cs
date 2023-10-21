using AplicacionApi.Data;
using AplicacionApi.DTO;
using AplicacionApi.Interfaces;
using AplicacionApi.Modelos;

namespace AplicacionApi.Servicios
{
    public class ClientesService : IAseguradora
    {
        private readonly  AseguradoraContext _context;

        public ClientesService(AseguradoraContext context) {
            this._context = context;
        }


        public Cliente IngresarCliente(ClienteDto cliente)
        {
            //Verificamos que no se encuentre el cliente en la base de datos
            var clienteExistente = _context.Clientes.Find(cliente.Cedula);
            if (clienteExistente != null)
            {
                throw new System.Exception("El cliente ya existe");
            }

            var clienteNuevo = new Cliente()
            {
                Cedula = cliente.Cedula,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Edad = cliente.Edad
            };

            _context.Clientes.Add(clienteNuevo);
            _context.SaveChanges();

            return clienteNuevo;
        }


        //Retornar todos los clientes o solo uno por la cedula si es null o no

        public object RetornarClientes(string cedula)
        {
            if (cedula == null)
            {
                
                var clientes = _context.Clientes.ToList();
                return clientes;
            }
            else
            {
                // Si cedula tiene un valor, buscar y devolver el cliente correspondiente
                var cliente = _context.Clientes.Find(cedula);

                
                if (cliente == null)
                {
                    return new { mensaje = "Cliente no encontrado" };
                }

                return cliente;
            }
        }

        public Cliente ActualizarCliente(ClienteDto cliente)
        {
            //Verificamos que se encuentre el cliente en la base de datos
            var clienteExistente = _context.Clientes.Find(cliente.Cedula);
            if (clienteExistente == null)
            {
                throw new System.Exception("El cliente no existe");
            }

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Edad = cliente.Edad;

            _context.Clientes.Update(clienteExistente);
            _context.SaveChanges();

            return clienteExistente;
        }


        public Cliente EliminarCliente(string cedula)
        {
            //Verificamos que se encuentre el cliente en la base de datos
            var clienteExistente = _context.Clientes.Find(cedula);
            if (clienteExistente == null)
            {
                throw new System.Exception("El cliente no existe");
            }

            _context.Clientes.Remove(clienteExistente);
            _context.SaveChanges();

            return clienteExistente;
        }


        //Funciones para el CRUD de seguros

        public Seguro IngresarSeguro(SeguroDto seguro)
        {
            //Creamos el codigo del seguro con un numero aleatorio de 5 digitos
            //Simplemente por motivos esteticos de no tener un codigo autoincremental

            int codigo;

            do
            {
                codigo = new System.Random().Next(10000, 99999);
            } while (_context.Seguros.Find(codigo) != null);

            var seguroNuevo = new Seguro()
            {
                Codigo = codigo,
                Nombre = seguro.Nombre,
                SumaAsegurada = seguro.SumaAsegurada,
                Prima = seguro.Prima

            };


            _context.Seguros.Add(seguroNuevo);
            _context.SaveChanges();
            return seguroNuevo;

        }

        public object RetornarSeguros(int codigo)
        {
            if (codigo == 0)
            {
                var seguros = _context.Seguros.ToList();
                return seguros;
            }
            else
            {
                // Si codigo tiene un valor, buscar y devolver el seguro correspondiente
                var seguro = _context.Seguros.Find(codigo);

                if (seguro == null)
                {
                    return new { mensaje = "Seguro no encontrado" };
                }

                return seguro;
            }
        }
    }

}
