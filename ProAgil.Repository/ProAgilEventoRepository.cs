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

        public async Task<Evento[]> GetAllAsync(bool incluirPalestrantes = false){
            return await GetAllAsync(incluirPalestrantes, null);
        }

        public async Task<Evento[]> GetAllAsync(bool incluirPalestrantes = false, string tema = null){
            IQueryable<Evento> query = DataContext.Eventos
                .Include(l => l.Lotes)
                .Include(p => p.RedeSociais);

                if(incluirPalestrantes){
                    query = query.Include(pe => pe.PalestrantesEventos)
                        .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(e => e.DataEvento);

                if(!String.IsNullOrEmpty(tema)){
                    query = query.Where( e => e.Tema.Contains(tema));
                }

                return await query.ToArrayAsync();
        }

    }
    
}