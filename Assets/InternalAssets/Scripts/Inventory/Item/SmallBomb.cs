using System;
using UnityEngine;

namespace InternalAssets.Scripts.Inventory.Item
{
    public class SmallBomb : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private int delay;
        private float timeRemaining;
        
        private void Start()
        {
            timeRemaining = delay;
        }
        private void Update()
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                Babah();
            }
        }

        private void Babah()
        {
            throw new NotImplementedException();
        }
    }
}