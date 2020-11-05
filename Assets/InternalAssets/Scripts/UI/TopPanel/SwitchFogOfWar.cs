using System;
using System.Diagnostics;
using UnityEngine;

namespace InternalAssets.Scripts.UI.Event
{
    public class SwitchFogOfWar : MonoBehaviour
    {
        [SerializeField] private bool isFogOfWar;
        private Transform instanceFogOfWar;

        private void Awake()
        {
            instanceFogOfWar = transform.Find("FogOfWar");
            isFogOfWar = instanceFogOfWar.gameObject.activeInHierarchy;
        }

        private void OnEnable()
        {
            EventManager.StartListening("SwitchIsFogOfWar", HandleSwitch);
        }

        private void OnDisable()
        {
            EventManager.StopListening("SwitchIsFogOfWar", HandleSwitch);
        }

        private void HandleSwitch()
        {
            isFogOfWar = !isFogOfWar;
            instanceFogOfWar.gameObject.SetActive(isFogOfWar);
        }
    }
}