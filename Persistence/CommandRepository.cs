using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class CommandRepository<T>:ICommandRepository<T> where T : class
    {
        private DbContext dbContext;
        private DbSet<T> dbSet;
 
        public CommandRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
