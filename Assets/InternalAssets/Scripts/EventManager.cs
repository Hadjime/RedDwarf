using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InternalAssets.Scripts
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> eventDictionary;
        private Dictionary<string, UnityEventWithOneParametr> eventDictionaryWithOneParametr;
        private static EventManager _eventManager;

        public static EventManager instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                    if (!_eventManager)
                    {
                        // Debug.LogError("Необходим один активный скрипт EventManager на GameObject в сцене");
                    }
                    else
                    {
                        _eventManager.Init();
                    }
                }

                return _eventManager;
            }
        }

        private void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UnityEvent>();
                eventDictionaryWithOneParametr = new Dictionary<string, UnityEventWithOneParametr>();
            }
        }
        public static void StartListening (string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.AddListener (listener);
            } 
            else
            {
                thisEvent = new UnityEvent ();
                thisEvent.AddListener (listener);
                instance.eventDictionary.Add (eventName, thisEvent);
            }
        }
        public static void StartListeningWithOneParametr (string eventName, UnityAction<int> listener)
        {
            UnityEventWithOneParametr thisEvent = null;
            if (instance.eventDictionaryWithOneParametr.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.AddListener (listener);
            } 
            else
            {
                thisEvent = new UnityEventWithOneParametr();
                thisEvent.AddListener (listener);
                instance.eventDictionaryWithOneParametr.Add (eventName, thisEvent);
            }
        }
        
        public static void StopListening (string eventName, UnityAction listener)
        {
            if (_eventManager == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.RemoveListener (listener);
            }
        }
        public static void StopListeningWithOneParametr (string eventName, UnityAction<int> listener)
        {
            if (_eventManager == null) return;
            UnityEventWithOneParametr thisEvent = null;
            if (instance.eventDictionaryWithOneParametr.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.RemoveListener (listener);
            }
        }

        public static void StartEvent (string eventName)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.Invoke ();
            }
        }
        public static void TriggerEventWithOneParametr (string eventName, int param)
        {
            UnityEventWithOneParametr thisEvent = null;
            if (instance.eventDictionaryWithOneParametr.TryGetValue (eventName, out thisEvent))
            {
                thisEvent.Invoke (param);
            }
        }
    }

    public class UnityEventWithOneParametr : UnityEvent<int>
    {
    }
}