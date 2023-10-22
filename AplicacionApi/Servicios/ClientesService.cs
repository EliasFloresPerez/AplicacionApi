using AplicacionApi.Data;
using AplicacionApi.DTO;
using AplicacionApi.Interfaces;
using AplicacionApi.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;

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


        public Seguro ActualizarSeguro(int codigo, SeguroDto seguro)
        {
            //Verificamos que se encuentre el seguro en la base de datos
            var seguroExistente = _context.Seguros.Find(codigo);
            if (seguroExistente == null)
            {
                throw new System.Exception("El seguro no existe");
            }

            seguroExistente.Nombre = seguro.Nombre;
            seguroExistente.SumaAsegurada = seguro.SumaAsegurada;
            seguroExistente.Prima = seguro.Prima;

            _context.Seguros.Update(seguroExistente);
            _context.SaveChanges();

            return seguroExistente;
        }


        public Seguro EliminarSeguro(int codigo)
        {
            //Verificamos que se encuentre el seguro en la base de datos
            var seguroExistente = _context.Seguros.Find(codigo);
            if (seguroExistente == null)
            {
                throw new System.Exception("El seguro no existe");
            }

            _context.Seguros.Remove(seguroExistente);
            _context.SaveChanges();

            return seguroExistente;
        }

        //Para asosciar seguros  a clientes

        public Cliente AsociarSeguro(string cedula, int codigo)
        {
            // Verificamos que se encuentre el cliente en la base de datos
            var clienteExistente = _context.Clientes
                .Include(c => c.Seguros)
                .FirstOrDefault(x => x.Cedula == cedula);

            if (clienteExistente == null)
            {
                throw new System.Exception("El cliente no existe");
            }

            // Verificamos que se encuentre el seguro en la base de datos
            var seguroExistente = _context.Seguros
                .FirstOrDefault(x => x.Codigo == codigo);

            if (seguroExistente == null)
            {
                throw new System.Exception("El seguro no existe");
            }

            // Asegúrate de que la relación muchos a muchos esté configurada correctamente en tu modelo de datos
            // Agrega el seguro a la lista de seguros del cliente
            clienteExistente.Seguros.Add(seguroExistente);

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            return clienteExistente;
        }


        //Ingresi masivo de seguros via archivo csv
        public object IngresarClientesCsv(IFormFile file)
        {
            //Verificamos que el archivo no sea nulo
            if (file == null || file.Length == 0)
            {
                return new { mensaje = "No se ha seleccionado ningun archivo" };
            }

            //Verificamos que el archivo sea de tipo csv
            if (!file.FileName.EndsWith(".csv"))
            {
                return new { mensaje = "El archivo no es de tipo csv" };
            }


            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var aseguradosDto = csv.GetRecords<Cliente>().ToList();
                var asegurados = new List<Cliente>();

                // Filtra los aseguradosDto que no existen en la base de datos por su cédula y del propio csv
                var aseguradosNoDuplicados = aseguradosDto
                    .Where(aseguradoDto =>
                        !_context.Clientes.Any(a => a.Cedula == aseguradoDto.Cedula) &&
                        !aseguradosDto.Any(s => s != aseguradoDto && s.Cedula == aseguradoDto.Cedula))
                    .ToList();


                _context.Clientes.AddRange(aseguradosNoDuplicados);
                _context.SaveChanges();

                return aseguradosNoDuplicados;
            }


        }

        public object IngresarSegurosCsv(IFormFile file)
        {
            //Verificamos que el archivo no sea nulo
            if (file == null || file.Length == 0)
            {
                return new { mensaje = "No se ha seleccionado ningun archivo" };
            }

            //Verificamos que el archivo sea de tipo csv
            if (!file.FileName.EndsWith(".csv"))
            {
                return new { mensaje = "El archivo no es de tipo csv" };
            }


            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var segurosDto = csv.GetRecords<Seguro>().ToList();
                var seguros = new List<Seguro>();

                // Filtra los aseguradosDto que no existen en la base de datos por su cédula y del propio csv
                var segurosNoDuplicados = segurosDto
                        .Where(seguroDto => !_context.Seguros.Any(a => a.Codigo == seguroDto.Codigo) &&
                                            !segurosDto.Any(s => s != seguroDto && s.Codigo == seguroDto.Codigo))
                        .ToList();



                _context.Seguros.AddRange(segurosNoDuplicados);
                _context.SaveChanges();

                return segurosNoDuplicados;
            }
        }

    }

}
