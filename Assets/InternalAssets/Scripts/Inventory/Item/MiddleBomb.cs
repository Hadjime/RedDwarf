using UnityEngine;


namespace InternalAssets.Scripts.Inventory.Item
{
	public class MiddleBomb : MonoBehaviour
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
				Instantiate(cellExplosion, transform.position, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.down, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.down + Vector3.left, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.down + Vector3.right, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.down * 2, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.up, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.up + Vector3.left, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.up + Vector3.right, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.up * 2, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.left, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.left * 2, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.right, Quaternion.identity);
				Instantiate(cellExplosion, transform.position + Vector3.right * 2, Quaternion.identity);
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
