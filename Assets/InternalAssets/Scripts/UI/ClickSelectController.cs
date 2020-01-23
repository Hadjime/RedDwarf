using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelectController : MonoBehaviour
{
    [SerializeField] private Camera _camer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var ray = _camer.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                Debug.Log(hitInfo.transform.name);
            }
        }
    }
}
