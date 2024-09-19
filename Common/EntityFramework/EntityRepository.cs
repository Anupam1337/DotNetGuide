using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly EFContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(EFContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);

        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);
    }

}
