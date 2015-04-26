using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private AdventureWorks context = new AdventureWorks();
        private CommandRepository<Department> departmentCommandRepository;

        public CommandRepository<Department> DepartmentCommandRepository
        {
            get
            {

                if (this.departmentCommandRepository == null)
                {
                    this.departmentCommandRepository = new CommandRepository<Department>(context);
                }
                return departmentCommandRepository;
            }
        }

       
        public void Commit()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
