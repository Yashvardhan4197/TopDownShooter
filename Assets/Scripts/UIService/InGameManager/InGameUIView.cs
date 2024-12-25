
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

    private void Start()
    {
        ResumeButton.onClick.AddListener(OnResumeGameButtonClicked);
        RestartButtonPauseMenuPopUo.onClick.AddListener(OnRestartButtonClicked);
        ExitToLobbyButtonPauseMenuPopUp.onClick.AddListener(OnExitToLobbyButtonClicked);
    }

    private void OnExitToLobbyButtonClicked()
    {

    }

    private void OnRestartButtonClicked()
    {

    }

    private void OnResumeGameButtonClicked()
    {

    }

    public void SetController(InGameUIController inGameUIController)
    {
        this.inGameUIController= inGameUIController;
    }
}
