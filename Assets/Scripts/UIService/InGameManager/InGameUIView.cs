
using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIView : MonoBehaviour
{
    private InGameUIController inGameUIController;
    [SerializeField] GameObject PauseMenuPopUpGB;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button RestartButtonPauseMenuPopUo;
    [SerializeField] Button ExitToLobbyButtonPauseMenuPopUp;

    [SerializeField] GameObject GameLostPopUpGB;
    [SerializeField] Button RestartButtonGameLostPopUp;
    [SerializeField] Button ExitToLobbyGameLostPopUp;

    [SerializeField] GameObject GameCompletedPopUpGB;
    [SerializeField] Button NextLevelButton;
    [SerializeField] Button RestartButtonGameCompletePopUp;
    [SerializeField] Button ExitToLobbyGameWonPopUp;

    private void Start()
    {
        ResumeButton.onClick.AddListener(OnResumeGameButtonClicked);
        RestartButtonPauseMenuPopUo.onClick.AddListener(OnRestartButtonClicked);
        ExitToLobbyButtonPauseMenuPopUp.onClick.AddListener(OnExitToLobbyButtonClicked);

        RestartButtonGameLostPopUp.onClick.AddListener(OnRestartButtonClicked);
        ExitToLobbyGameLostPopUp.onClick.AddListener(OnExitToLobbyButtonClicked);

        RestartButtonGameCompletePopUp.onClick.AddListener(OnRestartButtonClicked);
        ExitToLobbyGameWonPopUp.onClick.AddListener(OnExitToLobbyButtonClicked);
        NextLevelButton.onClick.AddListener(OpenNextLevel);
    }

    private void OnExitToLobbyButtonClicked()
    {
        inGameUIController.ExitToLobby();
    }

    private void OnRestartButtonClicked()
    {
        inGameUIController.RestartGame();
    }

    private void OnResumeGameButtonClicked()
    {
        inGameUIController.ResumeGame();
    }

    public void SetController(InGameUIController inGameUIController)
    {
        this.inGameUIController= inGameUIController;
    }

    public void OpenNextLevel()
    {
        inGameUIController?.SetNextLevel();
    }


    public GameObject GetLostScreenMenu() => GameLostPopUpGB;

    public GameObject GetWinScreenMenu() => GameCompletedPopUpGB;

    public GameObject GetPauseMenu() => PauseMenuPopUpGB;

    public Button GetNextLevelButton() => NextLevelButton;

}
