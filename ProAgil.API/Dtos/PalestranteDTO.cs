using System.Collections.Generic;

namespace ProAgil.API.Dtos
{
    public class PalestranteDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Miniurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }    
    }
}