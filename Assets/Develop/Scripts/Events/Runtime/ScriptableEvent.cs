using System.Collections.Generic;
using UnityEngine;

namespace Events.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Event", fileName ="New event")]
    public class ScriptableEvent : ScriptableObject
    {
        #region Properties

        private List<EventListener> Listeners
        {
            get
            {
                if(_listeners == null)
                {
                    _listeners = new List<EventListener>();
                }

                return _listeners;
            }

            set
            {
                _listeners = value;
            }
        }

        #endregion


        #region Main

        public void Raise()
        {
            foreach(var listener in Listeners)
            {
                listener.EventRaised();
            }
        }

        public void Subscribe(EventListener listener)
        {
            Listeners.Add(listener);
        }

        public void Unsubscribe(EventListener listener)
        {
            Listeners.Remove(listener);
        }

        #endregion


        #region Private

        private List<EventListener> _listeners;

        #endregion
    }
}