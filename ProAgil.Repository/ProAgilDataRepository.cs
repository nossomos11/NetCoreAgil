using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilDataRepository : IProAgilDataRepository
    {
        public ProAgilDataContext DataContext { get; }

        public ProAgilDataRepository(ProAgilDataContext dataContext)
        {
            DataContext = dataContext;
            DataContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            DataContext.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            DataContext.Update(entity);
        }


        public void Delete<T>(T entity) where T : class
        {
            DataContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await DataContext.SaveChangesAsync() > 0;
        }

    }
}