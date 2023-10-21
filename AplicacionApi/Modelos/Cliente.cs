
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<Seguro> Seguros{ get; set; }


    }
}
