using System;
using System.Diagnostics.Contracts;
using InternalAssets.Scripts.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace InternalAssets.Scripts.Weapon
{
    public class CellExplosion : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private int delay;
        [SerializeField, Range(0, 100)] private int damage;

        void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var objectTile = other.GetComponent<TileSetting>();
            if (other != null && objectTile != null)
            {
                objectTile.DamageTile(damage);
            }
        }

        public void DestroyObject()
        {
            Destroy(this.gameObject);
        }
    }
}
