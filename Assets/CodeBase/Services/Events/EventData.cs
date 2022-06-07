using System;

namespace CodeBase.Services.Events
{
    [Serializable]
    public class EventData
    {
        public string type;
        public string data;
    }
}