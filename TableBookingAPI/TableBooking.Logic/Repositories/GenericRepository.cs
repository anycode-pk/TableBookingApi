using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;

namespace TableBooking.Logic.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TableBookingContext _context;
        protected readonly DbSet<T> _objectSet;

        public GenericRepository(TableBookingContext context)
        {
            _context = context;
            _objectSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _objectSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _objectSet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
           await _objectSet.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            T objectToDelete = await _objectSet.FindAsync(id);
            _objectSet.Remove(objectToDelete);
        }

        public async Task Update(T entity)
        {
            var existingEntity = await _objectSet.FindAsync(GetKeyValues(entity));

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _objectSet.Update(entity);
        }
        
        private object[] GetKeyValues(T entity)
        {
            var keyProperties = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
            return keyProperties.Select(prop => prop.PropertyInfo.GetValue(entity)).ToArray();
        }
    }
}
