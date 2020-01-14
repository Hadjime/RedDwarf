using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyAnchors : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    void Update()
    {
        Debug.Log(rectTransform.anchoredPosition);
    }
}
