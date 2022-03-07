using System;
using InternalAssets.Scripts.Infrastructure;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.States;
using UnityEngine;


namespace InternalAssets.Scripts.Logic
{
	public class LevelTransferTrigger : MonoBehaviour
	{
		private const string PLAYER_LAYER_NAME = "Player";
		[SerializeField] private string transferTo;
		private int _playerMask;
		private IGameStateMachine _stateMachine;
		private bool _triggered;


		public void Constructor(IGameStateMachine stateMachine) =>
			_stateMachine = stateMachine;


		private void Awake()
		{
			_stateMachine = AllServices.Container.Single<IGameStateMachine>(); //TODO по хорошему надо через фабрику передать зависимость
			_playerMask = LayerMask.NameToLayer(PLAYER_LAYER_NAME);
		}


		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_triggered)
				return;
			
			if (other.gameObject.layer == _playerMask)
			{
				_stateMachine.Enter<LoadSceneState, string>(transferTo);
				_triggered = true;
			}
		}
	}
}
