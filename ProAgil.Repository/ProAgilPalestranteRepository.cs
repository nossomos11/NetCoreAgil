using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilPalestranteRepository : ProAgilDataRepository
    {
        public ProAgilPalestranteRepository(ProAgilDataContext dataContext) : base(dataContext)
        {

        }



        public async Task<Palestrante> Get(bool incluirEventos = false, int palestranteId = 0){
            return (await GetAllAsync(incluirEventos, palestranteId)).FirstOrDefault();
        }

        public async Task<Palestrante[]> GetAllAsync(bool incluirEventos = false, string nome = null){
            return await GetAllAsync(incluirEventos, 0, nome);
        }
        private async Task<Palestrante[]> GetAllAsync(bool incluirEventos = false, int palestranteId = 0, string nome = null){
            IQueryable<Palestrante> query = DataContext.Palestrantes
                .Include(r => r.RedeSociais);

                if(incluirEventos){
                    query = query.Include(pe => pe.PalestrantesEventos)
                        .ThenInclude(p => p.Evento);
                }

                if(palestranteId > 0){
                    query = query.Where(p => p.ID == palestranteId);
                }

                if(!String.IsNullOrEmpty(nome)){
                    query = query.Where( p => p.Nome.ToLower().Contains(nome.ToLower()));
                }

                query = query.OrderBy(p => p.Nome);

                return await query.ToArrayAsync();
        }
    }
    
}