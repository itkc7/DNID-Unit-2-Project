using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerUI : MonoBehaviour
{
    public void startIntroVideo()
    {
        SceneManager.LoadSceneAsync("CutScene");
    }

    public void startTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void startGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void restartLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
