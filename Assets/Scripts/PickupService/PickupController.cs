using System.Threading.Tasks;
using UnityEngine;

public class PickupController
{

    private PickupView pickupView;
    private PickupDataSO pickupDataSO;
    private int currentEnemyTypeIndex;
    private Coroutine destructionCoroutine;
    private int destructionTimer;
    public int DestructionTimer { get { return destructionTimer; } }

    public PickupController(PickupView pickupPrefab, PickupDataSO pickupDataSO)
    {
        pickupView = Object.Instantiate(pickupPrefab);
        pickupView.transform.gameObject.SetActive(false);
        this.pickupDataSO = pickupDataSO;
        pickupView.SetController(this);
    }

    public void InitializePickup(Vector3 position)
    {
        RandomizePickup();
        pickupView.GetAnimator().runtimeAnimatorController = pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupAnimator;
        destructionTimer = pickupDataSO.PickupCollections[currentEnemyTypeIndex].DestructTime;
        pickupView.gameObject.SetActive(true);
        pickupView.transform.position = position;
        CheckDestructCoroutine();
    }

    private void CheckDestructCoroutine()
    {
        if (destructionCoroutine != null)
        {
            pickupView.StopCoroutine();
        }
        destructionCoroutine = null;
        pickupView.StartCoroutine();
    }

    private void RandomizePickup()
    {
        //randomize pickup based on pickupData types
        int rand=Random.Range(0,pickupDataSO.PickupCollections.Count);
        currentEnemyTypeIndex = rand;
    }

    public void OnPickupEquipped()
    {
        if (pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.HEALTH_BOOST)
        {
            Debug.Log("Player health increase");
        }
        else if(pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.SHIELD)
        {
            Debug.Log("Invincible Enemy");
        }
        else if(pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.AMMO_BOOST)
        {
            Debug.Log("Ammo Increased");
        }


        ReturnPickup();
    }
    
    public void ReturnPickup()
    {
        pickupView?.gameObject.SetActive(false);
        //Return to Pool
        GameService.Instance.PickupService.ReturnToPool(this);
    }

    public void SetDestructionTimer(Coroutine coroutine)
    {
        destructionCoroutine= coroutine;
    }
}