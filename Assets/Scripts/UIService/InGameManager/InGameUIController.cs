
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIController
{
    private InGameUIView inGameUIView;
    private bool isPaused;
    private bool isLevelDone;
    public InGameUIController(InGameUIView inGameUIView)
    {
        this.inGameUIView = inGameUIView;
        this.inGameUIView.SetController(this);
        GameService.Instance.StartGameAction += CloseInGameUIPopUps;
    }

    public void CloseInGameUIPopUps()
    {
        ResumeGame();
        CloseGameLostScreen();
        CloseGameWinScreen();
        CheckForNextLevel();
    }

    public void TogglePause()
    {
        if (isLevelDone == false)
        {
            if (isPaused)
            {
                GameService.Instance.SoundService.SetPauseSpecialAudioSource(false);
                ResumeGame();
            }
            else
            {
                GameService.Instance.SoundService.SetPauseSpecialAudioSource(true);
                OpenPauseMenu();
            }
        }
        GameService.Instance.SoundService.PlaySFX(Sound.BUTTON_CLICK);
    }


    public void ResumeGame()
    {
        Time.timeScale = 1f;
        inGameUIView.GetPauseMenu().gameObject.SetActive(false);
        isPaused = false;
    }

    public void RestartGame()
    {
        //fix later
        GameService.Instance.StartGameAction?.Invoke();
        GameService.Instance.LevelService.ReloadLevel();
    }

    public void ExitToLobby()
    {
        GameService.Instance.UIService.GetLobbyController().OpenLobby();
        GameService.Instance.LevelService.LoadMenu();
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        inGameUIView.GetPauseMenu().gameObject.SetActive(true);
        isPaused = true;
    }


    public void OpenGameWinScreen()
    {
        inGameUIView.GetWinScreenMenu().gameObject.SetActive(true);
        GameService.Instance.SoundService.PlaySFX(Sound.GAME_WON);
        isLevelDone = true;
        Time.timeScale = 0f;
    }

    public void CloseGameWinScreen()
    {
        inGameUIView.GetWinScreenMenu().gameObject.SetActive(false);
        isLevelDone = false;
    }

    public void OpenGameLostScreen()
    {
        inGameUIView.GetLostScreenMenu().gameObject.SetActive(true);
        isLevelDone = true;
        Time.timeScale = 0f;
        GameService.Instance.SoundService.PlaySFX(Sound.GAME_LOST);
    }

    public void CloseGameLostScreen()
    {
        inGameUIView.GetLostScreenMenu().gameObject.SetActive(false);
        isLevelDone = false;
    }

    public void CheckForNextLevel()
    {
        if(GameService.Instance.LevelService.CurrentLevel+1< SceneManager.sceneCountInBuildSettings)
        {
            inGameUIView.GetNextLevelButton().gameObject.SetActive(true);
        }
        else
        {
            inGameUIView.GetNextLevelButton().gameObject.SetActive(false);
        }
    }

    public void SetNextLevel()
    {
        GameService.Instance.LevelService.LoadLevel(GameService.Instance.LevelService.CurrentLevel + 1);
    }
}
