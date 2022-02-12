using System;
using Unity.Mathematics;
using UnityEngine;

namespace InternalAssets.Scripts.Inventory.Item
{
    public class SmallBomb : MonoBehaviour
    {
		private const string LayerName = "Hittable";
		
		
        [SerializeField, Range(0, 10)] private int delay;
        [SerializeField] private GameObject cellExplosion;
        private float _timeRemaining;
        private bool once;
		private int _layerMask;
		private Collider2D[] _hits = new Collider2D[30];


		private void Start()
		{
			_layerMask = 1 << LayerMask.NameToLayer(LayerName);
            _timeRemaining = delay;
        }
        private void Update()
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0)
            {
                BabahOnce();
            }
        }

        public void BabahOnce()
        {
            if (!once)
            {
                Instantiate(cellExplosion, transform.position, quaternion.identity);
                Instantiate(cellExplosion, transform.position + Vector3.down, quaternion.identity);
                Instantiate(cellExplosion, transform.position + Vector3.up, quaternion.identity);
                Instantiate(cellExplosion, transform.position + Vector3.left, quaternion.identity);
                Instantiate(cellExplosion, transform.position + Vector3.right, quaternion.identity);
                Destroy(this.gameObject);
                once = true;
            }
        }


		private void Hit(Vector2 position)
		{
			Physics2D.OverlapBoxNonAlloc(position, new Vector2(0.9f, 0.9f), 0f, _hits, _layerMask);
		}
	}
}