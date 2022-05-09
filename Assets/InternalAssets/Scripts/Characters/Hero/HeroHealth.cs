using System;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
	{
		[SerializeField] private State _playerState;

		public event Action HpChanged;

        public float CurrentHp
        {
            get => _playerState.CurrentHp;
			set
            {
                if (Mathf.Approximately(_playerState.CurrentHp, value))
                    return;
				
                _playerState.CurrentHp = value;
                HpChanged?.Invoke();
            }
        }

        public float MaxHp
		{
			get => _playerState.MaxHp;
			set => _playerState.MaxHp = value;
		}



		public void LoadProgress(PlayerProgress progress)
        {
            _playerState = progress.PlayerState;
            HpChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHp = CurrentHp;
            progress.PlayerState.MaxHp = MaxHp;
        }

        public void ApplyDamage(float damage)
        {
         if (CurrentHp <= 0)
             return;

         CurrentHp -= damage;
         //TODO сюда же потом вставить анимацию получения урона типа Animator.Play(HitAnimation)
        }
    }
}