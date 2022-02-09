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
            triggerObserver.TriggerEnter += OnTriggerEnter;
            triggerObserver.TriggerExit += OnTriggerExit;
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= OnTriggerEnter;
            triggerObserver.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerEnter(Collider2D obj) => 
            attack.EnableAttack();

        private void OnTriggerExit(Collider2D obj) => 
            attack.DisableAttack();
    }
}