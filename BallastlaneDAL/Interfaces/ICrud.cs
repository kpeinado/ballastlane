using System.Linq.Expressions;

namespace BallastlaneDAL.Interfaces
{
    public interface ICrud<T>
    {
        bool Create(T entity);
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
