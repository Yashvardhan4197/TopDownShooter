
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
    [SerializeField] EnemyView enemyPrefab;
    //Data
    [SerializeField] PlayerDataSO playerDataSO;
    [SerializeField] WeaponDataSO weaponDataSO;
    [SerializeField] PickupDataSO pickupDataSO;
    [SerializeField] EnemyDataSO enemyDataSO;
    [SerializeField] float pickupProbabilityRate;
    [SerializeField] Transform enemyContainer;
    [SerializeField] AudioSource lFXAudioSource;
    [SerializeField] AudioSource sFXAudioSource;
    [SerializeField] AudioSource bgAudioSource;
    [SerializeField] SoundType[] soundTypes;
    //Services
    private PlayerService playerService;
    private WeaponService weaponService;
    private PickupService pickupService;
    private LevelService levelService;
    private UIService uIService;
    private SoundService soundService;
    public PlayerService PlayerService { get { return playerService; } }
    public WeaponService WeaponService { get { return weaponService; } }
    public LevelService LevelService { get { return levelService; } }
    public UIService UIService { get { return uIService; } }
    public PickupService PickupService { get { return pickupService; } }
    public SoundService SoundService { get { return soundService; } }
    //ACTIONS
    public UnityAction StartGameAction;
    //Public Pools
    private EnemyProjectilePool enemyProjectilePool;   
    private EnemyPool enemyPool;
    public EnemyProjectilePool EnemyProjectilePool { get { return enemyProjectilePool; } }
    public EnemyPool EnemyPool { get { return  enemyPool; } }

    private void Init()
    {
        playerService = new PlayerService(playerView,playerDataSO);
        weaponService = new WeaponService(weaponView,weaponDataSO);
        enemyProjectilePool = new EnemyProjectilePool(enemyProjectilePrefab);
        enemyPool = new EnemyPool(enemyPrefab, enemyDataSO, enemyContainer, playerView.transform);
        pickupService = new PickupService(pickupDataSO, pickupPrefab, pickupProbabilityRate);
        uIService = new UIService(lobbyView,levelUIView,inGameUIView,playerUIView,weaponUIView);
        levelService = new LevelService();
        soundService = new SoundService(bgAudioSource, sFXAudioSource, lFXAudioSource, soundTypes);
        uIService.GetLobbyController().OpenLobby();
        soundService.PlayBackGroundAudio(Sound.BACKGROUND_MUSIC);
    }
}
