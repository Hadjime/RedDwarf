﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;
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
                    Debug.LogError("Необходим один активный скрипт EvenManager на GameObject в сцене");
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
        
    public static void StopListening (string eventName, UnityAction listener)
    {
        if (_eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public static void TriggerEvent (string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke ();
        }
    }
}