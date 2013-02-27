using System;
using System.Data.Entity;
using System.Linq;
using Server.BL.Contracts;
using Server.DL.Contracts;
using System.Data.Entity.Migrations;
namespace Server.DL
{
    public class Repository<T> : IRepository<T> where T : class, IEntity 
    {
        protected DbSet<T> DbSet { get; set; }

        public Repository(DbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        } 

        public T GetById(int id)
        {
           return DbSet.Find(id);
        }

        public void Insert(T entity)
        {
            DbSet.AddOrUpdate(entity);
        }

        public void Update(T entity)
        {
            DbSet.AddOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        
        public IQueryable<T> Table
        {
           get{return DbSet;} 
        }
       
    }
}
