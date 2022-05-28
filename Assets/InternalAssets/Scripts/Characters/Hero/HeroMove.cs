using System;
using InternalAssets.Scripts.Cheats;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Characters.Hero
{
	public class HeroMove : MonoBehaviour, ISavedProgress
	{
		private const int DISTANCE = 1;
		[SerializeField] private float timeBetweenSteps = 1;
		[Range(0.1f, 50f)] [SerializeField] private float deltaTimeMultiplier = 1;
		[SerializeField] private bool isMoving;
		
		
		private IInputService _inputService;
		private HeroAnimator _animator;
		private Rigidbody2D _rb;
		private LayerMask _maskForMove;
		private Vector2 _currentNormalizeDirection;
		private RaycastHit2D[] _poolRaycast = new RaycastHit2D[10];
		private float _timer;

		//просто смотреть в инспекторе
		[field: SerializeField] public Vector2 _nextNormalizeDirection { get; private set; }
		[SerializeField] private Vector2 _previousPosition;
		[SerializeField] private Vector2 _nextPosition;

		public bool IsMoving => isMoving;
		public Vector2 CurrentNormalizeDirection => _currentNormalizeDirection;
		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_rb = GetComponent<Rigidbody2D>();
			_maskForMove = LayerMask.GetMask("Wall", "RockAndOther");
			_animator = GetComponent<HeroAnimator>();
			
			_previousPosition = _rb.position;
			_nextPosition = _previousPosition;
			
			_inputService.MovementDirectionChanged += OnMovementDirectionChanged;
			ResetAllFlags();
			_currentNormalizeDirection = new Vector2(1, 0);
			_nextNormalizeDirection = new Vector2(1, 0);
		}

		private void OnDestroy() =>
			_inputService.MovementDirectionChanged -= OnMovementDirectionChanged;


		private void Update()
		{
			if (_currentNormalizeDirection == Vector2.zero)
				ResetAllFlags();


			if (!isMoving) //если стоим на месте
			{
				_currentNormalizeDirection = _nextNormalizeDirection;
				RotationPlayer(_currentNormalizeDirection);
				if (IsMoveAvailable(_currentNormalizeDirection))
				{
					_previousPosition = _rb.position;
					_nextPosition = GetNextPosition(_currentNormalizeDirection);
					_animator.PlayRunAnimation();
					isMoving = true;
				}
			}

			CheatsThroughDI.Instance.IsMoving = isMoving;
		}

		private void FixedUpdate()
		{
			if (_currentNormalizeDirection == Vector2.zero)
				return;

			if (!isMoving)
				return;
			
			_timer += (Time.deltaTime * deltaTimeMultiplier);
			Vector2 position = Vector2.Lerp(_previousPosition, _nextPosition, _timer / timeBetweenSteps);
			_rb.MovePosition(position: position);
			// _rb.MovePosition(position: _rb.position + _currentNormalizeDirection * (Time.deltaTime * speed));
			if (!(Vector2.Distance(_rb.position, _nextPosition) <= 0.01))
				return;
				
			_timer = 0;
			_previousPosition = _nextPosition;
			_rb.position = _nextPosition;
			isMoving = false;
		}

		private void OnMovementDirectionChanged(Vector2 normalizeDirection)
		{
			_nextNormalizeDirection = normalizeDirection;
			CheatsThroughDI.Instance.NextNormalizeDirectionX = _nextNormalizeDirection.x;
			CheatsThroughDI.Instance.NextNormalizeDirectionY = _nextNormalizeDirection.y;
			
			if (!IsMovementInOppositeDirectionAvailable())
				return;

			TurnAround();
		}

		private bool IsMovementInOppositeDirectionAvailable()
		{
			return _currentNormalizeDirection == Vector2.up && _nextNormalizeDirection == Vector2.down ||
				   _currentNormalizeDirection == Vector2.down && _nextNormalizeDirection == Vector2.up ||
				   _currentNormalizeDirection == Vector2.left && _nextNormalizeDirection == Vector2.right ||
				   _currentNormalizeDirection == Vector2.right && _nextNormalizeDirection == Vector2.left ||
				   _currentNormalizeDirection == Vector2.zero;
		}

		private void TurnAround()
		{
			(_previousPosition, _nextPosition) = (_nextPosition, _previousPosition);
			_currentNormalizeDirection = _nextNormalizeDirection;
			RotationPlayer(_currentNormalizeDirection);
			if (!Mathf.Approximately(_timer, 0))
				_timer -= timeBetweenSteps;
			_timer = Math.Abs(_timer);
		}

		private string CurrentLevel() => 
			SceneManager.GetActiveScene().name;

		private void Warp(Vector3Data to) => 
			transform.position = to.AsUnityVector();

		private bool IsMoveAvailable(Vector2 point)
		{
			Vector2 position = _rb.position;
			int count = Physics2D.RaycastNonAlloc(position, point, _poolRaycast, DISTANCE, _maskForMove);
			Color rayColor = count > 0 ? Color.red : Color.green;
			Debug.DrawRay(position, point, rayColor);
			return count == 0;
		}

		private void RotationPlayer(Vector2 direction)
		{
			if (direction.y > 0)
				_rb.rotation = 90;
			
			if (direction.y < 0)
				_rb.rotation = 270;
			
			if (direction.x > 0)
				_rb.rotation = 0;

			if (direction.x < 0)
				_rb.rotation = 180;
		}

		private Vector2 GetNextPosition(Vector2 normalizeDirection)
		{
			Vector2 intVector = _rb.position + normalizeDirection;
			intVector.x = Mathf.Ceil(intVector.x);
			intVector.y = Mathf.Ceil(intVector.y);
			return intVector;
		}

		private void ResetAllFlags()
		{
			_currentNormalizeDirection = Vector2.zero;
			isMoving = false;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
			{
				Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
				if (savedPosition != null) 
					Warp(to: savedPosition);
			}
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			var snapPosition = transform.position;
			snapPosition.x = Mathf.CeilToInt(snapPosition.x);
			snapPosition.y = Mathf.CeilToInt(snapPosition.y);
			snapPosition.z = Mathf.CeilToInt(snapPosition.z);
			progress.WorldData.PositionOnLevel =
				new PositionOnLevel(CurrentLevel(), snapPosition.AsVectorData());
		}
	}
}
