using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView:MonoBehaviour
{
    private LobbyController lobbyController;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    public void SetController(LobbyController lobbyController)
    {
        this.lobbyController = lobbyController;
    }

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    public void OnExitButtonClicked()
    {
        lobbyController.OnExitButtonClicked();
    }

    private void OnStartButtonClicked()
    {
        lobbyController.OnStartButtonClicked();
    }

}