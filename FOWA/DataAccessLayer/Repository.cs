using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOWA.BusinessLayer.Contracts;
using FOWA.DataAccessLayer.Contracts;

namespace FOWA.DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Table
        {
            get { throw new NotImplementedException(); }
        }
    }
}
