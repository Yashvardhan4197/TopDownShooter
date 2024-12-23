
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
    [SerializeField] EnemyProjectileView enemyProjectilePrefab;
    //Data
    [SerializeField] PlayerDataSO playerDataSO;
    [SerializeField] WeaponDataSO weaponDataSO;
    //Services
    private PlayerService playerService;
    private WeaponService weaponService;
    public PlayerService PlayerService { get { return playerService; } }
    public WeaponService WeaponService { get { return weaponService; } }
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
    }
}
