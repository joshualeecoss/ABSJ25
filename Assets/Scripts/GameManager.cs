using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalInspiration = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }

    public void AddInspiration(int inspiration)
    {
        totalInspiration += inspiration;
        if (totalInspiration == 3)
        {
            GetComponent<SceneLoadTrigger>().SetTargetScene("Drama");
            GetComponent<SceneLoadTrigger>().LoadScene();
        }
        else if (totalInspiration == 6)
        {
            GetComponent<SceneLoadTrigger>().SetTargetScene("Music");
            GetComponent<SceneLoadTrigger>().LoadScene();
        }
        else if (totalInspiration == 9)
        {
            GetComponent<SceneLoadTrigger>().SetTargetScene("Credits");
            GetComponent<SceneLoadTrigger>().LoadScene();
        }
    }

    public void QuitGame()
    {
        // HACK: Wait a second before quitting for sound to play
        Invoke("QuitGameWaiter", 1);
    }

    void QuitGameWaiter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
