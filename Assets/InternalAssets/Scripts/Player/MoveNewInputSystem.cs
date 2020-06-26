using System;
using System.Security.Principal;
using InternalAssets.Scripts.Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player
{
    public class MoveNewInputSystem : MonoBehaviour
    {
        [Range(0.1f,50f)][SerializeField] private float speed = 2;
        public bool isUp;
        public bool isDown;
        public bool isLeft;
        public bool isRight;
        public bool isMoving;
        public bool isAttack;
        private Vector2 _direction;
        private Rigidbody2D _rb;
        private Vector2 _startPosition;
        private Vector2 _stopPosition;
        private LayerMask _maskForMove;
        private LayerMask _maskForAttack;
        private Animator _animator;
        private const float TimeoutAttack = 1f;
        private float currentTime;
        private static readonly int IsDigging = Animator.StringToHash("isDigging"); 
        private static readonly int IsRun = Animator.StringToHash("isRun");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startPosition = _rb.position;
            _maskForMove = LayerMask.GetMask("Wall", "RockAndOther");
            _maskForAttack = LayerMask.GetMask("RockAndOther");
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
            
            
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
                        _animator.SetBool(IsRun, true);
                        _rb.rotation = 90;
                        isUp = false;
                    }
                    if (isDown)
                    {
                        _direction = Vector2.down;
                        _animator.SetBool(IsRun, true);
                        _rb.rotation = 270;
                        isDown = false;
                    }
                    if (isRight)
                    {
                        _direction = Vector2.right;
                        _animator.SetBool(IsRun, true);
                        _rb.rotation = 0;
                        isRight = false;
                    }
                    if (isLeft)
                    {
                        _direction = Vector2.left;
                        _animator.SetBool(IsRun, true);
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
                        _animator.SetBool(IsRun, false);
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
                        _animator.SetBool(IsRun, true);
                        isMoving = true;
                    }
                    else
                    {
                        isAttack = true;
                        _animator.SetBool(IsDigging, true);
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
                    
                        hit.collider.GetComponent<TileSetting>().HpTile += -20;
                        if (CheckPointForMove(_direction))
                        {
                            isMoving = true;
                            isAttack = false;
                            _animator.SetBool(IsDigging, false);
                        }
                    }
                }
                else isAttack = false;
            }
            currentTime += Time.deltaTime;
            
        }

        public void Movement(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!isMoving || isAttack) //если стоим на месте
                {
                    _direction = context.ReadValue<Vector2>();
                    RotationPlayer(context.action.activeControl.name);
                    if (CheckPointForMove(context.ReadValue<Vector2>()))
                    {
                        var currentPosition = new Vector2(Mathf.RoundToInt(_rb.position.x), Mathf.RoundToInt(_rb.position.y));
                        _startPosition = currentPosition;
                        _stopPosition = currentPosition + _direction;
                        _animator.SetBool(IsDigging, false);
                        _animator.SetBool(IsRun, true);
                        isMoving = true;
                        isAttack = false;
                    }
                    
                }
                else // если находимся в движении
                {
                    if (_direction != context.ReadValue<Vector2>())
                    {
                        if (context.ReadValue<Vector2>() == Vector2.right)
                        {
                            isUp = false;
                            isDown = false;
                            isRight = true;
                            isLeft = false;
                        }
                        if (context.ReadValue<Vector2>() == Vector2.left)
                        {
                            isUp = false;
                            isDown = false;
                            isRight = false;
                            isLeft = true;
                        }
                        if (context.ReadValue<Vector2>() == Vector2.up)
                        {
                            isUp = true;
                            isDown = false;
                            isRight = false;
                            isLeft = false;
                        }
                        if (context.ReadValue<Vector2>() == Vector2.down)
                        {
                            isUp = false;
                            isDown = true;
                            isRight = false;
                            isLeft = false;
                        }
                    }
                    if (_direction == Vector2.up && context.ReadValue<Vector2>() == Vector2.down ||
                        _direction == Vector2.down && context.ReadValue<Vector2>() == Vector2.up ||
                        _direction == Vector2.left && context.ReadValue<Vector2>() == Vector2.right ||
                        _direction == Vector2.right && context.ReadValue<Vector2>() == Vector2.left) //для передвижения в противоположном направлении сразу меняем стартовую и конечную точки
                    {
                        var temp = _startPosition;
                        _startPosition = _stopPosition;
                        _stopPosition = temp;
                        _direction = context.ReadValue<Vector2>();
                        RotationPlayer(context.action.activeControl.name);
                        isUp = false;
                        isDown = false;
                        isRight = false;
                        isLeft = false;
                    }
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

        public void RotationPlayer(string btnName)
        {
            switch (btnName) // визуализация в каком направлении идет персонаж
            {
                case "w":
                    _rb.rotation = 90;
                    break;
                case "s":
                    _rb.rotation = 270;
                    break;
                case "a":
                    _rb.rotation = 180;
                    break;
                case "d":
                    _rb.rotation = 0;
                    break;
            }
        }
    }
}
