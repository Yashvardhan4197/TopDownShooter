
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
        int rand = Random.Range(0, pickupDataSO.PickupCollections.Count);
        currentEnemyTypeIndex = rand;
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

    public void InitializeIsolatedPickup(Vector3 position, PickupType pickupType)
    {
        for (int i = 0;i<pickupDataSO.PickupCollections.Count;i++)
        {
            if(pickupType==pickupDataSO.PickupCollections[i].PickupType)
            {
                currentEnemyTypeIndex = i;
                break;
            }
        }
        pickupView.GetAnimator().runtimeAnimatorController = pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupAnimator;
        destructionTimer = pickupDataSO.PickupCollections[currentEnemyTypeIndex].DestructTime;
        pickupView.gameObject.SetActive(true);
        pickupView.transform.position = position;

    }

    public void OnPickupEquipped()
    {
        if (pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.HEALTH_BOOST)
        {
            GameService.Instance.PlayerService.GetPlayerController().AddPlayerHealth(pickupDataSO.PickupCollections[currentEnemyTypeIndex].HealthBoost);
        }
        else if(pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.SHIELD)
        {
            GameService.Instance.PlayerService.GetPlayerController().ActivateShield(pickupDataSO.PickupCollections[currentEnemyTypeIndex].ActivatedTimer);
        }
        else if(pickupDataSO.PickupCollections[currentEnemyTypeIndex].PickupType==PickupType.AMMO_BOOST)
        {
            GameService.Instance.WeaponService.GetWeaponController().IncreaseAmmo(pickupDataSO.PickupCollections[currentEnemyTypeIndex].AmmoBoost);
        }
        GameService.Instance.SoundService.PlaySFX(Sound.PICKUP);
        ReturnPickup();
    }
    
    public void ReturnPickup()
    {
        pickupView?.gameObject.SetActive(false);
        GameService.Instance.PickupService.ReturnToPool(this);
    }

    public void SetDestructionTimer(Coroutine coroutine)
    {
        destructionCoroutine= coroutine;
    }



}