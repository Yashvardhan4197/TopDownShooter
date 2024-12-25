
using UnityEngine;

public class InGameUIController
{
    private InGameUIView inGameUIView;
    private bool isPaused;
    public InGameUIController(InGameUIView inGameUIView)
    {
        this.inGameUIView = inGameUIView;
        this.inGameUIView.SetController(this);
        GameService.Instance.StartGameAction += CloseInGameUIPopUps;
    }

    public void CloseInGameUIPopUps()
    {
        ResumeGame();
        //change for win lose popups too;
    }

    public void TogglePause()
    {
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            OpenPauseMenu();
        }
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

}
