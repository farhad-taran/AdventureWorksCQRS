using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Command
{
    public interface IEventBus
    {
        void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent;
        void DispatchPostCommitEvents();
        void dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;
    }
}
