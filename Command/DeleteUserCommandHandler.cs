using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;

namespace Command
{
    public class DeleteUserCommandHandler:ICommandHandler<DeleteUser>
    {
        private IEventBus eventBus;
        public DeleteUserCommandHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Handle(DeleteUser command)
        {
            eventBus.Raise(new UserDeleted());
        }
    }

    public class DeleteUser { }
}
