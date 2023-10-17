

using System.ComponentModel.DataAnnotations;

namespace AplicacionApi.Modelos
{
    public class Seguro
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public float SumaAsegurada { get; set; }
        
        public float Prima { get; set; }

    }
}
