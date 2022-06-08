using System;

namespace CodeBase.Services.Events
{
    [Serializable]
    public class EventsContainer
    {
        public EventsList storedEvents;
        public EventsList sendingEvents;

        public void ReturnSendingEvents()
        {
            if (sendingEvents?.events.Count > 0)
            {
                sendingEvents.events.AddRange(storedEvents.events);
                storedEvents = sendingEvents;
                sendingEvents = null;
            }
        }
    }
}