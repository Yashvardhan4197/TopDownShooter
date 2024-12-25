
public class UIService
{
    private LobbyController lobbyController;
    private LevelUIController levelUIController;
    private InGameUIController inGameUIController;
    public UIService(LobbyView lobbyView,LevelUIView levelUIView, InGameUIView inGameUIView)
    {
        lobbyController=new LobbyController(lobbyView);
        levelUIController = new LevelUIController(levelUIView);
        inGameUIController=new InGameUIController(inGameUIView);
    }

    public LobbyController GetLobbyController() => lobbyController;
    public LevelUIController GetLevelUIController() => levelUIController;

    public InGameUIController GetInGameUIController()=> inGameUIController;
}
