using Microsoft.EntityFrameworkCore;

namespace Common.EntityFramework
{
    public class EFManager
    {
        private readonly EFContext _context;

        public EFManager(EFContext context)
        {
            _context = context;
        }

        public async Task<int> AddEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> FindEntity<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> DeleteEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
