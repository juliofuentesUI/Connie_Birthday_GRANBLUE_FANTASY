using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNextSceneDelayed()
    {
        StartCoroutine(LoadNextSceneDelayedCoroutine());
    }

    public IEnumerator LoadNextSceneDelayedCoroutine()
    {
        yield return new WaitForSeconds(1f);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        Debug.Log($"SCENECOUNT IS {sceneCount}");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 == sceneCount)
        {
            //we are at the end. go to main menu.
            SceneManager.LoadScene(0);
        }
        else
        {

            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void RestartBattleScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLastScene()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(sceneCount - 2);
    }
}
