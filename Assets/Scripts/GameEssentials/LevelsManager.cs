using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    #region SingletonBehaviour
    private static LevelsManager instance;

    public static LevelsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelsManager();
            }
            return instance;
        }
    }

    private LevelsManager()
    {
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
    #endregion
    [SerializeField]
    private List<string> playableScenes;
    [SerializeField]
    private string mainMenuScene;

    private int sceneToLoadIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        sceneToLoadIndex = 0;
        LoadScene();
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void PlayNextLevel()
    {
        sceneToLoadIndex = GetIndexOfASceneToLoad(false);
        LoadScene();
    }

    public void RestartCurrentLevel()
    {
        sceneToLoadIndex = GetIndexOfASceneToLoad(true);
        LoadScene();
    }

    private int GetIndexOfASceneToLoad(bool shouldRestart)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int index = playableScenes.FindIndex(x => x == sceneName);
        if (shouldRestart)
        {
            return index;
        }
        return index == playableScenes.Count ? 0 : index + 1;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(playableScenes[sceneToLoadIndex]);
    }
}
