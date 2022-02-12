using System;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer enemySpriteRenderer;
		[SerializeField] private EnemyHealth enemyHealth;
		[SerializeField] private AgentMoveToPlayer agentMoveToPlayer;
		[SerializeField] private Attack enemyAttack;
		[SerializeField] private Aggro enemyAggro;
		[SerializeField] private GameObject redStainPrefab;

		public event Action Happened;


		private void Start() =>
			enemyHealth.HpChanged += OnHpChanged;


		private void OnDestroy() =>
			enemyHealth.HpChanged -= OnHpChanged;


		private void OnHpChanged()
		{
			if (enemyHealth.CurrentHp <= 0)
				Die();
		}


		private void Die()
		{
			//TODO проиграть анимацию смерти
			enemyHealth.HpChanged -= OnHpChanged;
			
			enemySpriteRenderer.enabled = false;
			agentMoveToPlayer.enabled = false;
			enemyAggro.enabled = false;
			enemyAttack.enabled = false;
			
			SpawnDeathFx();
			Happened?.Invoke();
			Destroy(gameObject);
		}


		private void SpawnDeathFx() =>
			Instantiate(redStainPrefab, transform.position, Quaternion.identity);
	}
}
