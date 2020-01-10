using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilDataContext : DbContext
    {
        public ProAgilDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PalestranteEvento>()
                .HasKey( pe => new { pe.PalestranteID, pe.EventoID });
        }
    }
}