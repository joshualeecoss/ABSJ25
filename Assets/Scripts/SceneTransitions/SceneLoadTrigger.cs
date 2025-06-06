using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    public string targetScene;

    public void LoadScene()
    {
        LoadingData.sceneToLoad = targetScene;
        LoadingData.sceneToUnload = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
    }

    public void LoadSceneNoLoadingScreen()
    {
        LoadingData.sceneToLoad = targetScene;
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Single);
    }

    public void SetTargetScene(string newScene)
    {
        targetScene = newScene;
    }
}
