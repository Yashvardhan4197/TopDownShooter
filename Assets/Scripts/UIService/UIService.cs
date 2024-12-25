
public class UIService
{
    private LobbyController lobbyController;
    private LevelUIController levelUIController;
    public UIService(LobbyView lobbyView,LevelUIView levelUIView)
    {
        lobbyController=new LobbyController(lobbyView);
        levelUIController = new LevelUIController(levelUIView);
    }

    public LobbyController GetLobbyController() => lobbyController;
    public LevelUIController GetLevelUIController() => levelUIController;
}
