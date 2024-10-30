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

    public void startLevel1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void startLevel2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }

    public void startLevel3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }

    public void startLevel4()
    {
        SceneManager.LoadSceneAsync("Level 4");
    }
}
