using System.Linq.Expressions;

namespace Entity.Data.Services
{
    public interface ICompanyService
    {
        Task<T> CreateEntity<T>(T request) where T : class;
        Task<IEnumerable<T>> GetAllEntities<T>() where T : class;
        Task<List<T>> UpdateEntity<T>(T entity) where T : class;
        Task<bool> DeleteEntity<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> GetEntityById<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> GetRelationalEntityById<T, S>(Expression<Func<T, bool>> predicate, Expression<Func<T, ICollection<S>>> expression) where T : class where S : class;
        Task<IEnumerable<T>> GetAllRelationalEntities<T, S>(Expression<Func<T, ICollection<S>>> expression) where T : class where S : class;

    }

}
