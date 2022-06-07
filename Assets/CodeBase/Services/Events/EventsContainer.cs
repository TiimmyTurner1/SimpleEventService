using System;

namespace CodeBase.Services.Events
{
    [Serializable]
    public class EventsContainer
    {
        public EventsList events;
        public EventsList sendingEvents;
    }
}