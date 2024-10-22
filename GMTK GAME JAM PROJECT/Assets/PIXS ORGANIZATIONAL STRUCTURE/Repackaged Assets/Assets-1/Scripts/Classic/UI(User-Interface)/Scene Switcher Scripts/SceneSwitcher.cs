using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public KeyCode switchSceneKey = KeyCode.N; // Key to switch to the next scene
    public KeyCode reloadSceneKey = KeyCode.R; // Key to reload the current scene

    void Update()
    {
        if (Input.GetKeyDown(switchSceneKey))
        {
            SwitchToNextScene();
        }

        if (Input.GetKeyDown(reloadSceneKey))
        {
            ReloadCurrentScene();
        }
    }

    void SwitchToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void SwitchToSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Scene index out of bounds!");
        }
    }

    public void SwitchToSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name not found!");
        }
    }
}

