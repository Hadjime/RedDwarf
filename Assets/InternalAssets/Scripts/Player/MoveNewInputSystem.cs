using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player
{
    public class MoveNewInputSystem : MonoBehaviour
    {
        [Range(1f,50f)][SerializeField] private float speed = 2;
        public bool isUp;
        public bool isDown;
        public bool isLeft;
        public bool isRight;
        public bool isMoving;
        private Vector2 _direction;
        private Rigidbody2D _rb;
        private Vector2 _destinationPoint;
        [SerializeField] private LayerMask _maskForMove;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _maskForMove = LayerMask.GetMask("Wall");
        }

        public void FixedUpdate()
        {
            if (/*isUp == true ||
                isDown == true ||
                isLeft == true ||
                isRight == true*/
                isMoving == true)
            {
                //transform.Translate(_direction * (Time.deltaTime * speed));
                _destinationPoint = _rb.position + _direction;
                //Debug.Log("_destinationpoint = " + _destinationPoint);
                //_rb.MovePosition(position: _rb.position + _direction * (Time.deltaTime * speed));
                //_rb.MovePosition(position: _destinationPoint * (Time.deltaTime * speed));
            }
            
        }

        public void Movement(InputAction.CallbackContext context)
        {
            if (context.ReadValue<Vector2>().x != 0 || context.ReadValue<Vector2>().y != 0)
            {
                _direction = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
                _destinationPoint = _rb.position + _direction;
                _rb.MovePosition(position: _destinationPoint * (Time.deltaTime * speed));
                Debug.Log(_destinationPoint);
                switch (context.action.activeControl.displayName) //визуализация в каком направлении идет персонаж
                {
                    case "W":
                        if (CheckPointForMove(Vector2.up))
                        {
                            isUp = true;
                            isDown = false;
                            isRight = false;
                            isLeft = false;  
                            isMoving = true;
                        }
                        break;
                    case "S":
                        if (CheckPointForMove(Vector2.down))
                        {
                            isUp = false;
                            isDown = true;
                            isRight = false;
                            isLeft = false;
                            isMoving = true;
                        }
                        break;
                    case "A":
                        if (CheckPointForMove(Vector2.left))
                        {
                            isUp = false;
                            isDown = false;
                            isRight = false;
                            isLeft = true;
                            isMoving = true;
                        }
                        break;
                    case "D":
                        if (CheckPointForMove(Vector2.right))
                        {
                            isUp = false;
                            isDown = false;
                            isRight = true;
                            isLeft = false;
                            isMoving = true;
                        }
                        break;
                }
            }
        }

        private bool CheckPointForMove(Vector2 point)
        {
            bool isCheckPoint = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, point, 1, _maskForMove);
            Debug.DrawRay(transform.position, point, Color. green);
            // If it hits something...
            if (hit.collider != null)
            {
                isCheckPoint = false;
                //Debug.Log(hit.collider.name);
            }
            return isCheckPoint;
        }
    }
}
