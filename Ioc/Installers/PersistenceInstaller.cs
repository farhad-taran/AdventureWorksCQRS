using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Persistence;

namespace Ioc.Installers
{
    public class PersistenceInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(ICommandRepository<>)).ImplementedBy(typeof(CommandRepository<>)));


            Func<IUnitOfWork> uowFactory = () => new UnitOfWork();

            // Register the factory as Lazy<IUnitOfWork>
            //container.Register<Lazy<IUnitOfWork>>(
            //    () => new Lazy<IUnitOfWork>(uowFactory),
            //    new WebRequestLifestyle());

            container.Register(Component.For(typeof(Lazy<IUnitOfWork>)).UsingFactoryMethod(() => new Lazy<IUnitOfWork>(uowFactory)));


            // Create a registration that redirects to Lazy<IUnitOfWork>
            //container.Register<IUnitOfWork>(
            //    () => container.GetInstance<Lazy<IUnitOfWork>>().Value,
            //    new WebRequestLifestyle());

            container.Register(
                Component.For(typeof (IUnitOfWork)).UsingFactoryMethod(() => container.Resolve<Lazy<IUnitOfWork>>().Value));

            //container.Register(Component.For(typeof (IUnitOfWork)).ImplementedBy(typeof (UnitOfWork)));
        }
    }
}
