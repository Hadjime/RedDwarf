﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectSizeDelta : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rectTransform.sizeDelta);
    }
}
