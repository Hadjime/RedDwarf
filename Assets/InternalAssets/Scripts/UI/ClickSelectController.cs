using UnityEngine;

namespace InternalAssets.Scripts.UI
{
    public class ClickSelectController : MonoBehaviour
    {
        public Camera camer;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var ray = camer.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                if (Physics.Raycast(ray, out var hitInfo))
                {
                    Debug.Log(hitInfo.transform.name);
                }
            }
        }
    }
}
