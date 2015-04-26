using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.Windsor;
using Command;
using Domain;

namespace Ioc
{
    public class EventBus : IEventBus
    {
        private List<IDomainEvent> postCommitDomainEvents = new List<IDomainEvent>();
 
        private readonly IWindsorContainer windsorContainer;

        public EventBus(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        public void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is IAsyncPostCommitDomainEvent)
            {
                postCommitDomainEvents.Add(@event);
            }

            else
            {
                dispatch(@event);
            }
        }

        public void DispatchPostCommitEvents()
        {
            foreach (var postCommitDomainEvent in postCommitDomainEvents)
            {
                dispatch(postCommitDomainEvent);
            }
        }

        public void dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            foreach (var handler in windsorContainer.ResolveAll<IDomainEventHandler<TEvent>>())
            {
                handler.Handle(eventToDispatch);
            }
        }
    }
}
