using System.Collections.Generic;
using UnityEngine;

namespace Events.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Event", fileName ="New event")]
    public class ScriptableEvent : ScriptableObject
    {
        #region Unity API

        private void OnEnable() 
        {
            _listeners = new List<EventListener>();    
        }

        #endregion


        #region Main

        public void Raise()
        {
            foreach(var listener in _listeners)
            {
                listener.EventRaised();
            }
        }

        public void Subscribe(EventListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unsubscribe(EventListener listener)
        {
            _listeners.Remove(listener);
        }

        #endregion


        #region Private

        private List<EventListener> _listeners;

        #endregion
    }
}