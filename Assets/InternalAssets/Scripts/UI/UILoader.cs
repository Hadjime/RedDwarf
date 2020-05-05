using InternalAssets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.UI
{
    public class UILoader : MonoBehaviour
    {
        public bool isVisibleUI;
        private Keyboard keyboard;
        private void Start()
        {
            keyboard = Keyboard.current;
            if (isVisibleUI) LoadUIIndicators();
        }

        //<summary
        //123
        //</summary>
        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.F1))
            {
                LoadUIIndicators();
            }*/

            if (keyboard.f1Key.wasPressedThisFrame)
            {
                LoadUIIndicators();
            }
        }

        private void LoadUIIndicators()
        {
            if (SceneManager.GetSceneByName("UIIndicators").isLoaded == false)
            {
                //SceneManager.LoadSceneAsync("UIIndicators", LoadSceneMode.Additive).completed += HandleSetActiveScene;
                SceneManager.LoadSceneAsync("UIIndicators", LoadSceneMode.Additive);
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
}
