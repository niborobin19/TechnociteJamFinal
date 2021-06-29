using UnityEngine;
using UnityEngine.Events;

namespace Events.Runtime
{
    public class EventListener : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private ScriptableEvent _event;

        [SerializeField]
        private UnityEvent _onEventRaised;

        #endregion


        #region Unity API

        private void Awake() 
        {
            _event.Subscribe(this);
        }

        private void OnDestroy() 
        {
            _event.Unsubscribe(this);    
        }

        #endregion


        #region Main

        public void EventRaised()
        {
            _onEventRaised.Invoke();
        }

        #endregion


        #region Private

        #endregion
    }
}