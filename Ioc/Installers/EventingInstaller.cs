using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Command;
using Domain;

namespace Ioc.Installers
{
    public class EventingInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof (IDomainEventHandler<>)).ImplementedBy(typeof (IDomainEventHandler<>)));
            container.Register(
                Component.For(typeof(IEventBus)).UsingFactoryMethod(()=> new EventBus(container)));
        }
    }
}
