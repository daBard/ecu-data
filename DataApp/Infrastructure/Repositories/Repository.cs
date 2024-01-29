using Helper;
using Infrastructure.Contexts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace Infrastructure.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    private readonly LocalDataContext _context;

    protected Repository(LocalDataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a new row in db table
    /// </summary>
    /// <param name="entity">Flexible param based on topical entity</param>
    /// <returns>Created entity if successful, else null</returns>
    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch(Exception ex) { LogError(ex.ToString()); }
        return null!;
    }

    /// <summary>
    /// Get all rows from db table
    /// </summary>
    /// <returns>IEnumerable of topical entity if successful, else null</returns>
    public virtual IEnumerable<TEntity> GetAll()
    {
        try
        {
            var result = _context.Set<TEntity>().ToList();
            return result;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    public virtual IEnumerable<TEntity> GetAllFromGuid(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entities = _context.Set<TEntity>().Where(expression).ToList();
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
    public virtual TEntity GetOne(Expression<Func<TEntity, bool>> expression) 
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
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
    public virtual TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
            _context.Entry<TEntity>(entityToUpdate!).CurrentValues.SetValues(entity);
            _context.SaveChanges();

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
    public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            return true;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    public virtual TEntity Exists(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(expression);
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
