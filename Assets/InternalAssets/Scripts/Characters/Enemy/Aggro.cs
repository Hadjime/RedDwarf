using System.Collections;
using InternalAssets.Scripts.Utils;
using UnityEngine;

namespace InternalAssets.Scripts.Characters.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private Seeker follow;
        [SerializeField] private float cooldown = 1;

        private Coroutine aggroCoroutine;
        private bool hasAggroTarget = default;

        private void Start()
        {
            triggerObserver.TriggerEnter += OnObserverTriggerEnter;
            triggerObserver.TriggerExit += OnObserverTriggerExit;

            FollowOff();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= OnObserverTriggerEnter;
            triggerObserver.TriggerExit -= OnObserverTriggerExit;
        }

        private void OnObserverTriggerEnter(Collider2D obj)
        {
            if (hasAggroTarget) 
                return;
            
            hasAggroTarget = true;
            StopAggroCoroutine();
            FollowOn();
        }

        private void OnObserverTriggerExit(Collider2D obj)
        {
            if (!hasAggroTarget) 
                return;
            
            hasAggroTarget = false;
            aggroCoroutine = StartCoroutine(FollowOffAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (aggroCoroutine == null) 
                return;
            
            StopCoroutine(aggroCoroutine);
            aggroCoroutine = null;
        }

        private IEnumerator FollowOffAfterCooldown()
        {
            yield return Coroutines.GetWait(cooldown);
            FollowOff();
        }

        private void FollowOn()
        {
            hasAggroTarget = true;
            follow.enabled = true;
        }

        private void FollowOff()
        {
            hasAggroTarget = false;
            follow.enabled = false;
        }
    }
}