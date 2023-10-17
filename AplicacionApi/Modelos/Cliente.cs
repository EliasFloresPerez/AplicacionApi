
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionApi.Modelos
{
    public class Cliente
    {
        [Key]
        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public int Edad { get; set; }

        // Lista de seguros asociados al cliente
        public List<Seguro> Seguros { get; set; }


    }
}
