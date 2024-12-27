
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelService
{
    private int currentLevel;
    public int CurrentLevel {  get { return currentLevel; } }
    public LevelService()
    {
        currentLevel = 0;
    }

    public void ReloadLevel()
    {
        if(currentLevel != 0) 
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(currentLevel));
            SceneManager.LoadScene(currentLevel,LoadSceneMode.Additive);
            GameService.Instance.StartGameAction?.Invoke();
            Debug.Log("CurrentLevel: " + currentLevel);
        }
    }


    public void LoadLevel(int levelNumber)
    {
        Debug.Log("CurrentLevel: " + currentLevel + " level to change: " + levelNumber);
        if (currentLevel != 0)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(currentLevel));
        }
        if (levelNumber>=SceneManager.sceneCountInBuildSettings)
        {
            //set to 0
            currentLevel = 0;
        }
        else
        {
            currentLevel = levelNumber;
        }
        if (currentLevel != 0)
        {
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
            GameService.Instance.StartGameAction?.Invoke();
        }
        else
        {
            GameService.Instance.UIService.GetLobbyController().OpenLobby();
        }
    }

    public void LoadMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(currentLevel));
        currentLevel = 0;
        //SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
    }

}

