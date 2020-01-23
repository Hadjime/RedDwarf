using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{

    //<summary
    //123
    //</summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (SceneManager.GetSceneByName("UIIndicators").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("UIIndicators", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("UIIndicators");
            }
        }
    }
}
