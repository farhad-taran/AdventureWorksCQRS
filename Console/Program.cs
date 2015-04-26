using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Windsor;
using Command;
using Ioc;

namespace Console
{
    class Program
    {
        private static IWindsorContainer windsorContainer = new WindsorContainer();
        static void Main(string[] args)
        {
            Action dosomeaction = () => Debug.Write("dosomeaction");
            
            Container.Bootstrap(windsorContainer);

            var command = windsorContainer.Resolve<ICommandHandler<CreateUser>>();
            command.Handle(new CreateUser());

            var delete = windsorContainer.Resolve<ICommandHandler<DeleteUser>>();
            delete.Handle(new DeleteUser());

            dosomeaction();


        }
    }
}
