using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string sceneName;

    public void Click() {
        SceneManager.LoadScene(sceneName);
    }
}
