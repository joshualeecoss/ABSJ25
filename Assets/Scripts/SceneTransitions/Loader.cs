using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Next scene loader
    public SceneLoadTrigger sceneLoadTrigger;
    [SerializeField] float waitTimeLoad = 0f;
    public bool nextSceneLoadStart = false;

    void Start()
    {
        sceneLoadTrigger = GetComponent<SceneLoadTrigger>();
    }

    public void NewGameTrigger()
    {
        nextSceneLoadStart = true;
        StartCoroutine(LoadNextOnTime(waitTimeLoad));
    }

    IEnumerator LoadNextOnTime(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        sceneLoadTrigger.LoadScene();
    }
}
