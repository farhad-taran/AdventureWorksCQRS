using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Ioc.Installers;

namespace Ioc
{
    public class Container
    {
        public static void Bootstrap(IWindsorContainer container)
        {
            container.Install(new PersistenceInstaller(), new EventingInstaller(),new CommandInstaller());
        }
    }
}
