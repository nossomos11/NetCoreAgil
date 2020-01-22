using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class PalestranteDTO
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Miniurriculo { get; set; }
        public string ImagemURL { get; set; }
        [Required]
        [StringLength (15, MinimumLength=14)]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }    
    }
}