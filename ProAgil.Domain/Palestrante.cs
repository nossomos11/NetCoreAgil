using System;
using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Palestrante
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Miniurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocial> RedeSociais { get; set; }
        public List<PalestranteEvento> PalestrantesEventos { get; set; }       

    }
}