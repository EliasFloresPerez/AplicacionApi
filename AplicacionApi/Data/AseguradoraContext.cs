
using Microsoft.EntityFrameworkCore;
using AplicacionApi.Modelos;

namespace AplicacionApi.Data
{
    public class AseguradoraContext : DbContext
    {

        public AseguradoraContext(DbContextOptions<AseguradoraContext> options) : base(options)
        {

        }

        // DbSet para cada modelo creado en Modelos
        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<Seguro> Seguros => Set<Seguro>();

    }
}
