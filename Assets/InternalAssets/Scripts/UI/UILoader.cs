using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Start()
    {
        LoadUIIndicators();
    }

    //<summary
    //123
    //</summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadUIIndicators();
        }
    }

    private void LoadUIIndicators()
    {
        if (SceneManager.GetSceneByName("UIIndicators").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("UIIndicators", LoadSceneMode.Additive).completed += HandleSetActiveScene;
        }
        else
        {
            SceneManager.UnloadSceneAsync("UIIndicators");
        }
    }

    private void HandleSetActiveScene(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIIndicators"));
    }
}
