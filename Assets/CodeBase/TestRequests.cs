using UnityEngine;

namespace CodeBase
{
    public class TestRequests : MonoBehaviour
    {
        [SerializeField] private Services.Services services;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                services.EventService.TrackEvent("KeyDown", "Space");
            }
        }
    }
}