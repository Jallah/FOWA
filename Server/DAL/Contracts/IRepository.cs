using System.Linq;
using Server.BL.Contracts;

namespace Server.DL.Contracts
{
    interface IRepository<T> where T : IEntity
    {
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
    }
}
