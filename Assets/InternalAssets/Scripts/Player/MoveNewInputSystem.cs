using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player
{
    public class MoveNewInputSystem : MonoBehaviour
    {
        [Range(1f,50f)][SerializeField] private float speed = 5;
        public bool isUp;
        public bool isDown;
        public bool isLeft;
        public bool isRight;
        public bool isMoving;
        private Vector2 _direction;
        private Rigidbody2D _rb;
        private Vector2 _destinationPoint;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
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
                Debug.Log("_destinationpoint = " + _destinationPoint);
                _rb.MovePosition(position: _rb.position + _direction * (Time.deltaTime * speed));
            }
            
        }

        public void Movement(InputAction.CallbackContext context)
        {
            if (context.ReadValue<Vector2>().x != 0 || context.ReadValue<Vector2>().y != 0)
            {
                _direction = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
                Debug.Log(context.ReadValue<Vector2>());
                isMoving = true;
                switch (context.action.activeControl.displayName) //визуализация в каком направлении идет персонаж
                {
                    case "W":
                        isUp = true;
                        isDown = false;
                        isRight = false;
                        isLeft = false;
                        break;
                    case "S":
                        isUp = false;
                        isDown = true;
                        isRight = false;
                        isLeft = false;
                        break;
                    case "A":
                        isUp = false;
                        isDown = false;
                        isRight = false;
                        isLeft = true;
                        break;
                    case "D":
                        isUp = false;
                        isDown = false;
                        isRight = true;
                        isLeft = false;
                        break;
                }
            }

            
        }
    }
}
