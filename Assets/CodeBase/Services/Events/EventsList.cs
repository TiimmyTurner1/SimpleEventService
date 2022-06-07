using System;
using System.Collections.Generic;

namespace CodeBase.Services.Events
{
    [Serializable]
    public class EventsList
    {
        public List<EventData> events = new List<EventData>();
    }
}