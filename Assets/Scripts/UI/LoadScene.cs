﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(string nameScene)
        {
            
            SceneManager.LoadScene(nameScene);
        }


    }
}