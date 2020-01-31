using UnityEngine;

namespace InternalAssets.Scripts._temp
{
    public class CuberController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnMoveUp()
        {
            transform.Translate(transform.up);
        }
        public void OnMoveDown()
        {
            transform.Translate(-transform.up);
        }
        public void OnMoveRight()
        {
            transform.Translate(transform.right);
        }
        public void OnMoveLeft()
        {
            transform.Translate(-transform.right);
        }
    }
}
