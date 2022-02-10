using System;
using UnityEngine;

namespace InternalAssets.Scripts.Characters.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack attack;
        [SerializeField] private TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEnter += OnObserverTriggerEnter;
            triggerObserver.TriggerExit += OnObserverTriggerExit;
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= OnObserverTriggerEnter;
            triggerObserver.TriggerExit -= OnObserverTriggerExit;
        }

        private void OnObserverTriggerEnter(Collider2D obj) => 
            attack.EnableAttack();

        private void OnObserverTriggerExit(Collider2D obj) => 
            attack.DisableAttack();
    }
}