namespace CodeBase.Services.Events
{
    public interface ISaveLoadEventsService
    {
        public void Save(EventsContainer events);
        public EventsContainer Load();
    }
}