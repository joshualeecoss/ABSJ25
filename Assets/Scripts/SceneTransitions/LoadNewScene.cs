using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LoadNewScene : MonoBehaviour
{
    // Initialize Values
    AsyncOperation loadingOperation;
    AsyncOperation unloadingOperation;
    AsyncOperation unloadingTransitionOperation;

    // Load scene control variables
    // Have loading screen show for at least 3 seconds
    float minLoadTime = 3f;
    // Have "Loading" graphic change every .5 seconds
    float textChangeLoad = 0.3f;

    //Timers
    float loadTimer = 0f;
    float loadTextTimer = 0f;
    float fadeTimer = 0f;

    // Text gameObject
    public TextMeshProUGUI loadingTextBox;
    public TextMeshProUGUI percentLoaded;

    // Fade
    public CanvasGroup canvasGroup;
    bool fadeInLoad = true;
    bool startFadeOut = false;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 0.5f;

    // Start unload of previous scene
    bool unloadStart = true;
    bool jobDone = false;

    // Progress
    float progressValue = 0f;

    void Update()
    {
        // Fade in
        if (fadeInLoad)
        {
            // Fade in operation adjusts alpha for the canvas group containing loading scene images
            if (loadTimer < fadeInTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, loadTimer / fadeInTime);
            }
            else
            {
                // Once fade in complete, set alpha
                canvasGroup.alpha = 1;
                // Unload previous scene
                if (unloadStart)
                {
                    unloadingOperation = SceneManager.UnloadSceneAsync(LoadingData.sceneToUnload, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                    unloadStart = false;
                }

                // Load next scene
                if (unloadingOperation.isDone)
                {
                    loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Additive);
                    // prevent loading screen from flashing too quickly
                    loadingOperation.allowSceneActivation = false;
                    fadeInLoad = false;
                }
            }
        }
        else
        // Progress bar and duration control
        {
            // Load percent text output
            progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            percentLoaded.text = Mathf.Round(progressValue * 100).ToString();

            // Prevent loading screen from flashing too quickly even if loading is done
            if ((loadTimer > minLoadTime) && (Mathf.Approximately(loadingOperation.progress, 0.9f)))
            {
                loadingOperation.allowSceneActivation = true;
            }

            // If scene is loaded, start the fade out process
            if (!startFadeOut && loadingOperation.isDone)
            {
                startFadeOut = true;
                // Set active scene to newly loaded scene to prevent crossover code issues
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(LoadingData.sceneToLoad));
                // Set fade out timer to 0
                fadeTimer = 0f;
            }

            // Fade out operation adjusts alpha for canvas group containing loading screen images
            if (startFadeOut && (fadeTimer < fadeOutTime))
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTimer / fadeOutTime);
            }
            else if (startFadeOut && !jobDone && (fadeTimer >= fadeOutTime))
            {
                // Once fade out is complete, set alpha
                canvasGroup.alpha = 0;
                // Unload loading screen scene
                unloadingTransitionOperation = SceneManager.UnloadSceneAsync("Loading", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                jobDone = true;
            }
        }

        // Set text based on load time
        if (loadTextTimer < textChangeLoad)
        {
            loadingTextBox.text = "LOADING...";
        }
        else if (loadTextTimer < 2 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING..";
        }
        else if (loadTextTimer < 3 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING.";
        }
        else if (loadTextTimer < 4 * textChangeLoad)
        {
            loadingTextBox.text = "LOADING..";
        }
        else
        {
            loadTextTimer = 0f;
        }

        //Increment load timer for changing text
        loadTextTimer += Time.deltaTime;

        //Increment total load timer
        loadTimer += Time.deltaTime;

        // Increment fade timer
        fadeTimer += Time.deltaTime;
    }
}
