using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveMenu : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.DOMove(Vector3.down, 2, false);
    }
}
