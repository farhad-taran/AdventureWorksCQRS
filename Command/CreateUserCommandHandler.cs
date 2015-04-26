using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Persistence;

namespace Command
{
    public class CreateUserCommandHandler:ICommandHandler<CreateUser>
    {
        private IUnitOfWork unitOfWork;
        private IEventBus eventBus;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            this.unitOfWork = unitOfWork;
            this.eventBus = eventBus;
        }

        public void Handle(CreateUser command)
        {
            Debug.Write("command handled");
            eventBus.Raise(new UserCreated());
        }
    }

    public class CreateUser
    {
    }
}
