using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Command;
using Persistence;

namespace Ioc.Intercepters
{
    public class TransactionHandlingIntercepter:IInterceptor
    {
        private static readonly MethodInfo Handle = 
        typeof(ICommandHandler<>).GetMethod("Handle");

        private static readonly MethodInfo Commit =
        typeof(IUnitOfWork).GetMethod("Handle");

        private readonly IKernel kernel;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public TransactionHandlingIntercepter(IKernel kernel,Lazy<IUnitOfWork> lazyUnitOfWork)
        {
            this.kernel = kernel;
            _lazyUnitOfWork = lazyUnitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.Equals(Handle.Name))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                invocation.Proceed();
            }
            finally
            {
                kernel.ReleaseComponent(invocation.Proxy);
            }
        }
    }
}
