using CodeBase.Services.Events;
using UnityEngine;

namespace CodeBase.Services
{
    public class Services : MonoBehaviour
    {
        public IEventService EventService { get; private set; }
        
        private ISaveLoadEventsService _saveLoadEventsService;
        
        private void Awake()
        {
            CreateServices();
            InitializeServices();
        }

        private void CreateServices()
        {
            EventService = CreateEventService();
            _saveLoadEventsService = CreateSaveLoadEventsService();
        }

        private void InitializeServices()
        {
            EventService.Init(_saveLoadEventsService);
        }

        private IEventService CreateEventService() => 
            new GameObject(nameof(EventService)).AddComponent<EventService>();
        
        private ISaveLoadEventsService CreateSaveLoadEventsService() => 
            new GameObject(nameof(SaveLoadEventsService)).AddComponent<SaveLoadEventsService>();
    }
}