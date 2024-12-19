
public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView,PlayerDataSO playerData)
    {
        playerController=new PlayerController(playerView,playerData);
    }

    public PlayerController GetPlayerController() => playerController;
}
