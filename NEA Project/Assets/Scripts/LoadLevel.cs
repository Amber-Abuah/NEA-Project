using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadStartLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadChoiceLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadStatisticsLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadMiniGameLevel()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void LoadPrePlayLevel()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void LoadLinksLevel()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void LoadTrainingLevel()
    {
        SceneManager.LoadSceneAsync(6);
    }
}
