using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] Image loadingBar;

    private string nextScene;

    void Awake()
    {
        nextScene = GameManager.Instance.GetSceneAfterLoad();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextScene);

        while (!loadLevel.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(loadLevel.progress / 0.9f);
            yield return null;
        }
    }
}
