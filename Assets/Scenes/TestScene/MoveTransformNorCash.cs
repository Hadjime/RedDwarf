using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformNorCash : MonoBehaviour
{
    [SerializeField]
    bool isUp;
    [SerializeField]
    bool isDown;
    [SerializeField]
    bool isLeft;
    [SerializeField]
    bool isRight;
    Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            isUp = true;
            isDown = false;
            isLeft = false;
            isRight = false;
            moveVector = new Vector3(0, 1, 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            isUp = false;
            isDown = true;
            isLeft = false;
            isRight = false;
            moveVector = new Vector3(0, -1, 0);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            isUp = false;
            isDown = false;
            isLeft = false;
            isRight = true;
            moveVector = new Vector3(1, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            isUp = false;
            isDown = false;
            isLeft = true;
            isRight = false;
            moveVector = new Vector3(-1, 0, 0);
        }
        this.transform.Translate(moveVector * Time.deltaTime);
        
    }
}
