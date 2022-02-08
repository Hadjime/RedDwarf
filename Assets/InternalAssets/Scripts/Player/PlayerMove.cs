using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Map;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Player
{
	public class PlayerMove : MonoBehaviour, ISavedProgress
	{
		[Range(0.1f, 50f)] [SerializeField] private float speed = 2;
		[Range(0.1f, 50f)] [SerializeField] private int handAttack = 2;
		[SerializeField] private bool isUp;
		[SerializeField] private bool isDown;
		[SerializeField] private bool isLeft;
		[SerializeField] private bool isRight;
		[SerializeField] private bool isMoving;
		[SerializeField] private bool isAttack;
		
		private static readonly int IsDiggingHash = Animator.StringToHash("isDigging");
		private static readonly int IsRunHash = Animator.StringToHash("isRun");
		
		private IInputService _inputService;
		private Vector2 _direction;
		private Rigidbody2D _rb;
		private LayerMask _maskForMove;
		private LayerMask _maskForAttack;
		private Animator _animator;
		private Vector2 _startPosition;
		private Vector2 _stopPosition;


		private const float TimeoutAttack = 1f;
		private float currentTime;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_rb = GetComponent<Rigidbody2D>();
			_maskForMove = LayerMask.GetMask("Wall", "RockAndOther");
			_maskForAttack = LayerMask.GetMask("RockAndOther");
			_animator = GetComponent<Animator>();

			_startPosition = _rb.position;
		}


		private void Update()
		{
			Movement(_inputService.RawMovementInput);
			// CustomDebug.Log($"RawMovement = {_inputService.RawMovementInput}", Color.green);
		}


		private void FixedUpdate()
		{
			if (_direction == Vector2.zero)
				return;
			
			if (isMoving) // если находимся в движении
			{
				_rb.MovePosition(position: _rb.position + _direction * (Time.deltaTime * speed));
				if (Vector2.Distance(_rb.position, _stopPosition) <= 0.01) //если пришли в конечную точку
				{
					_rb.position = _stopPosition;
					if (isUp)
					{
						_direction = Vector2.up;
						_animator.SetBool(IsRunHash, true);
						_rb.rotation = 90;
						isUp = false;
					}
					if (isDown)
					{
						_direction = Vector2.down;
						_animator.SetBool(IsRunHash, true);
						_rb.rotation = 270;
						isDown = false;
					}
					if (isRight)
					{
						_direction = Vector2.right;
						_animator.SetBool(IsRunHash, true);
						_rb.rotation = 0;
						isRight = false;
					}
					if (isLeft)
					{
						_direction = Vector2.left;
						_animator.SetBool(IsRunHash, true);
						_rb.rotation = 180;
						isLeft = false;
					}
					//isMoving = false;
					_startPosition = _stopPosition;
					if (CheckPointForMove(_direction))
					{
						_stopPosition = _startPosition + _direction;
					}
					else
					{
						isUp = false;
						isDown = false;
						isRight = false;
						isLeft = false;
						isMoving = false;
						_animator.SetBool(IsRunHash, false);
					}
				}
			}
			else // если стоим на месте
			{
				if (isAttack)
				{
					Attack(_direction);
				}
				else
				{
					if (CheckPointForMove(_direction))
					{
						_stopPosition = _startPosition + _direction;
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

		private string CurrentLevel() => 
			SceneManager.GetActiveScene().name;

		private void Warp(Vector3Data to) => 
			transform.position = to.AsUnityVector();

		private void Attack(Vector2 attackPoint)
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
						hit.collider.GetComponent<TileSetting>().DamageTile(handAttack);
						if (CheckPointForMove(_direction))
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

		private void Movement(Vector2 rawMovement)
		{
			if (_direction == Vector2.zero)
			{
				isMoving = false;
				isAttack = false;
				isUp = false;
				isDown = false;
				isRight = false;
				isLeft = false;
			}
			
			
			if (!isMoving || isAttack) //если стоим на месте
			{
				_direction = rawMovement;
				RotationPlayer(rawMovement);
				if (CheckPointForMove(rawMovement))
				{
					var currentPosition = new Vector2(Mathf.RoundToInt(_rb.position.x), Mathf.RoundToInt(_rb.position.y));
					_startPosition = currentPosition;
					_stopPosition = currentPosition + _direction;
					_animator.SetBool(IsDiggingHash, false);
					_animator.SetBool(IsRunHash, true);
					isMoving = true;
					isAttack = false;
				}
			}
			else // если находимся в движении
			{
				if (_direction != rawMovement)
				{
					if (rawMovement == Vector2.right)
					{
						isUp = false;
						isDown = false;
						isRight = true;
						isLeft = false;
					}
					if (rawMovement == Vector2.left)
					{
						isUp = false;
						isDown = false;
						isRight = false;
						isLeft = true;
					}
					if (rawMovement == Vector2.up)
					{
						isUp = true;
						isDown = false;
						isRight = false;
						isLeft = false;
					}
					if (rawMovement == Vector2.down)
					{
						isUp = false;
						isDown = true;
						isRight = false;
						isLeft = false;
					}
				}
				if (_direction == Vector2.up && rawMovement == Vector2.down ||
					_direction == Vector2.down && rawMovement == Vector2.up ||
					_direction == Vector2.left && rawMovement == Vector2.right ||
					_direction == Vector2.right && rawMovement == Vector2.left) //для передвижения в противоположном направлении сразу меняем стартовую и конечную точки
				{
					var temp = _startPosition;
					_startPosition = _stopPosition;
					_stopPosition = temp;
					_direction = rawMovement;
					RotationPlayer(rawMovement);
					isUp = false;
					isDown = false;
					isRight = false;
					isLeft = false;
				}
			}
		}


		private bool CheckPointForMove(Vector2 point)
		{
			var position = transform.position;
			RaycastHit2D hit = Physics2D.Raycast(position, point, 1, _maskForMove);
			Debug.DrawRay(position, point, Color.green);
			if (hit.collider != null)
			{
				return false;
			}
			return true;
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
	}
}
