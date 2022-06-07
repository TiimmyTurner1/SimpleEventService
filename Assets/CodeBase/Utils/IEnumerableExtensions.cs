using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Events;

namespace CodeBase.Utils
{
    public static class EnumerableExtensions
    {
        public static EventsList ToEventsList(this IEnumerable<EventData> collection)
        {
            var result = new EventsList
            {
                events = collection.ToList()
            };
            
            return result;
        }
    }
}