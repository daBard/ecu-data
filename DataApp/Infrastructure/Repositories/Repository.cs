using Helper;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace Infrastructure.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    private readonly LocalUserDataContext _context;

    protected Repository(LocalUserDataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a new row in db table
    /// </summary>
    /// <param name="entity">Flexible param based on topical entity</param>
    /// <returns>Created entity if successful, else null</returns>
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch(Exception ex) { LogError(ex.ToString()); }
        return null!;
    }

    /// <summary>
    /// Get all rows from db table
    /// </summary>
    /// <returns>IEnumerable of topical entity if successful, else null</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllFromGuidAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entities = await _context.Set<TEntity>().Where(expression).ToListAsync();
            return entities;
        }
        catch(Exception ex) { LogError(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Get one row from db table
    /// </summary>
    /// <param name="expression">One unique param from topical entity</param>
    /// <returns>One topical entity if successful, else null</returns>
    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression) 
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Update one row in db table
    /// </summary>
    /// <param name="entity">One entity of topical entity</param>
    /// <returns>Updated entity if successful, else null</returns>
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var entityToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            _context.Entry<TEntity>(entityToUpdate!).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entityToUpdate!;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Delete one row in db table
    /// </summary>
    /// <param name="expression"></param>
    /// <returns>True if entity is successfully removed, else false</returns>
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    public virtual async Task<TEntity> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entity != null)
                return entity;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Sends classname and error message to ErrorLogger in Helper
    /// </summary>
    /// <param name="errorMessage">String value for logging of error</param>
    private void LogError(string errorMessage)
    {
        string className = this.ToString() ?? "Unknown class";
        ErrorLogger errorLogger = new ErrorLogger();
        errorLogger.Logger(className, errorMessage);
    }
}
