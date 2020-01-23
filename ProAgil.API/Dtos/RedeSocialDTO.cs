using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class RedeSocialDTO
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
