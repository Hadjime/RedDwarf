using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class GenerateObject : MonoBehaviour
{
    public GameObject someObject;
    public int numberObject;

    int posX;
    int posY;
    Vector3 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberObject; i++)
        {
            posX = Random.Range(0, 50);
            posY = Random.Range(0, 50);
            currentPos = new Vector3(posX, posY, 0);
            Instantiate(someObject, currentPos, Quaternion.Normalize(this.transform.rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
