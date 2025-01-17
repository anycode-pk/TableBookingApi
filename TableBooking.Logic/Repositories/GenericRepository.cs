namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TableBookingContext _context;
    protected readonly DbSet<T> ObjectSet;

    protected GenericRepository(TableBookingContext context)
    {
        _context = context;
        ObjectSet = _context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await ObjectSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(object id)
    {
        var entity = await ObjectSet.FindAsync(id);
            
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} was not found.");
        }

        return entity;
    }

    public async Task InsertAsync(T entity)
    {
        await ObjectSet.AddAsync(entity);
    }

    public async Task Delete(object id)
    {
        var objectToDelete = await ObjectSet.FindAsync(id);
            
        if (objectToDelete == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} was not found.");
        }
            
        ObjectSet.Remove(objectToDelete);
    }

    public async Task Update(T entity)
    {
        var existingEntity = await ObjectSet.FindAsync(GetKeyValues(entity));

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        ObjectSet.Update(entity);
    }
        
    private object[] GetKeyValues(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

        var entityType = _context.Model.FindEntityType(typeof(T));
        if (entityType == null)
            throw new InvalidOperationException($"Entity type {typeof(T).Name} is not found in the EF model.");

        var primaryKey = entityType.FindPrimaryKey();
        if (primaryKey == null)
            throw new InvalidOperationException($"Primary key is not defined for entity type {typeof(T).Name}.");

        return primaryKey.Properties
            .Select(prop =>
            {
                if (prop.PropertyInfo == null)
                    throw new InvalidOperationException($"PropertyInfo is null for property {prop.Name} on entity type {typeof(T).Name}.");

                var value = prop.PropertyInfo.GetValue(entity);
                if (value == null)
                    throw new InvalidOperationException($"Primary key property {prop.Name} on entity type {typeof(T).Name} has a null value.");

                return value;
            })
            .ToArray();
    }
}