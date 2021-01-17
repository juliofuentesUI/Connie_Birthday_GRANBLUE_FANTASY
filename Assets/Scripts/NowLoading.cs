using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowLoading : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] float sceneHoldDuration;
    void Start()
    {
        StartCoroutine(HoldSceneCoroutine());
        SoundManager.Instance.StartFadeOutCoroutine();
    }

    private IEnumerator HoldSceneCoroutine()
    {
        yield return new WaitForSeconds(sceneHoldDuration);
        sceneLoader.LoadNextScene();
    }

}
