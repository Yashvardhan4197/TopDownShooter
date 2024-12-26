
using UnityEngine;
using UnityEngine.Events;

public class GameService : MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance { get { return instance; } }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Views
    [SerializeField] PlayerView playerView;
    [SerializeField] WeaponView weaponView;
    [SerializeField] LobbyView lobbyView;
    [SerializeField] LevelUIView levelUIView;
    [SerializeField] InGameUIView inGameUIView;
    [SerializeField] PlayerUIView playerUIView;
    [SerializeField] WeaponUIView weaponUIView;
    [SerializeField] EnemyProjectileView enemyProjectilePrefab;
    [SerializeField] PickupView pickupPrefab;
    //Data
    [SerializeField] PlayerDataSO playerDataSO;
    [SerializeField] WeaponDataSO weaponDataSO;
    [SerializeField] PickupDataSO pickupDataSO;
    [SerializeField] float pickupProbabilityRate;
    //Services
    private PlayerService playerService;
    private WeaponService weaponService;
    private PickupService pickupService;
    private LevelService levelService;
    private UIService uIService;
    public PlayerService PlayerService { get { return playerService; } }
    public WeaponService WeaponService { get { return weaponService; } }
    public LevelService LevelService { get { return levelService; } }
    public UIService UIService { get { return uIService; } }
    public PickupService PickupService { get { return pickupService; } }
    //ACTIONS
    public UnityAction StartGameAction;
    //Public Pools
    private EnemyProjectilePool enemyProjectilePool;   
    public EnemyProjectilePool EnemyProjectilePool { get { return enemyProjectilePool; } }

    private void Init()
    {
        playerService = new PlayerService(playerView,playerDataSO);
        weaponService = new WeaponService(weaponView,weaponDataSO);
        enemyProjectilePool = new EnemyProjectilePool(enemyProjectilePrefab);
        pickupService = new PickupService(pickupDataSO, pickupPrefab, pickupProbabilityRate);
        uIService = new UIService(lobbyView,levelUIView,inGameUIView,playerUIView,weaponUIView);
        levelService = new LevelService();
        uIService.GetLobbyController().OpenLobby();
    }
}
