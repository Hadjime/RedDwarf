﻿using System.Collections;
using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InternalAssets.Scripts.UI
{
    public class BtnManager : MonoBehaviour
    {
        [Header("---Загрузка уровня с переходом---")]
        public Animator transitionAnimator;
        public float transitionTime = 1f;
    
        public void LoadNextLevelWithTransition(string nameScene)
        {
            StartCoroutine(LoadLevel(nameScene));
        }
    
    
        public void SetLanguageIndex(int indexLanguage)
        {
            switch (indexLanguage)
            {
                case 0:
                    LeanLocalization.CurrentLanguage = "Russian";
                    break;
                case 1:
                    LeanLocalization.CurrentLanguage = "English";
                    break;
                default:
                    break;
            }
        }
    
        IEnumerator LoadLevel(string nameScene)
        {
            transitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(nameScene);
        }
    }
}
