using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class EventoDTO
    {
        public int ID { get; set; }
        [Required]
        public string Local { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataEvento { get; set; }
        [Required]
        public string Tema { get; set; }
        [Required]
        public int QtdPessoas { get; set; }
        [Required]
        public string ImagemURL { get; set; }
        [Required]
        [StringLength (15, MinimumLength=14)]
        public string Telefone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }   
    }
}

