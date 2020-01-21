using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilEventoRepository : ProAgilDataRepository
    {
        public ProAgilEventoRepository(ProAgilDataContext dataContext) : base(dataContext)
        {

        }

        public async Task<Evento> Get(bool incluirPalestrantes = false, int eventoId = 0){
            return (await GetAllAsync(incluirPalestrantes, eventoId)).FirstOrDefault();
        }

        public async Task<Evento[]> GetAllAsync(bool incluirPalestrantes = false, string tema = null){
            return await GetAllAsync(incluirPalestrantes, 0, tema);
        }

        private async Task<Evento[]> GetAllAsync(bool incluirPalestrantes = false, int eventoId = 0, string tema = null){
            IQueryable<Evento> query = DataContext.Eventos
                .Include(l => l.Lotes)
                .Include(p => p.RedeSociais);

                if(incluirPalestrantes){
                    query = query.Include(pe => pe.PalestrantesEventos)
                        .ThenInclude(p => p.Palestrante);
                }

                if(eventoId > 0){
                    query = query.Where( e => e.ID == eventoId);
                }

                if(!String.IsNullOrEmpty(tema)){
                    query = query.Where( e => e.Tema.ToLower().Contains(tema.ToLower()));
                }

                query = query.OrderByDescending(e => e.ID);

                return await query.ToArrayAsync();
        }
    }
    
}