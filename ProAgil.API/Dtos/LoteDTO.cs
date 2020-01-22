using System;

namespace ProAgil.API.Dtos
{
    public class LoteDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }
    }
}