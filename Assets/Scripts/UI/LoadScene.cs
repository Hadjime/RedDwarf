using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField]    private string sceneName;
    // Start is called before the first frame update
    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }


}
