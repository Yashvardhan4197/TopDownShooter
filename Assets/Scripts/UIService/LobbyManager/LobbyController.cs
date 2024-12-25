using System;
using UnityEngine;

public class LobbyController
{
    private LobbyView lobbyView;

    public LobbyController(LobbyView lobbyView)
    {
        this.lobbyView = lobbyView;
        this.lobbyView.SetController(this);
        GameService.Instance.StartGameAction += CloseLobby;
    }

    public void OpenLobby()
    {
        lobbyView.gameObject.SetActive(true);
        GameService.Instance.UIService.GetLevelUIController().CloseLevelUISection();
        Time.timeScale = 0f;
    }

    public void CloseLobby()
    {
        lobbyView.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void OnStartButtonClicked()
    {
        GameService.Instance.UIService.GetLevelUIController().OpenLevelUISection();
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
