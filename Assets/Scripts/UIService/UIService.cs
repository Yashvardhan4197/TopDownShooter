
public class UIService
{
    private LobbyController lobbyController;
    private LevelUIController levelUIController;
    private InGameUIController inGameUIController;
    private PlayerUIController playerUIController;
    private WeaponUIController weaponUIController;
    public UIService(LobbyView lobbyView,LevelUIView levelUIView, InGameUIView inGameUIView, PlayerUIView playerUIView, WeaponUIView weaponUIView)
    {
        lobbyController=new LobbyController(lobbyView);
        levelUIController = new LevelUIController(levelUIView);
        inGameUIController=new InGameUIController(inGameUIView);
        playerUIController=new PlayerUIController(playerUIView);
        weaponUIController=new WeaponUIController(weaponUIView);
    }

    public PlayerUIController GetPlayerUIController() => playerUIController;
    
    public LobbyController GetLobbyController() => lobbyController;
    
    public LevelUIController GetLevelUIController() => levelUIController;

    public InGameUIController GetInGameUIController()=> inGameUIController;

    public WeaponUIController GetWeaponUIController() => weaponUIController;
}
