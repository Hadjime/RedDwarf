using System;
using InternalAssets.Scripts.Infrastructure;
using InternalAssets.Scripts.Map;
using InternalAssets.Scripts.Services.Input;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;


namespace InternalAssets.Scripts.Player
{
	public class PlayerMove : MonoBehaviour
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
			_inputService = Game.InputServices;
			_rb = GetComponent<Rigidbody2D>();
			_maskForMove = LayerMask.GetMask("Wall", "RockAndOther");
			_maskForAttack = LayerMask.GetMask("RockAndOther");
			_animator = GetComponent<Animator>();

			_startPosition = _rb.position;
		}


		private void Update()
		{
			Movement(_inputService.RawMovementInput);
			CustomDebug.Log($"RawMovement = {_inputService.RawMovementInput}", Color.green);
		}


		public void FixedUpdate()
		{
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

		public void Movement(Vector2 rawMovement)
		{
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
