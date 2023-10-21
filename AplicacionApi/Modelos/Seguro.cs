

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AplicacionApi.Modelos
{
    public class Seguro
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public float SumaAsegurada { get; set; }
        
        public float Prima { get; set; }


        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
