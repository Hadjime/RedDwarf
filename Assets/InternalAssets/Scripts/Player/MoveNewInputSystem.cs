using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player
{
    public class MoveNewInputSystem : MonoBehaviour
    {
        [SerializeField] private bool isUp;
        [SerializeField] private bool isDown;
        [SerializeField] private bool isLeft;
        [SerializeField] private bool isRight;
        private Vector3 direction;
        public void Update()
        {
            transform.Translate(direction * Time.deltaTime);
        }

        public void Move(InputAction.CallbackContext context)
        {
            direction = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0);
            Debug.Log(context.ReadValue<Key>());
        }
    }
}
