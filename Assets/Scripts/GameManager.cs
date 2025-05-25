using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<GameManager>();
                instance.name = instance.GetType().ToString();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    string nextScene;

    public void StartNewGame()
    {
        LoadNewScene("Area1");
    }

    public void LoadNewScene(string scene)
    {
        SetSceneAfterLoad(scene);
        SceneManager.LoadScene("Loading");
    }

    public void SetSceneAfterLoad(string scene)
    {
        nextScene = scene;
    }

    public string GetSceneAfterLoad()
    {
        return nextScene;
    }
}
