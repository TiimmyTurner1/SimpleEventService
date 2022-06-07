using System.Collections;
using System.Net;
using System.Text;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Services.Events
{
    public class EventService : MonoBehaviour, IEventService
    {
        private float _cooldownBeforeSend;
        private ISaveLoadEventsService _saveLoadEventsService;
        private EventsContainer _eventsContainer;
        private Coroutine _sendingCoroutine;

        public void Init(ISaveLoadEventsService saveLoadEventsService)
        {
            _saveLoadEventsService = saveLoadEventsService;
            _cooldownBeforeSend = Constants.SendRequestCooldown;
            _eventsContainer = _saveLoadEventsService.Load();
            _eventsContainer.events ??= new EventsList();

            if (_eventsContainer.sendingEvents?.events.Count > 0)
            {
                _eventsContainer.sendingEvents.events.AddRange(_eventsContainer.events.events);
                _eventsContainer.events = _eventsContainer.sendingEvents.events.ToEventsList();
                _eventsContainer.sendingEvents = null;
            }

            StartCoroutine(SendingRequestRoutine());
        }

        public void TrackEvent(string type, string data)
        {
            var eventData = new EventData {type = type, data = data};
            _eventsContainer.events.events.Add(eventData);
            _saveLoadEventsService.Save(_eventsContainer);
        }

        private IEnumerator SendingRequestRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => _sendingCoroutine == null && _eventsContainer.events.events.Count > 0);
                _sendingCoroutine = StartCoroutine(SendingEventsToServer());
                yield return new WaitForSeconds(_cooldownBeforeSend);
            }
        }

        private IEnumerator SendingEventsToServer()
        {
            var request = new UnityWebRequest(Constants.ServerURL, "POST");
            var bodyRaw = new UTF8Encoding().GetBytes(JsonUtility.ToJson(_eventsContainer.events));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            _eventsContainer.sendingEvents = _eventsContainer.events.events.ToEventsList();
            _eventsContainer.events.events.Clear();
            _saveLoadEventsService.Save(_eventsContainer);

            yield return request.SendWebRequest();

            if (request.responseCode != (int) HttpStatusCode.OK)
            {
                _eventsContainer.sendingEvents.events.AddRange(_eventsContainer.events.events);
                _eventsContainer.events = _eventsContainer.sendingEvents.events.ToEventsList();
                Debug.LogError($"Sending event failed with code: {request.responseCode}");
            }
            else
            {
                _eventsContainer.sendingEvents = null;
                _saveLoadEventsService.Save(_eventsContainer);
                Debug.Log("Sending event successful!");
            }

            _sendingCoroutine = null;
        }
    }
}