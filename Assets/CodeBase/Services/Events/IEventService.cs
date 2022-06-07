namespace CodeBase.Services.Events
{
    public interface IEventService
    {
        public void Init(ISaveLoadEventsService saveLoadEventsService);
        public void TrackEvent(string type, string data);
    }
}