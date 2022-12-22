using Company.Data.DataAccess;
using Entity.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Company.Data.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _dataContext;
        public CompanyService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> CreateEntity<T>(T entity)
            where T : class
        {
            try
            {
                var result = await _dataContext.Set<T>().AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<IEnumerable<T>> GetAllEntities<T>()
            where T : class
        {
            var result = await _dataContext.Set<T>().ToListAsync();
            return result;
        }
        public async Task<List<T>> UpdateEntity<T>(T entity)
            where T : class
        {
            _dataContext.Set<T>().Update(entity);
            await _dataContext.SaveChangesAsync();
            var result = await _dataContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<bool> DeleteEntity<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            try
            {
                var entityToRemove = await _dataContext.Set<T>().FirstOrDefaultAsync(predicate);

                _dataContext.Set<T>().Remove(entityToRemove);
                var result = await _dataContext.SaveChangesAsync();
                if (result == -1) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<T> GetEntityById<T>(Expression<Func<T, bool>> predicate)
             where T : class
        {
            try
            {
                var result = await _dataContext.Set<T>().AsQueryable().FirstOrDefaultAsync(predicate);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<T> GetRelationalEntityById<T, S>(Expression<Func<T, bool>> predicate, Expression<Func<T, ICollection<S>>> expression)
            where T : class
            where S : class
        {
            var result = await _dataContext.Set<T>().AsQueryable().Where(predicate).Include(expression).FirstOrDefaultAsync();
            return result;
        }


        public async Task<IEnumerable<T>> GetAllRelationalEntities<T, S>(Expression<Func<T, ICollection<S>>> expression)
            where T : class
            where S : class
        {
            var result = await _dataContext.Set<T>().AsQueryable().Include(expression).ToListAsync();
            return result;
        }
    }
}
