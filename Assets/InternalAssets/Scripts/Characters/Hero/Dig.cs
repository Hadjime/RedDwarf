using System;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
	public class Dig : MonoBehaviour
	{
		[SerializeField] private HeroMove heroMove;
		[SerializeField] private float TimeoutAttack = 1f;
		[Range(0.1f, 50f)] [SerializeField] private int handAttack = 20;
		[SerializeField] private bool isAttack;
		private HeroAnimator _animator;
		private LayerMask _maskForAttack;
		private RaycastHit2D[] _poolRaycast = new RaycastHit2D[10];
		private float _timer = 0;

		private void Awake()
		{
			_maskForAttack = LayerMask.GetMask("RockAndOther");
			_animator = GetComponent<HeroAnimator>();
		}

		private void Update()
		{
			if (heroMove.IsMoving)
			{
				isAttack = false;
				_animator.SetActiveDiggingAnimation(false);
				return;
			}
				

			isAttack = true;
			_animator.PlayDiggingAnimation();
			DigAttack(heroMove.CurrentNormalizeDirection);
		}

		private void DigAttack(Vector2 attackPoint)
		{
			if (_timer >= TimeoutAttack)
			{
				_timer = 0;
				Vector3 position = transform.position;
				int count = Physics2D.RaycastNonAlloc(position, attackPoint, _poolRaycast, 1, _maskForAttack);
				Debug.DrawRay(position, attackPoint, Color.red);
				if (count > 0)
				{
					RaycastHit2D hit = _poolRaycast[0];
					if (1 << hit.collider.gameObject.layer == _maskForAttack.value)
						hit.collider.GetComponent<IHealth>()?.ApplyDamage(handAttack);
				}
			}
			
			_timer += Time.deltaTime;
		}
	}
}
