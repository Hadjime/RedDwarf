using System;
using UnityEngine;


namespace InternalAssets.Scripts.Characters
{
	public class CharactersAnimator : MonoBehaviour
	{
		private static readonly int IsDiggingHash = Animator.StringToHash("isDigging");
		private static readonly int IsRunHash = Animator.StringToHash("isRun");
		[SerializeField] private Animator _animator;

		private void Awake()
		{
			if (_animator != null)
				return;
			
			if (TryGetComponent(out Animator animator))
				_animator = animator;
		}

		public void PlayDiggingAnimation()
		{
			_animator.SetBool(IsDiggingHash, true);
			_animator.SetBool(IsRunHash, false);
		}

		public void PlayRunAnimation()
		{
			_animator.SetBool(IsDiggingHash, false);
			_animator.SetBool(IsRunHash, true);
		}

		public void SetActiveDiggingAnimation(bool isState) =>
			_animator.SetBool(IsDiggingHash, isState);

		public void SetActiveRunAnimation(bool isState) =>
			_animator.SetBool(IsRunHash, isState);
	}
}
