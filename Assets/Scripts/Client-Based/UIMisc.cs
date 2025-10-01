using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMisc : MonoBehaviour
{
    public Button quitButton;
    public int[] uselessPlatforms =
    {
        17,
        25,
        9,
        15,
        38,
        24,
        15,
        32
    };
    private void Awake() {
        if ((Application.isConsolePlatform || Application.platform == RuntimePlatform.WebGLPlayer) && quitButton != null) {
            quitButton.gameObject.SetActive(false);
        }
    }
    public void RestartScene() {
        // int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(SceneIndex);
    }
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenURL(string url) {
        Application.OpenURL(url);
    }

    public void Quit() {
        if(Application.platform == RuntimePlatform.WebGLPlayer) return;
        Application.Quit();
    }
}
