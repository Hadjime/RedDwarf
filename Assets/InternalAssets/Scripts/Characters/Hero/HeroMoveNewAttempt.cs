using System;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Characters.Hero
{
	public class HeroMoveNewAttempt : MonoBehaviour, ISavedProgress
	{
		private const float TimeoutAttack = 1f;
		private const int DISTANCE = 1;

		[SerializeField] private float time = 1;
		[Range(0.1f, 50f)] [SerializeField] private float deltaTimeMultiplier = 2;
		[Range(0.1f, 50f)] [SerializeField] private int handAttack = 2;
		[SerializeField] private bool isMoving;
		[SerializeField] private bool isAttack;
		
		private static readonly int IsDiggingHash = Animator.StringToHash("isDigging");
		private static readonly int IsRunHash = Animator.StringToHash("isRun");
		
		private IInputService _inputService;
		private Vector2 _currentNormalizeDirection;
		private Rigidbody2D _rb;
		private LayerMask _maskForMove;
		private LayerMask _maskForAttack;
		private Animator _animator;
		private Vector2 _startPosition;
		private Vector2 _stopPosition;
		private Transform thisTransform;
		private RaycastHit2D[] detect = new RaycastHit2D[10];
		private float _currentTimer;

		
		private float currentTime;
		[SerializeField] private Vector2 _previousPosition;
		[SerializeField] private Vector2 _nextPosition;
		[SerializeField] private Vector2 _nextNormalizeDirection;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_rb = GetComponent<Rigidbody2D>();
			_maskForMove = LayerMask.GetMask("Wall", "RockAndOther");
			_maskForAttack = LayerMask.GetMask("RockAndOther");
			_animator = GetComponent<Animator>();

			thisTransform = transform;
			_startPosition = _rb.position;
			
			_previousPosition = _rb.position;
			_nextPosition = _rb.position;

			_inputService.MovementDirectionChanged += OnMovementDirectionChanged;
			
			ResetAllFlags();
		}

		private void OnDestroy() =>
			_inputService.MovementDirectionChanged -= OnMovementDirectionChanged;

		private void OnMovementDirectionChanged(Vector2 normalizeDirection)
		{
			// if (_currentNormalizeDirection == normalizeDirection)
			// 	return;
			
			_nextNormalizeDirection = normalizeDirection;
			// if (IsMoveAvailable(normalizeDirection))
			// {
			// 	isMoving = true;
			// 	_nextPoint = GetNextPoint(_currentNormalizeDirection);
			// 	CustomDebug.Log($"_nextPoint = {_nextPoint}");
			// };
			CustomDebug.Log($"normalizeDirection - {_currentNormalizeDirection}");
			
		}


		private void Update() =>
			Movement(_inputService.RawMovementInput);

		private void Movement(Vector2 rawMovement)
		{
			if (_currentNormalizeDirection == Vector2.zero)
				ResetAllFlags();


			if (!isMoving || isAttack) //если стоим на месте
			{
				_currentNormalizeDirection = _nextNormalizeDirection;
				RotationPlayer(_currentNormalizeDirection);
				if (IsMoveAvailable(_currentNormalizeDirection))
				{
					_previousPosition = _rb.position;
					_nextPosition = GetNextPosition(_currentNormalizeDirection);
					_animator.SetBool(IsDiggingHash, false);
					_animator.SetBool(IsRunHash, true);
					isMoving = true;
					isAttack = false;
				}
			}
			else // если находимся в движении
			{
				if (_currentNormalizeDirection == Vector2.up && _nextNormalizeDirection == Vector2.down ||
					_currentNormalizeDirection == Vector2.down && _nextNormalizeDirection == Vector2.up ||
					_currentNormalizeDirection == Vector2.left && _nextNormalizeDirection == Vector2.right ||
					_currentNormalizeDirection == Vector2.right && _nextNormalizeDirection == Vector2.left) //для передвижения в противоположном направлении сразу меняем стартовую и конечную точки
				{
					(_previousPosition, _nextPosition) = (_nextPosition, _previousPosition);
					_currentNormalizeDirection = _nextNormalizeDirection;
					RotationPlayer(_currentNormalizeDirection);
					_currentTimer -= time;
					_currentTimer = Math.Abs(_currentTimer);
				}
			}
		}

		private void FixedUpdate()
		{
			if (_currentNormalizeDirection == Vector2.zero)
				return;
			
			if (isMoving) // если находимся в движении
			{
				_currentTimer += (Time.deltaTime * deltaTimeMultiplier);
				Vector2 position = Vector2.Lerp(_previousPosition, _nextPosition, _currentTimer / time);
				_rb.MovePosition(position: position);
				// _rb.MovePosition(position: _rb.position + _currentNormalizeDirection * (Time.deltaTime * speed));
				if (Vector2.Distance(_rb.position, _nextPosition) <= 0.01) //если пришли в конечную точку
				{
					_currentTimer = 0;
					_previousPosition = _nextPosition;
					_rb.position = _nextPosition;
					isMoving = false;
					// _nextPosition = GetNextPosition(_currentNormalizeDirection);
					
					
					// if (isUp)
					// {
					// 	_currentNormalizeDirection = Vector2.up;
					// 	_animator.SetBool(IsRunHash, true);
					// 	_rb.rotation = 90;
					// 	isUp = false;
					// }
					// if (isDown)
					// {
					// 	_currentNormalizeDirection = Vector2.down;
					// 	_animator.SetBool(IsRunHash, true);
					// 	_rb.rotation = 270;
					// 	isDown = false;
					// }
					// if (isRight)
					// {
					// 	_currentNormalizeDirection = Vector2.right;
					// 	_animator.SetBool(IsRunHash, true);
					// 	_rb.rotation = 0;
					// 	isRight = false;
					// }
					// if (isLeft)
					// {
					// 	_currentNormalizeDirection = Vector2.left;
					// 	_animator.SetBool(IsRunHash, true);
					// 	_rb.rotation = 180;
					// 	isLeft = false;
					// }
					// //isMoving = false;
					// _startPosition = _stopPosition;
					// if (IsMoveAvailable(_currentNormalizeDirection))
					// {
					// 	_stopPosition = _startPosition + _currentNormalizeDirection;
					// }
					// else
					// {
					// 	isUp = false;
					// 	isDown = false;
					// 	isRight = false;
					// 	isLeft = false;
					// 	isMoving = false;
					// 	_animator.SetBool(IsRunHash, false);
					// }
				}
			}
			else // если стоим на месте
			{
				if (isAttack)
				{
					DigAttack(_currentNormalizeDirection);
				}
				else
				{
					if (IsMoveAvailable(_currentNormalizeDirection))
					{
						_nextPosition = GetNextPosition(_currentNormalizeDirection);
						_animator.SetBool(IsRunHash, true);
						isMoving = true;
					}
					else
					{
						isAttack = true;
						_animator.SetBool(IsDiggingHash, true);
					}
				}
			}
		}


		private string CurrentLevel() => 
			SceneManager.GetActiveScene().name;

		private void Warp(Vector3Data to) => 
			transform.position = to.AsUnityVector();

		private void DigAttack(Vector2 attackPoint)
		{
			if (currentTime >= TimeoutAttack)
			{
				currentTime = 0;
				var position = transform.position;
				RaycastHit2D hit = Physics2D.Raycast(position, attackPoint, 1, _maskForMove);
				Debug.DrawRay(position, attackPoint, Color.red);
				if (hit.collider != null)
				{
					if (1 << hit.collider.gameObject.layer == _maskForAttack.value)
					{
						hit.collider.GetComponent<IHealth>()?.ApplyDamage(handAttack);
						if (IsMoveAvailable(_currentNormalizeDirection))
						{
							isMoving = true;
							isAttack = false;
							_animator.SetBool(IsDiggingHash, false);
						}
					}
				}
				else isAttack = false;
			}
			currentTime += Time.deltaTime;
		}




		private bool IsMoveAvailable(Vector2 point)
		{
			Vector2 position = _rb.position;
			int count = Physics2D.RaycastNonAlloc(position, point, detect, DISTANCE, _maskForMove);
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
			isAttack = false;
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
