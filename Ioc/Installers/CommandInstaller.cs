using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Command;
using Ioc.Intercepters;

namespace Ioc.Installers
{
    public class CommandInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IInterceptor>()
                    .ImplementedBy<TransactionHandlingIntercepter>().LifestyleTransient(),
                Classes
                    .FromAssemblyContaining<CreateUser>()
                    .Where(t => t.Name.EndsWith("CommandHandler"))
                    .LifestylePerThread()
                    .WithServiceAllInterfaces().Configure(c => c.Interceptors<TransactionHandlingIntercepter>())
                );
        }
    }
}
