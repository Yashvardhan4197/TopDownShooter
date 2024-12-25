
using UnityEngine.SceneManagement;

public class LevelService
{
    private int currentLevel;
    public LevelService()
    {
        currentLevel = 0;
    }

    public void ReloadLevel()
    {
        if(currentLevel != 0) 
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(currentLevel));
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
        }
    }


    public void LoadLevel(int levelNumber)
    {
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

