using System;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class LoteDTO
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Range(1, 99999)]
        public Decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        [Required]
        [Range(1, 1000)]
        public int Quantidade { get; set; }
    }
}